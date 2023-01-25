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

namespace MTCG.API.RouteCommands.Packages
{
    internal class AddPackageCommand : AuthenticatedRouteCommand
    {
        private readonly IPackageManager _packageManager;
        private readonly List<Card> _cards;

        public AddPackageCommand(IPackageManager packageManager, User identity, List<CardPrototype> cards) : base(identity)
        {
            if (Identity.Username != null)
            {
                _packageManager = packageManager;
                var protoCards = new List<CardPrototype>();
                protoCards = cards;
                _cards = new List<Card>();
                foreach (CardPrototype card in protoCards)
                {
                    Card newCard;
                    string cardname = card.Name;
                    if (cardname.Length < 5)
                    {
                        newCard = new Monster(card.Id, cardname, (int)Convert.ToDouble(card.Damage));
                    }
                    else
                    {
                        if (cardname.Substring(cardname.Length - 5) == "Spell")
                        {
                            newCard = new Spell(card.Id, cardname, (int)Convert.ToDouble(card.Damage));
                        }
                        else
                        {
                            newCard = new Monster(card.Id, cardname, (int)Convert.ToDouble(card.Damage));
                        }
                    }

                    _cards.Add(newCard);
                }
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                if (Identity.Username == "admin")
                {
                    Package newPackage = _packageManager.AddPackage(_cards);
                    response.StatusCode = StatusCode.Ok;
                    /*foreach(Card card in newPackage._cards)
                    {

                    }*/
                    
                }
                else
                {
                    response.StatusCode = StatusCode.Forbidden;
                }
            }
            catch (Exception ex)
            {
                if (ex is CardAlreadyExistsException)
                    response.StatusCode = StatusCode.Conflict;
                else if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
            }
            return response;
        }
    }
}
