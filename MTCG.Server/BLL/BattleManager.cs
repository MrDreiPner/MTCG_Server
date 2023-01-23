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

namespace SWE1.MTCG.BLL
{
    internal class BattleManager : IBattleManager
    {
        private readonly IBattleDao _battleDao;

        public BattleManager(IBattleDao battleDao)
        {
            _battleDao = battleDao;
        }

    }
}
