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
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace MTCG.API.RouteCommands.Packages
{
    internal class BuyPackageCommand : AuthenticatedRouteCommand
    {
        private readonly IPackageManager _packageManager;

        public BuyPackageCommand(IPackageManager packageManager, User identity) : base(identity)
        {
            if (Identity.Username != null)
                _packageManager = packageManager;
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                Package boughtPackage = _packageManager.BuyPackage(Identity.Username);
                Console.WriteLine("Purchase successful");
                if(boughtPackage != null)
                {
                    //Package boughtPackage = _packageManager.BuyPackage(Identity.Username);
                    List<Card> packageCards = new List<Card>();
                    packageCards = boughtPackage._cards;
                    response.StatusCode = StatusCode.Ok;
                    response.Payload = JsonConvert.SerializeObject(packageCards, Formatting.Indented);
                }
                else
                {
                    response.StatusCode = StatusCode.NotFound;
                }

            }
            catch (Exception ex)
            {
                if (ex is MessageNotFoundException)
                    response.StatusCode = StatusCode.NotFound;
                else if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if (ex is NotEnoughMoneyException)
                    response.StatusCode = StatusCode.Forbidden;
            }
            return response;
        }
    }
}
