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
    internal class CreateTradeDealCommand : AuthenticatedRouteCommand
    {
        private readonly ITradeManager _tradeManager;
        private readonly TradeDeal _tradeDeal;


        public CreateTradeDealCommand(ITradeManager tradeManager, User identity, TradeDeal tradeDeal) : base(identity)
        {
            if (Identity.Username != null)
            {
                _tradeManager = tradeManager;
                _tradeDeal = tradeDeal;
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                _tradeManager.CreateTrade(_tradeDeal, Identity.Username);
                response.StatusCode = StatusCode.Ok;
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if(ex is WrongCardOwnerException) 
                    response.StatusCode = StatusCode.Forbidden;
                else if(ex is CardAlreadyExistsException)
                    response.StatusCode = StatusCode.Conflict;

            }
            return response;
        }
    }
}
