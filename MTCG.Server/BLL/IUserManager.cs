using MTCG_Server.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.MTCG.BLL
{
    internal interface IUserManager
    {
        User LoginUser(Credentials credentials);
        UserContent GetUser(string userToFetch);
        void UpdateUser(User user, UserContent userContent, string userToUpdate);
        void RegisterUser(Credentials credentials);
        User GetUserByAuthToken(string authToken);
        User GetUserByUsername(string username);
    }
}
