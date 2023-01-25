
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using MTCG.MTCG.BLL;
using MTCG.MTCG.Models;

namespace MTCG.Test
{
    public class CreationTests
    {

        //Creating cards correctly dependent on Name input

        [Test]
        public void CreateFireCards()
        {
            //Setup + Execute
            CardTypes.Card card1 = new CardTypes.Monster("1", "FireOrk", 100);
            CardTypes.ElementID result1 = card1.ElementID;
            CardTypes.Card card2 = new CardTypes.Monster("2", "Dragon", 100);
            CardTypes.ElementID result2 = card2.ElementID;
            CardTypes.Card card3 = new CardTypes.Spell("2", "FireSpell", 100);
            CardTypes.ElementID result3 = card3.ElementID;
            CardTypes.Card card4 = new CardTypes.Monster("2", "Ork", 100);
            CardTypes.ElementID result4 = card4.ElementID;
            //Assert
            if ((result1 == CardTypes.ElementID.fire && result2 == CardTypes.ElementID.fire && result3 == CardTypes.ElementID.fire) && result4 != CardTypes.ElementID.fire)
                Assert.Pass();
            else
                Assert.Fail();
        }
        [Test]
        public void CreateWaterCards()
        {
            //Setup + Execute
            CardTypes.Card card1 = new CardTypes.Monster("1", "WaterOrk", 100);
            CardTypes.ElementID result1 = card1.ElementID;
            CardTypes.Card card2 = new CardTypes.Monster("2", "Kraken", 100);
            CardTypes.ElementID result2 = card2.ElementID;
            CardTypes.Card card3 = new CardTypes.Spell("2", "WaterSpell", 100);
            CardTypes.ElementID result3 = card3.ElementID;
            CardTypes.Card card4 = new CardTypes.Monster("2", "Ork", 100);
            CardTypes.ElementID result4 = card4.ElementID;

            //Assert
            if ((result1 == CardTypes.ElementID.water && result2 == CardTypes.ElementID.water && result3 == CardTypes.ElementID.water) && result4 != CardTypes.ElementID.water)
                Assert.Pass();
            else
                Assert.Fail();
        }
        [Test]
        public void CreateNormalCards()
        {
            //Setup + Execute
            CardTypes.Card card1 = new CardTypes.Monster("1", "FireOrk", 100);
            CardTypes.ElementID result1 = card1.ElementID;
            CardTypes.Card card2 = new CardTypes.Monster("2", "Dragon", 100);
            CardTypes.ElementID result2 = card2.ElementID;
            CardTypes.Card card3 = new CardTypes.Spell("2", "FireSpell", 100);
            CardTypes.ElementID result3 = card3.ElementID;
            CardTypes.Card card4 = new CardTypes.Monster("2", "Ork", 100);
            CardTypes.ElementID result4 = card4.ElementID;
            //Assert
            if ((result1 == CardTypes.ElementID.fire && result2 == CardTypes.ElementID.fire && result3 == CardTypes.ElementID.fire) && result4 != CardTypes.ElementID.fire)
                Assert.Pass();
            else
                Assert.Fail();

        }

        // Package creation
        [Test]
        public void CreatePackage()
        {
            //Setup
            var packageDaoMock = new Mock<MTCG.DAL.IPackageDao>();
            var packageManager = new PackageManager(packageDaoMock.Object);
            List<CardTypes.Card>? packContent = null;
            Package emptyPack = new Package(packContent, 0);
            packContent = new List<CardTypes.Card>();
            packContent.Add(new CardTypes.Monster("1", "WaterOrk", 100));
            packContent.Add(new CardTypes.Monster("2", "Kraken", 100));
            packContent.Add(new CardTypes.Spell("2", "WaterSpell", 100));
            packContent.Add(new CardTypes.Monster("2", "Ork", 100));
            packContent.Add(new CardTypes.Monster("2", "Dragon", 100));
            Package expectedPack = new Package(packContent, 0);

            //Execute
            packageDaoMock.Setup(x => x.AddPackage(packContent)).Returns(new Package(packContent, 0));
            Package result = packageManager.AddPackage(packContent);
            //Assert
            Assert.AreEqual(expectedPack._cards, result._cards);
            Assert.AreNotEqual(emptyPack._cards, result._cards);
        }

        //Deck creation
        [Test]
        public void CreateDeck()
        {
            List<CardTypes.Card> testCards = new List<CardTypes.Card>();
            testCards.Add(new CardTypes.Monster("1", "WaterOrk", 100));
            testCards.Add(new CardTypes.Monster("2", "Kraken", 100));
            testCards.Add(new CardTypes.Monster("2", "Ork", 100));
            testCards.Add(new CardTypes.Spell("2", "WaterSpell", 100));
            DeckStack.Deck testDeck = new DeckStack.Deck("tester", testCards);

            Assert.That(testDeck, Is.Not.Null);
            Assert.That(testDeck.Size, Is.EqualTo(4));
            Assert.That(testDeck.OwnerID, Is.EqualTo("tester"));
            Assert.That(testDeck.DeckCards, Is.EqualTo(testCards));
        }

        [Test]
        public void ShowDeck()
        {
            //Setup
            var cardDaoMock = new Mock<MTCG.DAL.ICardDao>();
            var cardManager = new CardManager(cardDaoMock.Object);
            string username = "tester";
            List<CardTypes.Card> expected = new List<CardTypes.Card>();
            expected.Add(new CardTypes.Monster("1", "WaterOrk", 100));
            expected.Add(new CardTypes.Monster("2", "Kraken", 100));
            expected.Add(new CardTypes.Monster("2", "Ork", 100));
            expected.Add(new CardTypes.Spell("2", "WaterSpell", 100));
            
            //Execute
            cardDaoMock.Setup(x => x.ShowDeck(username)).Returns(expected);
            List<CardTypes.Card> result = cardManager.ShowDeck(username);
            
            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }
    }
}