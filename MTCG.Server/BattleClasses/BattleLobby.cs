using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.CardTypes;
using MTCG.DeckStack;
using MTCG.Models;
using MTCG.MTCG.Models;

namespace MTCG.BattleClasses
{
    public class BattleLobby
    {
        private static Random rand = new Random();
        private BattleUser player1;
        private BattleUser player2;
        private string? player1Name;
        private string? player2Name;
        private Deck? player1Deck;
        private Deck? player2Deck;
        private string? _battleLog;
        public BattleResults battleResults;
        public int pickedUpBattleresults;

        private bool lobbyDone;
        private int _checkLobbyDone;

        private int _playerCount;

        public bool LobbyDone { get { return lobbyDone; } }
        public int PlayerCount { get{ return _playerCount;} set { _playerCount = value; } }
        public string BattleLog { get { return _battleLog; } }
        public int CheckLobbyDone { get { return _checkLobbyDone; } set { _checkLobbyDone = value; } }
        public BattleLobby()
        {
            _playerCount = 0;
            _battleLog = "";
            lobbyDone= false;
            _checkLobbyDone = 0;
            pickedUpBattleresults = 0;
        }

        public void AddPlayer1(object player1)
        {
            this.player1 = (BattleUser) player1;
            player1Name = this.player1.Username;
            player1Deck = this.player1.Deck;
            _playerCount++;
        }
        public void AddPlayer2(object player2)
        {
            _playerCount++;
            this.player2 = (BattleUser) player2;
            player2Name = this.player2.Username;
            player2Deck = this.player2.Deck;
        }
        public void StartCombat()
        {
            int result;
            int roundCounter = 0;
            //Battle Starts
            while (true)
            {
                result = 0;
                roundCounter++;
                if ((player1Deck.Size == 0 || player2Deck.Size == 0) || roundCounter > 100)
                {
                    break;
                }
                _battleLog += "!!============ TURN " + roundCounter + " STARTED ============!!\n";
                //Both players draw a card
                Card battleCardP1 = DrawCard(player1Deck);
                Card battleCardP2 = DrawCard(player2Deck);
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
                }
                else if(result == 0)
                {
                    throw new Exception("Missed some cases in BattleLogic!");
                }
                newRound = null;
            }
            int winner;
            if ((player1Deck.Size > player2Deck.Size) && roundCounter <= 100)
            {
                _battleLog = "!! " + player1.Username + " HAS WON !!\n" + _battleLog;
                winner = 1;
            }
            else if ((player1Deck.Size < player2Deck.Size) && roundCounter <= 100)
            {
                _battleLog = "!! " + player2.Username + " HAS WON !!\n" + _battleLog;
                winner = 2;
            }
            else
            {
                _battleLog = "!! THIS MATCH IS A DRAW !!\n" + _battleLog;
                winner = 3;
            }

            if(winner != 3)
                CaclulateNewELO(winner);
            battleResults = new BattleResults(_battleLog, player1.Elo, player1.Wins, player1.Losses, player2.Elo, player2.Wins, player2.Losses);
            lobbyDone = true;
        }
        public void CaclulateNewELO(int winner)
        {

            double diff = Math.Abs(((double)player1.Elo - (double)player2.Elo)/100);
            if (diff < 0.5)
                diff = 0.5;
            else if (diff > 5 )
                diff = 5;
            double points = 10 * diff;
            if(winner == 1)
            {
                if (player1.Elo > player2.Elo)
                {
                    player1.Elo = player1.Elo + (int)points / 2;
                    if ((int)points / 2 > 10)
                        points = 10;
                    else
                        points = (int)points / 2;
                    player2.Elo = player2.Elo - (int) points;
                }
                else
                {
                    player1.Elo = player1.Elo + (int)points;
                    player2.Elo = player2.Elo - (int)points;
                }
                if(player2.Elo < 0)
                    player2.Elo = 0;
                player1.Wins++;
                player2.Losses++;
            }
            else if(winner == 2)
            {
                if(player1.Elo < player2.Elo)
                {
                    player2.Elo = player2.Elo + (int)points / 2;
                    if((int) points / 2 > 10)
                        points = 10;
                    else
                        points = (int)points / 2;
                    player2.Elo = player2.Elo - (int)points;
                }
                else
                {
                    player2.Elo = player2.Elo + (int)points;
                    player1.Elo = player1.Elo - (int)points;
                }
                if (player1.Elo < 0)
                    player1.Elo = 0;
                player2.Wins++;
                player1.Losses++;
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
    }
}
