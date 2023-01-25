using MTCG_Server.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.MTCG.DAL
{
    internal interface ICardDao
    {
        List<Card>? ShowCards(string username);
        List<Card>? ShowDeck(string username);
        List<Card>? ConfigureDeck(string username, List<string> arguments);
    }
}
