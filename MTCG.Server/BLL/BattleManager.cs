using MTCG.MTCG.Models;
using MTCG.MTCG.BLL;
using MTCG.MTCG.DAL;
using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using MTCG.Models;
using MTCG;
using MTCG.BattleClasses;

namespace MTCG.MTCG.BLL
{
    internal class BattleManager : IBattleManager
    {
        private readonly IBattleDao _battleDao;

        public BattleManager(IBattleDao battleDao)
        {
            _battleDao = battleDao;
        }

        public UserStats ShowUserStats(string username)
        {
            return _battleDao.ShowUserStats(username);
        }

        public List<UserStats> ShowScoreboard()
        {
            return _battleDao.ShowScoreboard();
        }

        public string? StartBattle(string username, List<BattleLobby> battleLobbies)
        {
            BattleUser player;
            player = _battleDao.GetBattleUser(username);
            Session session = new Session(battleLobbies, player);
            BattleResultsUser? resultUser = session.SendToBattleLobby();
            _battleDao.UpdateBattleStats(username, resultUser);
            string? battleLog = resultUser._battleLog;

            return battleLog;
        }
    }
}
