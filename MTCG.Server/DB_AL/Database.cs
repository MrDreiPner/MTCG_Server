using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MessageServer.DAL
{
    internal class Database
    {
        // see also https://www.postgresql.org/docs/current/ddl-constraints.html
        private const string CreateTablesCommand = @"
CREATE TABLE IF NOT EXISTS users (uid serial PRIMARY KEY, username varchar, password varchar, bio varchar, image varchar, name varchar , coins numeric, elo numeric);
CREATE TABLE IF NOT EXISTS cards (id serial PRIMARY KEY, cardname varchar, elementID elementIDenum, dmg numeric, inDeck boolean, inTrade boolean, ownerID numeric, packID numeric, type varchar);
";
        /*CREATE TYPE elementIDenum AS ENUM ('Water', 'Fire', 'Normal');*/


        public IMessageDao MessageDao { get; private set; }
        public IUserDao UserDao { get; private set; }

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
