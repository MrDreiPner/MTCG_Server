﻿using SWE1.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.DAL
{
    internal interface IPackageDao
    {
        //IEnumerable<Package> GetPackages();
        Package? GetOldestPackage(string username);
        Package? AddPackage(List<Card> packContent);
    }
}
