using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.CardTypes;

namespace MTCG.DeckStack
{
    public class Deck
    {
        protected int size;
        protected List<Card>? deckCards;
        protected string? _ownerID;

        public string OwnerID { get { return _ownerID; } }
        public int Size { get { return size; } }
        public List<Card> DeckCards { get { return deckCards; } set { deckCards = value; } }

        public Deck(string ownerID, List<Card> incomingDeck)
        {
            DeckCards = new List<Card>();
            DeckCards = incomingDeck;
            size = DeckCards.Count();
            _ownerID = ownerID;
        }
        public void AddCard(Card newCard)
        {
            DeckCards.Add(newCard);
            size++;
            //Console.WriteLine("Added: " + newCard.Name + " - ID: " + newCard.GuID+" to OwnerID "+_ownerID);
        }
        public void RemoveCard(int spot) 
        {
            try
            {
                spot--;
                //Console.WriteLine(DeckCards.ElementAt(spot).Name + " has been removed from OwnerID "+_ownerID);
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
                string? elemetType = Enum.GetName(typeof(CardTypes.ElementID),card.ElementID);
                Console.WriteLine(counter + ". " + card.Name + " - ID: " + card.GuID+" | Element Type+ID: "+elemetType + " " + card.ElementID+" | OwnerID: "+card.OwnerID);
            }
            Console.WriteLine("Size of Deck: " + size + "\nOwnerID of this Deck: " + _ownerID);
        }
        ~Deck() { }
    }
}
