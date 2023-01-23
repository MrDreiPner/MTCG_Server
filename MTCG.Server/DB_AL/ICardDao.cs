using SWE1.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.DAL
{
    internal interface ICardDao
    {
        List<Card>? ShowCards(string username);
        List<Card>? ShowDeck(string username);
        List<Card>? ConfigureDeck(string username, List<string> arguments);
    }
}
