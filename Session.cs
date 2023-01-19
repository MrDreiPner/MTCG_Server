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
        protected Trader trader;
        protected List<BattleLobby> battleLobbies;
        protected User myPlayer;
        private bool busyWaiting;

        public Trader Trader { get { return trader; } set { trader = value; } }
        public List<BattleLobby> BattleLobbies { get { return battleLobbies; } set { battleLobbies = value; } }
        public Session(Trader trader, List<BattleLobby> battleLobbies, User myPlayer)
        {
            this.trader = trader;
            this.battleLobbies = battleLobbies;
            this.myPlayer = myPlayer;
        }

        ~Session() { } 
        public void RunSession()
        {
            SendToBattleLobby();
        }

        public void SendToBattleLobby()
        {
            bool foundLobby = false;
            BattleLobby_Mutex.BattleMutex.WaitOne();
            foreach (BattleLobby lobby in BattleLobbies)
            {
                if (lobby.PlayerCount == 1)
                {
                    Console.WriteLine(myPlayer.Username + " found open Lobby!");
                    lobby.AddPlayer2(myPlayer);
                    BattleLobby_Mutex.BattleMutex.ReleaseMutex();
                    Thread.Sleep(1000);
                    lobby.StartCombat();
                    busyWaiting = true;
                    while (busyWaiting)
                    {
                        Console.WriteLine("Lobby state: " + lobby.LobbyDone);
                        if (lobby.LobbyDone)
                        {
                            //send response with lobby.Battlelog
                            busyWaiting = false;
                        }
                        Thread.Sleep(1000);
                    }
                    //BattleLobbies.Remove(lobby);
                    Console.WriteLine(myPlayer.Username + " done waiting!");
                    foundLobby = true;
                    break;
                }
            }
            if (!foundLobby)
            {
                Console.WriteLine(myPlayer.Username + " created a new Lobby");
                BattleLobby newLobby = new BattleLobby();
                newLobby.AddPlayer1 (myPlayer);
                //BattleLobby_Mutex.BattleMutex.WaitOne();
                BattleLobbies.Add(newLobby);
                BattleLobby_Mutex.BattleMutex.ReleaseMutex();
                Console.WriteLine(myPlayer.Username + " waiting in Lobby");
                busyWaiting = true;
                while (busyWaiting)
                {
                    Console.WriteLine("Lobby state: " + newLobby.LobbyDone);
                    if (newLobby.LobbyDone)
                    {
                        //send response with lobby.Battlelog
                        busyWaiting = false;
                    }
                    Thread.Sleep(1000);
                }
                Console.WriteLine(myPlayer.Username + " done waiting!");
            }
            //player1.Deck.PrintDeck();
            //player2.Deck.PrintDeck();
        }

    }
}
