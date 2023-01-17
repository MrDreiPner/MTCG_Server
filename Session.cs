using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.BattleClasses;
using MTCG_Server.DeckStack;

namespace MTCG_Server
{
    internal class Session
    {
        protected List<BattleLobby> battleLobbies;
        protected User myPlayer;

        public List<BattleLobby> BattleLobbies { get { return battleLobbies; } set { battleLobbies = value; } }
        public Session(List<BattleLobby> battleLobbies, User myPlayer)
        {
            this.battleLobbies = battleLobbies;
            this.myPlayer = myPlayer;
        }
        public void RunSession()
        {
            SendToBattleLobby(ref myPlayer);
        }

        public void SendToBattleLobby(ref User myPlayer)
        {
            int counter = 0;
            foreach (BattleLobby lobby in BattleLobbies)
            {
                if (lobby.PlayerCount == 1)
                {
                    Console.WriteLine("Found open Lobby!");
                    lobby.AddPlayer2(myPlayer);
                    counter++;
                    break;
                }
                if(counter == BattleLobbies.Count())
                {
                    Console.WriteLine("Created new Lobby");
                    BattleLobby newLobby = new BattleLobby();
                    newLobby.AddPlayer1(myPlayer);
                    BattleLobbies.Add(newLobby);
                }
                counter++;
            }
            if (counter == 0)
            {
                Console.WriteLine("Created new Lobby");
                BattleLobby newLobby = new BattleLobby();
                newLobby.AddPlayer1(myPlayer);
                BattleLobbies.Add(newLobby);
            }
            //player1.Deck.PrintDeck();
            //player2.Deck.PrintDeck();
        }

    }
}
