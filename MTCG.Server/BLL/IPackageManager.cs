using MTCG.CardTypes;
using MTCG.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.MTCG.BLL
{
    public interface IPackageManager
    {
        //public IEnumerable<Package> ShowPackages();
        Package AddPackage(List<Card> packContent);
        Package BuyPackage(string username);
    }
}
