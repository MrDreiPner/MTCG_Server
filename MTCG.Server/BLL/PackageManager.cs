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

        public Package? BuyPackage(string username)
        {
            return _packageDao.GetOldestPackage(username);
        }
    }
}
