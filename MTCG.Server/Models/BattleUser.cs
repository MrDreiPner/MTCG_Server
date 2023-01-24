using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.DeckStack;

namespace MTCG_Server.Models
{
    internal class BattleUser
    {
        public string? Uid { get { return _uid; } set { _uid = value; } }
        public string? Username { get { return _username; } set { _username = value; } }
        public int Elo { get { return _elo; } set { _elo = value; } }
        public int Wins { get { return _wins; } set { _wins = value; } }
        public int Losses { get { return _losses; } set { _losses = value; } }
        public Deck Deck { get { return _deck; } set { _deck = value; } }

        private string _uid;
        private string? _username;
        private int _elo;
        private int _wins;
        private int _losses;
        private Deck? _deck;

        public BattleUser()
        {
        }
        ~BattleUser() { }
    }


}
