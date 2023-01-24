using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.Models
{
    public class BattleResultsUser
    {
        public int _newElo { get; set; }
        public string? _battleLog { get; set; }
        public int _wins { get; set; }
        public int _losses { get; set; }

        public BattleResultsUser(int newElo, string battleLog, int wins, int losses) { 
            _newElo= newElo;
            _battleLog= battleLog;
            _wins= wins;
            _losses= losses;
        }
    }
}
