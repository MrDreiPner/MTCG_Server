using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.BattleClasses;
using MTCG_Server.DeckStack;

namespace MTCG_Server
{
    internal class Server
    {
        protected List<BattleLobby> battleLobbies;
        protected List<Thread> sessionthreads;
        private bool abortRequest;

        public List<BattleLobby> BattleLobbies { get { return battleLobbies; } set { battleLobbies = value; } }
        public List<Thread> SessionThreads { get { return sessionthreads; } set { sessionthreads = value; } }
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
            sessionthreads = new List<Thread>();
            Console.WriteLine("Server is constructed");
        }

        public void RunServer()
        {
            /*while (!abortRequest)
            {

            }*/
            //Thread newThread = new Thread(new ParameterizedThreadStart(lobby.AddPlayer));
            //newThread.Start(player2);
            //SessionThreads.Add(newThread);
            //Thread newThread = new Thread(new ParameterizedThreadStart(lobby.AddPlayer));
            //newThread.Start(ref player2);
            User player1 = new User(20, "Maruice", "12345", 1000, 1);
            User player2 = new User(53, "Damacool", "dada", 800, 2);
            Session newSessionPlayer1 = new Session(this.battleLobbies, player1);
            Session newSessionPlayer2 = new Session(this.battleLobbies, player2);
            Thread newThread1 = new Thread(new ThreadStart(newSessionPlayer1.RunSession));
            newThread1.Start();
            SessionThreads.Add(newThread1);
            Thread.Sleep(1000);
            Thread newThread2 = new Thread(new ThreadStart(newSessionPlayer2.RunSession));
            newThread2.Start();
            SessionThreads.Add(newThread2);
            foreach (Thread thread in sessionthreads)
            {
                thread.Join();
            }//*/
        }
        

        ~Server()
        {
            Console.WriteLine("Server destructor has been called");
        }
    }
}
