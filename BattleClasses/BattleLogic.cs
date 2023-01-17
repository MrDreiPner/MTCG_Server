using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.CardTypes;

namespace MTCG_Server.BattleClasses
{
    internal class BattleLogic
    {
        private string player1Name;
        private string player2Name;
        private Card cardPlayer1;
        private Card cardPlayer2;
        public BattleLogic(ref Card player1, ref Card player2, string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            cardPlayer1 = player1;
            cardPlayer2 = player2;
        }

        public int Fight(ref string battleLog, int roundCounter)
        {
            if ((cardPlayer1 is Monster && cardPlayer2 is Monster) || (cardPlayer1.ElementID == cardPlayer2.ElementID))
            {
                if (cardPlayer1.Dmg > cardPlayer2.Dmg)
                {
                    battleLog += "Turn " + roundCounter + " was won by "+player1Name+ "! " + cardPlayer1.Type +" "+cardPlayer1.Name + " dealt "+ cardPlayer1.Dmg +" damage against " + cardPlayer2.Type + " " + cardPlayer2.Name + "'s " + cardPlayer2.Dmg+ " damage!\n";
                    return 1;
                }
                else if (cardPlayer1.Dmg < cardPlayer2.Dmg)
                {
                    battleLog += "Turn " + roundCounter + " was won by " + player2Name + "! " + cardPlayer2.Type + " " + cardPlayer2.Name + " dealt " + cardPlayer2.Dmg + " damage against " + cardPlayer1.Type + " " + cardPlayer1.Name + "'s " + cardPlayer1.Dmg + " damage!\n";
                    return 2;
                }
                else
                {
                    battleLog += "Turn " + roundCounter + " was a tie! " + cardPlayer1.Type + " " + cardPlayer1.Name + " and " + cardPlayer2.Type + " " + cardPlayer2.Name + " both dealt " + cardPlayer1.Dmg+" damage!\n";
                    return 3;
                }
            }
            else if((cardPlayer1.ElementID == 1 && cardPlayer2.ElementID == 2)
                    || (cardPlayer1.ElementID == 2 && cardPlayer2.ElementID == 3)
                    || (cardPlayer1.ElementID == 3 && cardPlayer2.ElementID == 1))
            {
                int CP1dmg = cardPlayer1.Dmg * 2;
                int CP2dmg = cardPlayer2.Dmg / 2;
                if (CP1dmg > CP2dmg)
                {
                    battleLog += "Turn " + roundCounter + " was won by " + player1Name + "! " + cardPlayer1.Type + " " + cardPlayer1.Name + " dealt " + CP1dmg + " damage against " + cardPlayer2.Type + " " + cardPlayer2.Name + "'s " + CP2dmg + " damage!\n";
                    return 1;
                }
                else if (CP1dmg < CP2dmg)
                {
                    battleLog += "Turn " + roundCounter + " was won by " + player2Name + "! " + cardPlayer2.Type + " " + cardPlayer2.Name + " dealt " + CP2dmg + " damage against " + cardPlayer1.Type + " " + cardPlayer1.Name + "'s " + CP1dmg + " damage!\n";
                    return 2;
                }
                else
                {
                    battleLog += "Turn " + roundCounter + " was a tie! " + cardPlayer1.Type + " " + cardPlayer1.Name + " and " + cardPlayer2.Type + " " + cardPlayer2.Name + ", both dealt " + CP2dmg + " damage!\n";
                    return 3;
                }
            }
            else if ((cardPlayer2.ElementID == 1 && cardPlayer1.ElementID == 2)
                    || (cardPlayer2.ElementID == 2 && cardPlayer1.ElementID == 3)
                    || (cardPlayer2.ElementID == 3 && cardPlayer1.ElementID == 1))
            {
                int CP2dmg = cardPlayer2.Dmg * 2;
                int CP1dmg = cardPlayer1.Dmg / 2;
                if (CP1dmg > CP2dmg)
                {
                    battleLog += "Turn " + roundCounter + " was won by " + player1Name + "! " + cardPlayer1.Type + " " + cardPlayer1.Name + " dealt " + CP1dmg + " damage against " + cardPlayer2.Type + " " + cardPlayer2.Name + "'s " + CP2dmg + " damage!\n";
                    return 1;
                }
                else if (CP1dmg < CP2dmg)
                {
                    battleLog += "Turn " + roundCounter + " was won by " + player2Name + "! " + cardPlayer2.Type + " " + cardPlayer2.Name + " dealt " + CP2dmg + " damage against " + cardPlayer1.Type + " " + cardPlayer1.Name + "'s " + CP1dmg + " damage!\n";
                    return 2;
                }
                else
                {
                    battleLog += "Turn " + roundCounter + " was a tie! " + cardPlayer1.Type + " " + cardPlayer1.Name + " and " + cardPlayer2.Type + " " + cardPlayer2.Name + ", both dealt " + CP2dmg + " damage!\n";
                    return 3;
                }
            }
            else return 0;
        }

        ~BattleLogic() 
        { 
            Console.WriteLine("BattleLogic destructor has been called"); 
        }
    }
}
