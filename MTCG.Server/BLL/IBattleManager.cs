using MTCG_Server.CardTypes;
using MTCG_Server.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.BattleClasses;

namespace MTCG_Server.MTCG.BLL
{
    internal interface IBattleManager
    {
        public UserStats ShowUserStats(string username);
        public List<UserStats> ShowScoreboard();
        public string? StartBattle(string username, List<BattleLobby> battleLobbies);
    }
}
