using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.CardTypes;
using MTCG_Server.DeckStack;

namespace MTCG_Server.BattleClasses
{
    internal class BattleLobby
    {
        private static Random rand = new Random();
        private string? player1Name;
        private string? player2Name;
        private Card? battleCardP1;
        private Card? battleCardP2;
        private Deck? player1Deck;
        private Deck? player2Deck;
        private string? _battleLog;

        public string BattleLog { get { return _battleLog; } }
        public BattleLobby(User player1, User player2)
        {
            player1Name = player1.Username;
            player2Name = player2.Username;
            player1Deck = player1.Deck;
            player2Deck = player2.Deck;
            player1Deck.PrintDeck();
            player2Deck.PrintDeck();
            _battleLog = "";

        }

        public int StartCombat()
        {
            int result;
            int roundCounter = 0;
            while (true)
            {
                result = 0;
                roundCounter++;
                if ((player1Deck.Size == 0 || player2Deck.Size == 0) || roundCounter == 100)
                {
                    break;
                }
                _battleLog += "!!============ TURN " + roundCounter + " STARTED ============!!\n";
                battleCardP1 = DrawCard(ref player1Deck);
                Console.WriteLine("Randomly drawn card is: "+ battleCardP1.Name);
                battleCardP2 = DrawCard(ref player2Deck);
                Console.WriteLine("Randomly drawn card is: " + battleCardP2.Name);
                BattleLogic newRound = new BattleLogic(ref battleCardP1, ref battleCardP2, player1Name, player2Name);
                result = newRound.Fight(ref _battleLog, roundCounter);
                if (result == 1)
                {
                    player1Deck.AddCard(battleCardP2);
                    int counter = 0;
                    foreach (Card card in player2Deck.DeckCards)
                    {
                        counter++;
                        if (card.GuID == battleCardP2.GuID)
                        {
                            break;
                        }
                    }
                    player2Deck.RemoveCard(counter);
                    player1Deck.PrintDeck();
                    player2Deck.PrintDeck();
                }
                else if(result == 2)
                {
                    player2Deck.AddCard(battleCardP1);
                    int counter = 0;
                    foreach (Card card in player1Deck.DeckCards)
                    {
                        counter++;
                        if (card.GuID == battleCardP1.GuID)
                        {
                            break;
                        }
                    }
                    player1Deck.RemoveCard(counter);
                    player1Deck.PrintDeck();
                    player2Deck.PrintDeck();
                }
                else if(result == 0)
                {
                    throw new Exception("Missed some cases in BattleLogic!");
                }
                newRound = null;
            }
            Console.WriteLine(_battleLog);
            CleanUp(player1Deck, player2Deck);
            return 0;
        }
        private Card DrawCard(ref Deck Deck)
        {
            int max = Deck.Size;
            int pick = rand.Next(1, max);
            try
            {
                pick--;
                if (pick < 0)
                    throw new Exception("pick out of range");
                Card pickedCard = Deck.DeckCards.ElementAt(pick);
                return pickedCard;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        private void CleanUp(Deck player1Deck, Deck player2Deck)
        {
            Card? tmpCard = null;
            while (player1Deck.Size != 4 && player2Deck.Size != 4)
            {
                int counter = 0;
                bool foundOwner = false;
                foreach (Card card in player1Deck.DeckCards)
                {
                    counter++;
                    if(card.OwnerID != player1Deck.OwnerID)
                    {
                        tmpCard = card;
                        foundOwner = true;
                        break;
                    }
                }
                if (foundOwner)
                {
                    player2Deck.AddCard(tmpCard);
                    player1Deck.RemoveCard(counter);
                }
                counter = 0;
                foundOwner = false;
                foreach (Card card in player2Deck.DeckCards)
                {
                    counter++;
                    if (card.OwnerID != player2Deck.OwnerID && card.OwnerID == player1Deck.OwnerID)
                    {
                        tmpCard = card;
                        foundOwner = true;
                        break;
                    }
                }
                if (foundOwner)
                {
                    player1Deck.AddCard(tmpCard);
                    player2Deck.RemoveCard(counter);
                }
            }
        }
    }
}
