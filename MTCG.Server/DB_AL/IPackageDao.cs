using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.MTCG.DAL
{
    public interface IPackageDao
    {
        //IEnumerable<Package> GetPackages();
        Package? GetOldestPackage(string username);
        Package? AddPackage(List<Card> packContent);
    }
}
