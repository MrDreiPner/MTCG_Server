using MTCG.MTCG.Models;
using MTCG.MTCG.BLL;
using MTCG.MTCG.DAL;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.MTCG.BLL
{
    public class CardManager : ICardManager
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
