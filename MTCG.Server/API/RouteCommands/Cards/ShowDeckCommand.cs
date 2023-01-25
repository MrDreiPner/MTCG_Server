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

namespace MTCG.API.RouteCommands.Cards
{
    internal class ShowDeckCommand : AuthenticatedRouteCommand
    {
        private readonly ICardManager _cardManager;
        private readonly int _variant;

        public ShowDeckCommand(ICardManager cardManager, User identity, int variant) : base(identity)
        {
            if (Identity.Username != null)
            {
                _cardManager = cardManager;
                _variant= variant;
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                List<Card>? cards = null;
                cards = _cardManager.ShowDeck(Identity.Username);
                response.StatusCode = StatusCode.Ok;
                if(_variant == 1)
                    response.Payload = JsonConvert.SerializeObject(cards, Formatting.Indented);
                else
                {
                    response.Payload = "!-- This is " + Identity.Username + "'s Deck --!";
                    foreach (Card card in cards)
                    {
                        response.Payload += "\n:============:";
                        response.Payload += "\n Card ID: " + card.GuID;
                        response.Payload += "\n Name: " + card.Name;
                        response.Payload += "\n Damage: " + card.Dmg;
                        response.Payload += "\n Element: " + card.ElementID;
                        response.Payload += "\n Type: " + card.Type;
                        response.Payload += "\n:============:\n";
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex is MessageNotFoundException)
                    response.StatusCode = StatusCode.NoContent;
                else if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
            }
            return response;
        }
    }
}
