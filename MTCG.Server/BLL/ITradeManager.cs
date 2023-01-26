using MTCG.CardTypes;
using MTCG.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MTCG.BattleClasses;

namespace MTCG.MTCG.BLL
{
    internal interface ITradeManager
    {
        public void Trade(string tradeID, string cardID, string username);
        public void CreateTrade(TradeDeal _tradeDeal, string username);
        public List<TradeDeal> GetTradeDeals();
        public void DeleteTrade(string tradeID, string username);
    }
}
