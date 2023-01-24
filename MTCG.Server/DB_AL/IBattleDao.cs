using SWE1.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.Models;

namespace SWE1.MTCG.DAL
{
    internal interface IBattleDao
    {
        public UserStats ShowUserStats(string username);
        public List<UserStats> ShowScoreboard();
        public BattleUser StartBattle(string username);
    }
}
