
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace MTCG.Test
{
    public class EloCalculationTests
    {
        // change should be by unmodified min value of 5
        [Test]
        public void NoEloDifferenceT1win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 100;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(1);

            //Assert
            Assert.That(tester1.Elo, Is.GreaterThan(tester2.Elo));
            Assert.That(tester1.Elo, Is.EqualTo(105));
            Assert.That(tester2.Elo, Is.EqualTo(95));
        }
        // change should be by unmodified min value of 5
        [Test]
        public void NoEloDifferenceT2win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 100;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(2);

            //Assert
            Assert.That(tester2.Elo, Is.GreaterThan(tester1.Elo));
            Assert.That(tester2.Elo, Is.EqualTo(105));
            Assert.That(tester1.Elo, Is.EqualTo(95));
        }
        //Winner has higher/lower Elo
        [Test]
        public void SlightlyHigherEloT1Win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 110;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(1);

            //Assert
            Assert.That(tester1.Elo, Is.GreaterThan(tester2.Elo));
            Assert.That(tester1.Elo, Is.LessThan(115));
            Assert.That(tester2.Elo, Is.GreaterThan(95));
        }
        [Test]
        public void SlightlyLowerEloT2Win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 110;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(2);

            //Assert
            Assert.That(tester1.Elo, Is.EqualTo(105));
            Assert.That(tester2.Elo, Is.EqualTo(105));
        }

        [Test]
        public void MassivelyHigherT1Win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 1000;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(1);

            //Assert
            Assert.That(tester1.Elo, Is.GreaterThan(tester2.Elo));
            Assert.That(tester1.Elo, Is.LessThan(1050));
            Assert.That(tester2.Elo, Is.EqualTo(90));
        }

        [Test]
        public void MassivelyLowerT2Win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 1000;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(2);

            //Assert
            Assert.That(tester1.Elo, Is.EqualTo(950));
            Assert.That(tester2.Elo, Is.EqualTo(150));
        }
        [Test]
        public void LoserHasNear0Elo()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 100;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 1;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(1);

            //Assert
            Assert.That(tester1.Elo, Is.GreaterThan(100));
            Assert.That(tester2.Elo, Is.EqualTo(0));
        }
        [Test]
        public void WinnerHas0Elo()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 100;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 0;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(2);

            //Assert
            Assert.That(tester1.Elo, Is.EqualTo(90));
            Assert.That(tester2.Elo, Is.EqualTo(10));

        }
        //Scalalble Elo calculations --> lower elo not losing more than 5 points
        [Test]
        public void InScalableEloRangeT1Win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 350;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(1);

            //Assert
            Assert.That(tester1.Elo, Is.GreaterThan(350));
            Assert.That(tester1.Elo, Is.LessThan(375));
            Assert.That(tester2.Elo, Is.EqualTo(90));
        }
        [Test]
        public void InScalableEloRangeT2Win()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 350;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 100;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(2);

            //Assert
            Assert.That(tester1.Elo, Is.EqualTo(325));
            Assert.That(tester2.Elo, Is.EqualTo(125));
        }
        //Draw
        [Test]
        public void DrawNoChange()
        {
            //Setup
            BattleClasses.BattleLobby battleLobby = new BattleClasses.BattleLobby();
            Models.BattleUser tester1 = new Models.BattleUser();
            tester1.Elo = 500;
            Models.BattleUser tester2 = new Models.BattleUser();
            tester2.Elo = 0;
            battleLobby.AddPlayer1(tester1);
            battleLobby.AddPlayer2(tester2);

            //Execute
            battleLobby.CaclulateNewELO(3);

            //Assert
            Assert.That(tester1.Elo, Is.EqualTo(500));
            Assert.That(tester2.Elo, Is.EqualTo(0));
        }
    }
}