using SWE1.MTCG.API.RouteCommands;
using SWE1.MTCG.BLL;
using SWE1.MTCG.Core.Response;
using SWE1.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1.MTCG.DAL;
using Newtonsoft.Json;

namespace MTCG_Server.API.RouteCommands.Cards
{
    internal class ShowCardsCommand : AuthenticatedRouteCommand
    {
        private readonly ICardManager _cardManager;

        public ShowCardsCommand(ICardManager cardManager, User identity) : base(identity)
        {
            if (Identity.Username != null)
            {
                _cardManager = cardManager;
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                List<Card>? package = null;
                package = _cardManager.ShowCards(Identity.Username);
                response.StatusCode = StatusCode.Ok;
                response.Payload = JsonConvert.SerializeObject(package, Formatting.Indented);
            }
            catch (Exception ex)
            {
                if (ex is MessageNotFoundException)
                    response.StatusCode = StatusCode.NotFound;
                else if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
            }
            return response;
        }
    }
}
