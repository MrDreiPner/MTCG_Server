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
        public enum ElementID
        {
            Normal = 1,
            Water = 2,
            Fire = 3
        }
        public Server()
        {
            Console.WriteLine("Server is constructed");
            runServer();
        }

        public void runServer()
        {
            User player1 = new User(20,"Maruice","12345", 850, 1);
            User player2 = new User(53, "Damacool", "dada", 1000, 2);
            buildLobby(ref player1, ref player2);
            Console.WriteLine("New ELO:\n"+player1.Username+": "+player1.Elo+"\n"+player2.Username+": "+player2.Elo);
        }

        public void buildLobby(ref User player1, ref User player2)
        {
            BattleLobby newLobby = new BattleLobby(ref player1, ref player2);
            newLobby.StartCombat();
            player1.Deck.PrintDeck();
            player2.Deck.PrintDeck();
        }

        ~Server()
        {
            Console.WriteLine("Server destructor has been called");
        }
    }
}
