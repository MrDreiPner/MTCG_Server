using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.BattleClasses;
using MTCG_Server.DeckStack;
using MTCG_Server.Models;

namespace MTCG_Server
{
    internal class Server
    {
        protected List<BattleLobby> battleLobbies;
        protected List<Thread> threadList;
        private bool abortRequest;

        public List<BattleLobby> BattleLobbies { get { return battleLobbies; } set { battleLobbies = value; } }
        public List<Thread> ThreadList { get { return threadList; } set { threadList = value; } }

        public Trader trader;
        public enum ElementID
        {
            normal = 1,
            water = 2,
            fire = 3
        }
        public Server()
        {
            abortRequest = false;
            battleLobbies = new List<BattleLobby>();
            threadList = new List<Thread>();
            trader = new Trader();
            Console.WriteLine("Server is constructed");
        }

        public void RunServer()
        {
            /*while (!abortRequest)
            {

            }*/
            //Start Trader
            Thread traderThread = new Thread(new ThreadStart(trader.StartTrader));
            traderThread.Start();
            ThreadList.Add(traderThread);
            //Start mock battle
            /*for(int i = 0; i < 100; i++)
            {
                BattleUser player1 = new BattleUser("Test"+1, "Maruice"+i, 1000 + i * 10);
                Session newSessionPlayer1 = new Session(this.trader, this.battleLobbies, player1);
                Thread newThread1 = new Thread(new ThreadStart(newSessionPlayer1.RunSession));
                newThread1.Start();
                ThreadList.Add(newThread1);
            }*/
            int count = 0;
            Thread.Sleep(1000);
            trader.AbortTrader();
            foreach (Thread thread in ThreadList)
            {
                count++;
                thread.Join();
                Console.WriteLine(count.ToString());
            }
            Console.WriteLine("Server shut down ");
        }
        

        ~Server()
        {
            Console.WriteLine("Server destructor has been called");
        }
    }
}
