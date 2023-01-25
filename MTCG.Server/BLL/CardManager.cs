using MTCG_Server.MTCG.Models;
using MTCG_Server.MTCG.BLL;
using MTCG_Server.MTCG.DAL;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.MTCG.BLL
{
    internal class CardManager : ICardManager
    {
        private readonly ICardDao _cardDao;

        public CardManager(ICardDao cardDao)
        {
            _cardDao = cardDao;
        }

        public List<Card>? ShowCards(string username)
        {
            //Console.WriteLine("We are in the card manager. Userename: " + username);
            return _cardDao.ShowCards(username);
        }

        public List<Card>? ShowDeck(string username)
        {
            //Console.WriteLine("We are in the card manager. Userename: " + username);
            return _cardDao.ShowDeck(username);
        }
        public List<Card>? ConfigureDeck(string username, List<string> arguments)
        {
            //Console.WriteLine("We are in the card manager. Userename: " + username);
            return _cardDao.ConfigureDeck(username, arguments);
        }
    }
}
