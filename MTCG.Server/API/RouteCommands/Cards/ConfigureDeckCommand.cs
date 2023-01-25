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
using Microsoft.VisualBasic;

namespace MTCG_Server.API.RouteCommands.Cards
{
    internal class ConfigureDeckCommand : AuthenticatedRouteCommand
    {
        private readonly ICardManager _cardManager;
        private readonly List<string> _arguments;

        public ConfigureDeckCommand(ICardManager cardManager, User identity, List<string> arguments) : base(identity)
        {
            if (Identity.Username != null)
            {
                _cardManager = cardManager;
                _arguments = arguments;
                foreach(string arg in _arguments)
                {
                    Console.WriteLine(arg);
                }
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                if (_arguments.Count < 4 || _arguments.Distinct().Count() != _arguments.Count())
                {
                    throw new TooFewArgumentsException();
                }
                else
                {
                    List<Card>? cards = null;
                    cards = _cardManager.ConfigureDeck(Identity.Username, _arguments);
                    response.StatusCode = StatusCode.Ok;
                    response.Payload = "!-- This is " + Identity.Username + "'s current Deck --!\n";
                    response.Payload += JsonConvert.SerializeObject(cards, Formatting.Indented);
                }
            }
            catch (Exception ex)
            {
                if (ex is WrongCardOwnerException)
                {
                    response.StatusCode = StatusCode.Forbidden;
                }
                else if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if(ex is TooFewArgumentsException)
                {
                    response.StatusCode = StatusCode.BadRequest;
                } 
                else if(ex is MessageNotFoundException)
                {
                    response.StatusCode = StatusCode.NotFound;
                }
            }
            return response;
        }
    }
}
