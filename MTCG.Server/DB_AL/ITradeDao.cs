using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.MTCG.DAL
{
    public interface ITradeDao
    {
        public bool CheckValidity(string tradeID, string cardID, string username);
        public void CompleteTrade(string tradeID, string cardID, string username);
        public void CreateTrade(TradeDeal tradeDeal, string username);
        public List<TradeDeal> GetTradeDeals();
        public void DeleteTrade(string tradeID, string username);

    }
}
