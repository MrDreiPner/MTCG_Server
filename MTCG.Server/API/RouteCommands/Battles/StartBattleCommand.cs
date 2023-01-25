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
using MTCG.BattleClasses;

namespace MTCG.API.RouteCommands.Battles
{
    internal class StartBattleCommand : AuthenticatedRouteCommand
    {
        private readonly IBattleManager _battleManager;
        private List<BattleLobby> _battleLobbyList;

        public StartBattleCommand(IBattleManager battleManager, User identity, List<BattleLobby> battleLobbies) : base(identity)
        {
            if (Identity.Username != null)
            {
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
                {
                    response.StatusCode = StatusCode.NoContent;
                    response.Payload = "No Deck found for " + Identity.Username + "\n";

                }
                else if(ex is DataAccessFailedException) 
                    response.StatusCode = StatusCode.Conflict;

            }
            return response;
        }
    }
}
