using MTCG.MTCG.API.RouteCommands;
using MTCG.MTCG.BLL;
using MTCG.MTCG.Core.Response;
using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.MTCG.DAL;
using Newtonsoft.Json;
using Microsoft.VisualBasic;

namespace MTCG.API.RouteCommands.Trades
{
    internal class FetchTradeDealsCommand : AuthenticatedRouteCommand
    {
        private readonly ITradeManager _tradeManager;

        public FetchTradeDealsCommand(ITradeManager tradeManager, User identity) : base(identity)
        {
            if (Identity.Username != null)
            {
                _tradeManager = tradeManager;
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                List<TradeDeal> tradeDeals = new List<TradeDeal>();
                tradeDeals = _tradeManager.GetTradeDeals();
                response.StatusCode = StatusCode.Ok;
                response.Payload = JsonConvert.SerializeObject(tradeDeals, Formatting.Indented);
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if(ex is MessageNotFoundException) 
                    response.StatusCode = StatusCode.NoContent;

            }
            return response;
        }
    }
}