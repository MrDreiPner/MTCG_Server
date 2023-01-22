using SWE1.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.DAL
{
    public interface IUserDao
    {
        User? GetUserByAuthToken(string authToken);
        User? GetUserByCredentials(string username, string password);
        User? GetUserByUsername(string username);
        UserContent GetUser(string userToFetch);
        bool UpdateUser(string username, UserContent userContent);
        bool InsertUser(User user);
    }
}
