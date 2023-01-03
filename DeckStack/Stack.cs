﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.CardTypes;

namespace MTCG_Server.DeckStack
{
    internal class Stack
    {
        protected int size;
        protected List<Card> stackCards;
        protected int _ownerID;

        public int OwnerID { get { return _ownerID; } }
        public int Size { get { return size; } }
        public List<Card> StackCards { get { return stackCards; } set { stackCards = value; } }

        public Stack()
        {
            Card player1 = new Monster(12, "Dragonlord", 120);
            Card player2 = new Spell(45, "Magnum Spark", 50);
            size = 0;
            stackCards = new List<Card>();
            AddCard(player1);
            AddCard(player2);
            _ownerID = 100;
            Console.WriteLine("Size of Deck: "+size+"\nOwnerID of this Stack: "+_ownerID);
        }

        public void AddCard(Card newCard)
        {
            newCard.InDeck = false;
            stackCards.Add(newCard);
            size++;
            int counter = 0;
            foreach(Card card in stackCards)
            {
                counter++;
                Console.WriteLine(counter+". Added card: "+card.Name);
            }
        }
        ~Stack() { }
    }
}