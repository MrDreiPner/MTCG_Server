using MTCG_Server.CardTypes;
using SWE1.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.BLL
{
    internal interface ICardManager
    {
        //public IEnumerable<Package> ShowPackages();
        List<Card>? ShowCards(string username);
        List<Card>? ShowDeck(string username);
        List<Card>? ConfigureDeck(string username, List<string> arguments);
    }
}
