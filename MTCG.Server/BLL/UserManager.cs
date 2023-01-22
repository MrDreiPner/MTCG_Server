using SWE1.MTCG.DAL;
using SWE1.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.BLL
{
    public class UserManager : IUserManager
    {
        private readonly IUserDao _userDao;

        public UserManager(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void UpdateUser(User user, UserContent userContent, string userToUpdate)
        {
            Console.WriteLine("We are here - user to update: " + userToUpdate);
            if (_userDao.UpdateUser(userToUpdate, userContent) == false)
                throw new UserNotFoundException();
        }

        public UserContent? GetUser(string userToFetch)
        {
            UserContent user = _userDao.GetUser(userToFetch);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            else
            {
                return user;
            }
        }

        public User LoginUser(Credentials credentials)
        {
            return _userDao.GetUserByCredentials(credentials.Username, credentials.Password) ?? throw new UserNotFoundException();
        }

        public void RegisterUser(Credentials credentials)
        {
            var user = new User(credentials.Username, credentials.Password);
            if (_userDao.InsertUser(user) == false)
            {
                throw new DuplicateUserException();
            }
        }

        public User GetUserByUsername(string username)
        {
            User? user = _userDao.GetUserByUsername(username);
            if (user == null)
            {
                Console.WriteLine("Exception thrown");
                throw new UserNotFoundException();
            }
            return user;
        }
        public User GetUserByAuthToken(string authToken)
        {
            User? user = _userDao.GetUserByAuthToken(authToken);
            if(user == null)
            {
                Console.WriteLine("Exception thrown"); 
                throw new UserNotFoundException();
            }
            return user;
        }
    }
}
