
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace MTCG.Test
{
    public class BattleLogicTest
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
        // 2 win
        [Test]
        public void WaterS_v_NormalS()
        {
            CardTypes.Card card1 = new CardTypes.Spell("1", "WaterSpell", 100);
            CardTypes.Card card2 = new CardTypes.Spell("2", "StoneSpell", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 2)
                Assert.Pass();
            else
                Assert.Fail();
        }
        // draw
        [Test]
        public void WaterS_v_WaterS()
        {
            CardTypes.Card card1 = new CardTypes.Spell("1", "WaterSpell", 100);
            CardTypes.Card card2 = new CardTypes.Spell("2", "WaterSpell", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 3)
                Assert.Pass();
            else
                Assert.Fail();
        }
        //Monster VS Monster --> Elements have no effect
        // draw
        [Test]
        public void WaterM_v_FireM ()
        {
            CardTypes.Card card1 = new CardTypes.Monster("1", "WaterMonster", 100);
            CardTypes.Card card2 = new CardTypes.Monster("2", "FireMonster", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 3)
                Assert.Pass();
            else
                Assert.Fail();
        }
        // draw
        [Test]
        public void WaterM_v_NormalM()
        {
            CardTypes.Card card1 = new CardTypes.Monster("1", "WaterMonster", 100);
            CardTypes.Card card2 = new CardTypes.Monster("2", "StoneMonster", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 3)
                Assert.Pass();
            else
                Assert.Fail();
        }
        // draw
        [Test]
        public void WaterM_v_WaterM()
        {
            CardTypes.Card card1 = new CardTypes.Monster("1", "WaterMonster", 100);
            CardTypes.Card card2 = new CardTypes.Monster("2", "WaterMonster", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 3)
                Assert.Pass();
            else
                Assert.Fail();
        }
        //Monster VS Spell --> Elements have effect
        // 1 win
        [Test]
        public void WaterM_v_FireS()
        {
            CardTypes.Card card1 = new CardTypes.Monster("1", "WaterMonster", 100);
            CardTypes.Card card2 = new CardTypes.Spell("2", "FireSpell", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 1)
                Assert.Pass();
            else
                Assert.Fail();
        }
        // 2 win
        [Test]
        public void WaterM_v_NormalS()
        {
            CardTypes.Card card1 = new CardTypes.Monster("1", "WaterMonster", 100);
            CardTypes.Card card2 = new CardTypes.Spell("2", "StoneSpell", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 2)
                Assert.Pass();
            else
                Assert.Fail();
        }
        // draw
        [Test]
        public void WaterM_v_WaterS()
        {
            CardTypes.Card card1 = new CardTypes.Monster("1", "WaterMonster", 100);
            CardTypes.Card card2 = new CardTypes.Spell("2", "WaterSpell", 100);
            BattleClasses.BattleLogic logic = new BattleClasses.BattleLogic(ref card1, ref card2, "Tester1", "Tester2");
            string battlelog = "";
            int result = logic.Fight(ref battlelog, 0);
            if (result == 3)
                Assert.Pass();
            else
                Assert.Fail();
        }
    }
}