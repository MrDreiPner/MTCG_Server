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

namespace MTCG_Server.API.RouteCommands.Packages
{
    internal class AddPackageCommand : AuthenticatedRouteCommand
    {
        private readonly IPackageManager _packageManager;
        private readonly List<Card> _cards;

        public AddPackageCommand(IPackageManager userManager, User identity, List<CardPrototype> cards) : base(identity)
        {
            Console.WriteLine("Building Deck command aufgerufen");
            if (Identity.Username != null)
            {
                _packageManager = userManager;
                var protoCards = new List<CardPrototype>();
                protoCards = cards;
                _cards = new List<Card>();
                foreach (CardPrototype card in cards)
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
                    Console.WriteLine("Card passed: " + card.Name + " ID: " + card.Id + " Damage: " + card.Damage);
                }
            }
            else 
                throw new UserNotFoundException();
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                if (Identity.Username == "admin")
                {
                    Console.WriteLine("We are in as admins");
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
