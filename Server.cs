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
            User player1 = new User(20,"Maruice","12345", 423, 1);
            User player2 = new User(53, "Damacool", "dada", 577, 2);
            buildLobby(ref player1, ref player2);
        }

        public void buildLobby(ref User player1, ref User player2)
        {
            BattleLobby newLobby = new BattleLobby(player1, player2);
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
