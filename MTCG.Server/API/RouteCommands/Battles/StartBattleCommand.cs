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
using MTCG_Server.BattleClasses;

namespace MTCG_Server.API.RouteCommands.Battles
{
    internal class StartBattleCommand : AuthenticatedRouteCommand
    {
        private readonly IBattleManager _battleManager;
        private List<BattleLobby> _battleLobbyList;

        public StartBattleCommand(IBattleManager battleManager, User identity, List<BattleLobby> battleLobbies) : base(identity)
        {
            if (Identity.Username != null)
            {
                Console.WriteLine("Show Start Battle");
                _battleManager = battleManager;
                _battleLobbyList = battleLobbies;
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                string? battlelog = _battleManager.StartBattle(Identity.Username, _battleLobbyList);
                response.StatusCode = StatusCode.Ok;
                response.Payload = battlelog;
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if(ex is MessageNotFoundException)
                    response.StatusCode = StatusCode.NoContent;
                    response.Payload = "No Deck found for "+ Identity.Username + "\n";

            }
            return response;
        }
    }
}
