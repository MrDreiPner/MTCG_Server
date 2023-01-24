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
using Microsoft.VisualBasic;
using MTCG_Server.BattleClasses;

namespace MTCG_Server.API.RouteCommands.Battles
{
    internal class StartBattleCommand : AuthenticatedRouteCommand
    {
        private readonly IBattleManager _battleManager;

        public StartBattleCommand(IBattleManager battleManager, User identity, List<BattleLobby> _battleLobbies) : base(identity)
        {
            if (Identity.Username != null)
            {
                Console.WriteLine("Show Start Battle");
                _battleManager = battleManager;
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                BattleResultsUser result = _battleManager.StartBattle(Identity.Username);
                response.StatusCode = StatusCode.Ok;
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if(ex is MessageNotFoundException)
                    response.StatusCode = StatusCode.NotFound;
                    response.Payload = "No Deck found for "+ Identity.Username;

            }
            return response;
        }
    }
}
