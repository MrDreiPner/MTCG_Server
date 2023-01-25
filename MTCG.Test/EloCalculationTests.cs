
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace MTCG.Test
{
    public class EloCalculationTests
    {

        //Result:
        //  1 = Tester 1 won
        //  2 = Tester 2 won
        //  3 = Draw

        //Spell VS Spell --> Elements have an effect
        // 1 win
        [Test]
        public void WaterS_v_FireS()
        {
            //Setup
            CardTypes.Card card1 = new CardTypes.Spell("1", "WaterSpell", 100);
            CardTypes.Card card2 = new CardTypes.Spell("2", "FireSpell", 100);
            //Execute
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            //Assert
            if (result == 1)
                Assert.Pass();
            else
                Assert.Fail();
        }
        
    }
}