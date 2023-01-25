using MTCG_Server.MTCG.API.RouteCommands;
using MTCG_Server.MTCG.BLL;
using MTCG_Server.MTCG.Core.Response;
using MTCG_Server.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.MTCG.DAL;
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
