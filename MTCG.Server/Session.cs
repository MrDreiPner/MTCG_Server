using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.BattleClasses;
using MTCG.DeckStack;
using MTCG.Models;
using MTCG.MTCG.Models;
using MTCG.MTCG.API.RouteCommands;
using System.Net;

namespace MTCG
{
    public class Session
    {
        //protected Trader trader;
        protected List<BattleLobby> battleLobbies;
        protected BattleUser myPlayer;
        private bool busyWaiting;

        //public Trader Trader { get { return trader; } set { trader = value; } }
        public List<BattleLobby> BattleLobbies { get { return battleLobbies; } set { battleLobbies = value; } }
        public Session(/*Trader trader,*/ List<BattleLobby> battleLobbies, BattleUser myPlayer)
        {
            //this.trader = trader;
            this.battleLobbies = battleLobbies;
            this.myPlayer = myPlayer;
        }

        ~Session() { } 

        public BattleResultsUser? SendToBattleLobby()
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
                    lobby.StartCombat();
                    busyWaiting = true;
                    while (busyWaiting)
                    {
                        //Console.WriteLine("Lobby state: " + lobby.LobbyDone);
                        if (lobby.LobbyDone)
                        {
                            //send response with lobby.Battlelog
                            busyWaiting = false;
                            BattleResults results = lobby.battleResults;
                            BattleResultsUser userResults = new BattleResultsUser(results._newEloPlayer2, results._battleLog, results._winsPlayer2, results._lossesPlayer2);
                            BattleLobby_Mutex.BattleMutex.WaitOne();
                            lobby.pickedUpBattleresults++;
                            BattleLobby_Mutex.BattleMutex.ReleaseMutex();
                            Console.WriteLine(myPlayer.Username + " done waiting!");
                            return userResults;
                        }
                        Thread.Sleep(1000);
                    }
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
                busyWaiting = true;
                while (busyWaiting)
                {
                    //Console.WriteLine("Lobby state: " + newLobby.LobbyDone);
                    if (newLobby.LobbyDone)
                    {
                        //send response with lobby.Battlelog
                        busyWaiting = false;
                        BattleResults results = newLobby.battleResults;
                        BattleResultsUser userResults = new BattleResultsUser(results._newEloPlayer1, results._battleLog, results._winsPlayer1, results._lossesPlayer1);
                        BattleLobby_Mutex.BattleMutex.WaitOne();
                        newLobby.pickedUpBattleresults++;
                        BattleLobby_Mutex.BattleMutex.ReleaseMutex();
                        Console.WriteLine(myPlayer.Username + " done waiting!");
                        return userResults;
                    }
                    Thread.Sleep(1000);
                }
            }
            return null;
        }

    }
}
