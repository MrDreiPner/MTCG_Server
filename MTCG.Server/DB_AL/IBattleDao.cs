using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.Models;

namespace MTCG.MTCG.DAL
{
    internal interface IBattleDao
    {
        public UserStats ShowUserStats(string username);
        public List<UserStats> ShowScoreboard();
        public BattleUser GetBattleUser(string username);
        public void UpdateBattleStats(string username, BattleResultsUser resultUser);
    }
}
