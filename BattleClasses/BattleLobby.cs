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
        private User player1;
        private User player2;
        private string? player1Name;
        private string? player2Name;
        private Deck? player1Deck;
        private Deck? player2Deck;
        private string? _battleLog;

        public string BattleLog { get { return _battleLog; } }
        public BattleLobby(ref User player1, ref User player2)
        {
            this.player1 = player1;
            this.player2 = player2;
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
                if ((player1Deck.Size == 0 || player2Deck.Size == 0) || roundCounter > 100)
                {
                    break;
                }
                _battleLog += "!!============ TURN " + roundCounter + " STARTED ============!!\n";
                Card battleCardP1 = DrawCard(player1Deck);
                Console.WriteLine("Randomly drawn card is: "+ battleCardP1.Name);
                Card battleCardP2 = DrawCard(player2Deck);
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
            int winner;
            if ((player1Deck.Size > player2Deck.Size) && roundCounter <= 100)
                winner = 1;
            else if ((player1Deck.Size < player2Deck.Size) && roundCounter <= 100)
                winner = 2;
            else
                winner = 3;

            CaclulateNewELO(winner);
            CleanUp();
            return 0;
        }
        private void CaclulateNewELO(int winner)
        {
            double diff = Math.Abs((double)player1.Elo - (double)player2.Elo)/100;
            if (diff < 0.5)
                diff = 0.5;
            else if (diff > 5 )
                diff = 5;
            double points = 10 * diff;
            if(winner == 1)
            {
                if (player1.Elo > player2.Elo + 50)
                {
                    player1.Elo = player1.Elo + (int)points / 2;
                    player2.Elo = player2.Elo - (int)points / 2;
                }
                else
                {
                    player1.Elo = player1.Elo + (int)points;
                    player2.Elo = player2.Elo - (int)points;
                }
            }
            else if(winner == 2)
            {
                if(player1.Elo + 50 < player2.Elo)
                {
                    player2.Elo = player2.Elo + (int)points / 2;
                    player1.Elo = player1.Elo - (int)points / 2;
                }
                else
                {
                    player2.Elo = player2.Elo + (int)points;
                    player1.Elo = player1.Elo - (int)points;
                }
            }
        }

        private Card DrawCard(Deck Deck)
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

        private void CleanUp()
        {
            while (player1Deck.Size != 4 && player2Deck.Size != 4)
            {
                SwapCards(player1Deck, player2Deck);
                SwapCards(player2Deck, player1Deck);
            }
        }

        private void SwapCards(Deck Deck1, Deck Deck2)
        {
            Card? tmpCard = null;
            int counter = 0;
            bool foundOwner = false;
            foreach (Card card in Deck1.DeckCards)
            {
                counter++;
                if (card.OwnerID != Deck1.OwnerID)
                {
                    tmpCard = card;
                    foundOwner = true;
                    break;
                }
            }
            if (foundOwner)
            {
                Deck2.AddCard(tmpCard);
                Deck1.RemoveCard(counter);
            }
        }
    }
}
