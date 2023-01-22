using SWE1.MTCG.Models;
using SWE1.MTCG.BLL;
using SWE1.MTCG.DAL;
using SWE1.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.BLL
{
    internal class PackageManager : IPackageManager
    {
        private readonly IPackageDao _packageDao;

        public PackageManager(IPackageDao packageDao)
        {
            _packageDao = packageDao;
        }

        public Package AddPackage(List<Card> packContent)
        {
            Console.WriteLine("We are in the package manager");
            var package = _packageDao.AddPackage(packContent);
            return package;
        }

        public void BuyPackage()
        {
            if (_packageDao.GetOldestPackage() == null)
            {
                throw new MessageNotFoundException();
            }
        }

        /*public IEnumerable<Package> ShowPackages()
        {
            return _packageDao.GetPackages();
        }
        public void UpdateMessage(User user, int messageId, string content)
        {
            Message? message;
            if ((message = _messageDao.GetMessageById(user.Username, messageId)) != null)
            {
                message.Content = content;
                _messageDao.UpdateMessage(user.Username, message);
            }
            else
            {
                throw new MessageNotFoundException();
            }
        }*/
    }
}
