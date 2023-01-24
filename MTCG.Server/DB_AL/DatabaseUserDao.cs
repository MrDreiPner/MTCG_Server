using MTCG_Server;
using Npgsql;
using SWE1.MTCG.BLL;
using SWE1.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.DAL
{
    internal class DatabaseUserDao : DatabaseBaseDao, IUserDao
    {
        private const string InsertUserCommand = "INSERT INTO users(username, password, name, coins, elo, wins, losses) VALUES (@username, @password, @username, 20, 100, 0, 0)";
        private const string SelectUsersCommand = "SELECT username, password FROM users";
        private const string SelectUserByCredentialsCommand = "SELECT username, password FROM users WHERE username=@username AND password=@password";
        private const string SelectUserContentByUsername = "SELECT name, bio, image FROM users WHERE username=@username";
        private const string UpdateUserCommand = "UPDATE users SET name = @name, bio = @bio, image = @image WHERE username=@username";
        private const string SelectUserByUsername = "SELECT username, password FROM users WHERE username=@username";


        public DatabaseUserDao(string connectionString) : base(connectionString)
        {
        }

        private List<User> GetAllUsers()
        {
            return ExecuteWithDbConnection((connection) =>
            {
                var users = new List<User>();

                using var cmd = new NpgsqlCommand(SelectUsersCommand, connection);
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var user = ReadUser(reader);
                    users.Add(user);
                }

                return users;
            });
        }

        public UserContent? GetUser(string username)
        {
            return ExecuteWithDbConnection((connection) =>
            {
                UserContent? userContent = null;

                using var cmd = new NpgsqlCommand(SelectUserContentByUsername, connection);
                cmd.Parameters.AddWithValue("username", username);

                // take the first row, if any
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userContent = ReadUserContent(reader);
                }
                return userContent;
            });

        }
        public bool UpdateUser(string username, UserContent userContent)
        {
            return ExecuteWithDbConnection((connection) =>
            {
                Console.WriteLine("We are about to deal with DB");
                using var cmd = new NpgsqlCommand(UpdateUserCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("name", userContent.Name);
                cmd.Parameters.AddWithValue("bio", userContent.Bio);
                cmd.Parameters.AddWithValue("image", userContent.Image);
                cmd.Prepare();
                // take the first row, if any
                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("We edited!");
                    return true;
                }
                return false;
            });
        }
        public User? GetUserByAuthToken(string authToken)
        {
            return GetAllUsers().SingleOrDefault(u => u.Token == authToken);
        }

        public User? GetUserByUsername(string username)
        {
            return ExecuteWithDbConnection((connection) =>
            {
                User? user = null;

                using var cmd = new NpgsqlCommand(SelectUserByUsername, connection);
                cmd.Parameters.AddWithValue("username", username);

                // take the first row, if any
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = ReadUser(reader);
                }

                return user;
            });
        }
        public User? GetUserByCredentials(string username, string password)
        {
            return ExecuteWithDbConnection((connection) =>
            {
                User? user = null;

                using var cmd = new NpgsqlCommand(SelectUserByCredentialsCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);

                // take the first row, if any
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = ReadUser(reader);
                }

                return user;
            });
        }

        public bool InsertUser(User user)
        {
            return ExecuteWithDbConnection((connection) =>
            {
                var affectedRows = 0;
                try
                {
                    using var cmd = new NpgsqlCommand(InsertUserCommand, connection);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    affectedRows = cmd.ExecuteNonQuery();
                }
                catch (PostgresException)
                {
                    // this might happen, if the user already exists (constraint violation)
                    // we just catch it an keep affectedRows at zero
                }

                return affectedRows > 0;
            });
        }

        private UserContent ReadUserContent(IDataRecord record)
        {
            return new UserContent(Convert.ToString(record["name"])!, Convert.ToString(record["image"])!, Convert.ToString(record["bio"])!);
        }

        private User ReadUser(IDataRecord record)
        {
            return new User(Convert.ToString(record["username"])!, Convert.ToString(record["password"])!);
        }
        
    }
}
