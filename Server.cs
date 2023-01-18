using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.BattleClasses;
using MTCG_Server.DeckStack;

namespace MTCG_Server
{
    class BattleLobby_Mutex
    {
        public static Mutex BattleMutex= new Mutex();
    }
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
            Normal = 1,
            Water = 2,
            Fire = 3
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
            for(int i = 0; i < 100; i++)
            {
                User player1 = new User(20 + i, "Maruice"+i, "12345", 1000 + i * 10, 20 - 1, (i % 2) + 1);
                Session newSessionPlayer1 = new Session(this.trader, this.battleLobbies, player1);
                Thread newThread1 = new Thread(new ThreadStart(newSessionPlayer1.RunSession));
                newThread1.Start();
                ThreadList.Add(newThread1);
            }
            /*User player1 = new User(20, "Maruice", "12345", 1000, 20, 1);
            User player2 = new User(53, "Damacool", "dada", 800, 10, 2);
            Session newSessionPlayer1 = new Session(this.trader, this.battleLobbies, player1);
            Session newSessionPlayer2 = new Session(this.trader, this.battleLobbies, player2);
            Thread newThread1 = new Thread(new ThreadStart(newSessionPlayer1.RunSession));
            newThread1.Start();
            ThreadList.Add(newThread1);
            Thread newThread2 = new Thread(new ThreadStart(newSessionPlayer2.RunSession));
            newThread2.Start();
            ThreadList.Add(newThread2);
            User player3 = new User(78, "DooDoo", "12345", 500, 20, 1);
            User player4 = new User(123, "Bandooo", "dada", 900, 10, 2);
            Session newSessionPlayer3 = new Session(this.trader, this.battleLobbies, player3);
            Session newSessionPlayer4 = new Session(this.trader, this.battleLobbies, player4);
            Thread newThread3 = new Thread(new ThreadStart(newSessionPlayer3.RunSession));
            newThread3.Start();
            ThreadList.Add(newThread3);
            Thread newThread4 = new Thread(new ThreadStart(newSessionPlayer4.RunSession));
            newThread4.Start();
            ThreadList.Add(newThread4);*/
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
