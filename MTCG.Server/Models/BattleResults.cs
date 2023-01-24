using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1.MTCG.Models
{
    public class BattleResults
    {

        public string _battleLog { get; set; }
        public int _newEloPlayer1 { get; set; }
        public int _winsPlayer1 { get; set; }
        public int _lossesPlayer1 { get; set; }
        public int _newEloPlayer2 { get; set; }
        public int _winsPlayer2 { get; set; }
        public int _lossesPlayer2 { get; set; }

        public BattleResults(string battlelog, int elo1, int wins1, int losses1, int elo2, int wins2, int losses2)
        {
            _battleLog = battlelog;
            _newEloPlayer1 = elo1;
            _winsPlayer1 = wins1;
            _lossesPlayer1 = losses1;
            _newEloPlayer2 = elo2;
            _winsPlayer2 = wins2;
            _lossesPlayer2 = losses2;
        }
    }
}
