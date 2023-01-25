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

namespace MTCG.API.RouteCommands.Battles
{
    internal class ShowUserStatsCommand : AuthenticatedRouteCommand
    {
        private readonly IBattleManager _battleManager;

        public ShowUserStatsCommand(IBattleManager battleManager, User identity) : base(identity)
        {
            if (Identity.Username != null)
            {
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
