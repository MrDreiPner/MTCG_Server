using MTCG_Server.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.MTCG.DAL
{
    internal interface IPackageDao
    {
        //IEnumerable<Package> GetPackages();
        Package? GetOldestPackage(string username);
        Package? AddPackage(List<Card> packContent);
    }
}
