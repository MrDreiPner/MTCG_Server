using MTCG.MTCG.Models;
using MTCG.MTCG.BLL;
using MTCG.MTCG.DAL;
using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.MTCG.BLL
{
    public class PackageManager : IPackageManager
    {
        private readonly IPackageDao _packageDao;

        public PackageManager(IPackageDao packageDao)
        {
            _packageDao = packageDao;
        }

        public Package AddPackage(List<Card> packContent)
        {
            var package = _packageDao.AddPackage(packContent);
            return package;
        }

        public Package? BuyPackage(string username)
        {
            return _packageDao.GetOldestPackage(username);
        }
    }
}
