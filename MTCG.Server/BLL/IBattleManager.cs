using MTCG_Server.CardTypes;
using SWE1.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.BLL
{
    internal interface IBattleManager
    {
        public UserStats ShowUserStats(string username);
        public List<UserStats> ShowScoreboard();
        public BattleResultsUser StartBattle(string username);
    }
}
