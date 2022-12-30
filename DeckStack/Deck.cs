using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.CardTypes;

namespace MTCG_Server.DeckStack
{
    internal class Deck
    {
        private const int maxSize = 4;
        protected int size;
        protected List<Card>? deckCards;
        protected int _ownerID;

        public int OwnerID { get { return _ownerID; } }
        public int Size { get { return size; } }
        public List<Card> DeckCards { get { return deckCards; } set { deckCards = value; } }

        public Deck(int ownerID, int deckType)
        {
            Card card1, card2, card3, card4;
            switch (deckType)
            {
                case 1:
                    card1 = new Monster(222, "FireDragee", 75);
                    card2 = new Spell(245, "WaterSwoosh", 80);
                    card3 = new Spell(250, "IronicHail", 50);
                    card4 = new Spell(200, "FireTatertot", 69);
                break;
                case 2:
                    card1 = new Monster(157, "FireDragonlord", 100);
                    card2 = new Spell(111, "WaterSpark", 60);
                    card3 = new Monster(123, "MagnumDonger", 50);
                    card4 = new Spell(100, "WaterGreata", 96);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            size = 0;
            DeckCards = new List<Card>();
            AddCard(card1);
            AddCard(card2);
            AddCard(card3);
            AddCard(card4);
            foreach(Card card in DeckCards)
            {
                card.OwnerID = ownerID;
            }
            //RemoveCard(2);
            _ownerID = ownerID;
            //PrintDeck();
            Console.WriteLine("Size of Deck: " + size + "\nOwnerID of this Deck: " + _ownerID);
        }

        public void AddCard(Card newCard)
        {
            newCard.InDeck = true;
            DeckCards.Add(newCard);
            size++;
            Console.WriteLine("Added: " + newCard.Name + " - ID: " + newCard.GuID+" to OwnerID "+_ownerID);
        }
        public void RemoveCard(int spot) 
        {
            try
            {
                spot--;
                Console.WriteLine(DeckCards.ElementAt(spot).Name + " has been removed from OwnerID "+_ownerID);
                DeckCards.RemoveAt(spot);
                size--;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Invalid Index for removal input: "+ex.ToString());
                Environment.Exit(1);
            }
        }

        public void PrintDeck()
        {
            int counter = 0;
            foreach (Card card in DeckCards)
            {
                counter++;
                string? elemetType = Enum.GetName(typeof(Server.ElementID),card.ElementID);
                Console.WriteLine(counter + ". " + card.Name + " - ID: " + card.GuID+" | Element Type+ID: "+elemetType + " " + card.ElementID+" | OwnerID: "+card.OwnerID);
            }
            Console.WriteLine("Size of Deck: " + size + "\nOwnerID of this Deck: " + _ownerID);
        }
        ~Deck() { }
    }
}
