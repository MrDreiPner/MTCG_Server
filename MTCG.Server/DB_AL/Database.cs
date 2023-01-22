using SWE1.MTCG.DAL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.DAL
{
    internal class Database
    {
        // see also https://www.postgresql.org/docs/current/ddl-constraints.html
        private const string CreateTablesCommand = @"
CREATE TABLE IF NOT EXISTS users (username varchar PRIMARY KEY, password varchar, bio varchar, image varchar, name varchar , coins numeric, elo numeric);
CREATE TABLE IF NOT EXISTS cards (cid varchar PRIMARY KEY, cardname varchar, elementID element_id, dmg numeric, inDeck boolean, inTrade boolean, ownerID varchar, packID numeric, type varchar, CONSTRAINT owner_ID FOREIGN KEY(ownerID) REFERENCES users(username));
CREATE TABLE IF NOT EXISTS package (pid serial PRIMARY KEY) 
";
        /*CREATE TYPE element_id AS ENUM ('water', 'fire', 'normal');*/


        public IMessageDao MessageDao { get; private set; }
        public IUserDao UserDao { get; private set; }
        public IPackageDao PackageDao { get; private set; }

        public Database(string connectionString)
        {
            try
            {
                try
                {
                    // https://github.com/npgsql/npgsql/issues/1837https://github.com/npgsql/npgsql/issues/1837
                    // https://www.npgsql.org/doc/basic-usage.html
                    // https://www.npgsql.org/doc/connection-string-parameters.html#pooling
                    EnsureTables(connectionString);
                    Console.WriteLine("Tables created");
                }
                catch (NpgsqlException e)
                {
                    // provide our own custom exception
                    throw new DataAccessFailedException("Could not connect to or initialize database", e);
                }

                UserDao = new DatabaseUserDao(connectionString);
                MessageDao = new DatabaseMessageDao(connectionString);
                PackageDao = new DatabasePackageDao(connectionString);
            }
            catch (NpgsqlException e)
            {
                // provide our own custom exception
                throw new DataAccessFailedException("Could not connect to or initialize database", e);
            }
        }

        private void EnsureTables(string connectionString)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var cmd = new NpgsqlCommand(CreateTablesCommand, connection);
            int check = cmd.ExecuteNonQuery();
            Console.WriteLine("Number of rows affected: " + check);
        }
    }
}
