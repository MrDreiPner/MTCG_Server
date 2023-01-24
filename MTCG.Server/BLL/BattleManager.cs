using SWE1.MTCG.Models;
using SWE1.MTCG.BLL;
using SWE1.MTCG.DAL;
using SWE1.MTCG.Models;
using MTCG_Server.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using MTCG_Server.Models;
using MTCG_Server;

namespace SWE1.MTCG.BLL
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

        public BattleResultsUser StartBattle(string username)
        {
            try
            {
                BattleResultsUser userResults;
                BattleUser player;
                player = _battleDao.StartBattle(username);
                Session session = new Session(battleLobbies, player);
                

                return userResults;
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
