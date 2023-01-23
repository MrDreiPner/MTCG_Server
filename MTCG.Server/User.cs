using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.DeckStack;

namespace MTCG_Server
{
    internal class BattleUser
    {
        public string Uid { get { return _uid; } }
        public string? Username { get { return _username; } }
        public string? Password { get { return _password; } set { _password = value; } }
        public int Elo { get { return _elo; } set { _elo = value; } }
        public Deck Deck { get { return _deck; } set { _deck = value; } }
        public Stack Stack { get { return _stack; } set { _stack = value; } }

        private string _uid;
        private string? _username;
        private string? _password;
        private int _elo;
        private Deck? _deck;
        private Stack? _stack;

        public BattleUser(string uid, string username, string password, int elo, int deckType)
        {
            _uid = uid;
            _username = username;
            _password = password;
            _elo = elo;
            _deck = new Deck(_uid, deckType);
        }
        ~BattleUser() { }
    }


}
