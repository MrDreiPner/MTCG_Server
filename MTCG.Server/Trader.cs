using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.CardTypes;

namespace MTCG_Server
{
    internal class Trader
    {
        private struct TraderStruct {
            private string ownerID;
            private Card cardToTrade;
            private int desiredDmg;
            private int desiredElement;
            private int desiredType;

            public string OwnerID { get { return ownerID; } set { ownerID = value; } }
            public Card CardToTrade { get { return cardToTrade; } set { cardToTrade = value; } }
            public int DesiredDmg { get { return desiredDmg; } set { desiredDmg = value; } }
            public int DesiredElement { get { return desiredElement; } set { desiredElement = value; } }
            public int DesiredType { get { return desiredType; } set { desiredType = value; } }
        };

        public bool abortRequested;

        private List<TraderStruct> trades;


        public Trader()
        {
            trades = new List<TraderStruct>();
            abortRequested = false;
        }

        ~Trader() { }

        public void StartTrader()
        {
            while (!abortRequested)
            {
                if(trades.Count() > 1) {
                    
                }
            }
            Console.WriteLine("Trader has been aborted");
        }

        public void AddOffer(Card card, int desiredDmg, int desiredElement, int desiredType) {
            if (card.InDeck)
            {
                Console.WriteLine("Card is in DECK and can´t be traded! Please take card out of your DECK");
            }
            else
            {
                TraderStruct newOffer = new TraderStruct();
                card.InTrade = true;
                newOffer.OwnerID = card.OwnerID;
                newOffer.CardToTrade = card;
                newOffer.DesiredDmg = desiredDmg;
                newOffer.DesiredElement = desiredElement;
                newOffer.DesiredType = desiredType;
                trades.Add(newOffer);
            }
        }

        public void RemoveOffer(string ownerID) { 
            for(int i = trades.Count() - 1; i >= 0; i--)
            {
                if(ownerID == trades[i].OwnerID) {
                    trades[i].CardToTrade.InTrade = false;
                    trades.RemoveAt(i);
                }
            }
        }
        public void AbortTrader()
        {
            abortRequested = true;
        }
    }
}
