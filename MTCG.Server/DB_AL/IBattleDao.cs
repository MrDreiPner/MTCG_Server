using MTCG_Server.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.Models;

namespace MTCG_Server.MTCG.DAL
{
    internal interface IBattleDao
    {
        public UserStats ShowUserStats(string username);
        public List<UserStats> ShowScoreboard();
        public BattleUser GetBattleUser(string username);
        public void UpdateBattleStats(BattleResultsUser resultUser);
    }
}
