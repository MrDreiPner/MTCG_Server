using MTCG_Server.CardTypes;
using SWE1.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.BLL
{
    internal interface IPackageManager
    {
        //public IEnumerable<Package> ShowPackages();
        Package AddPackage(List<Card> packContent);
        void BuyPackage();
    }
}
