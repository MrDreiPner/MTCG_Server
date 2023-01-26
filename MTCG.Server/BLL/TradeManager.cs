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
    public class TradeManager : ITradeManager
    {
        private readonly ITradeDao _tradeDao;

        public TradeManager(ITradeDao tradeDao)
        {
            _tradeDao = tradeDao;
        }

        public void Trade(string tradeID, string cardID, string username)
        {
            if(_tradeDao.CheckValidity(tradeID, cardID, username))
                _tradeDao.CompleteTrade(tradeID, cardID, username);
        }
        public void CreateTrade(TradeDeal tradeDeal, string username)
        {
            _tradeDao.CreateTrade(tradeDeal, username);
        }
        public List<TradeDeal> GetTradeDeals()
        {
            return _tradeDao.GetTradeDeals();
        }
        public void DeleteTrade(string tradeID, string username)
        {
            _tradeDao.DeleteTrade(tradeID, username);
        }
    }
}
