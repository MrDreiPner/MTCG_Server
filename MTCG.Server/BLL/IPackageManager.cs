using MTCG_Server.CardTypes;
using MTCG_Server.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.MTCG.BLL
{
    internal interface IPackageManager
    {
        //public IEnumerable<Package> ShowPackages();
        Package AddPackage(List<Card> packContent);
        Package BuyPackage(string username);
    }
}
