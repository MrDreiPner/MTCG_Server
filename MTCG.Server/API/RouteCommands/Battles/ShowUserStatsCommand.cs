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

namespace MTCG_Server.API.RouteCommands.Battles
{
    internal class ShowUserStatsCommand : AuthenticatedRouteCommand
    {
        private readonly IBattleManager _battleManager;

        public ShowUserStatsCommand(IBattleManager battleManager, User identity) : base(identity)
        {
            if (Identity.Username != null)
            {
                Console.WriteLine("Show User Stats");
                _battleManager = battleManager;
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                UserStats userStats;
                userStats = _battleManager.ShowUserStats(Identity.Username);
                response.StatusCode = StatusCode.Ok;
                response.Payload = JsonConvert.SerializeObject(userStats, Formatting.Indented);
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
            }
            return response;
        }
    }
}
