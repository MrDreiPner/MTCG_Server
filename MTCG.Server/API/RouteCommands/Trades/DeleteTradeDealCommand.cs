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
    internal class DeleteTradeDealCommand : AuthenticatedRouteCommand
    {
        private readonly ITradeManager _tradeManager;
        private readonly string _tradeID;


        public DeleteTradeDealCommand(ITradeManager tradeManager, User identity, string tradeID) : base(identity)
        {
            if (Identity.Username != null)
            {
                _tradeManager = tradeManager;
                _tradeID = tradeID;
                Console.WriteLine(tradeID);
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                _tradeManager.DeleteTrade(_tradeID, Identity.Username);
                response.StatusCode = StatusCode.Ok;
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if (ex is WrongCardOwnerException)
                    response.StatusCode = StatusCode.Forbidden;
                else if (ex is MessageNotFoundException)
                    response.StatusCode = StatusCode.NotFound;
            }
            return response;
        }
    }
}
