using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.MTCG.DAL
{
    public interface ICardDao
    {
        List<Card>? ShowCards(string username);
        List<Card>? ShowDeck(string username);
        List<Card>? ConfigureDeck(string username, List<string> arguments);
    }
}
