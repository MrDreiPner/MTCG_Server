using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.CardTypes
{
    abstract class Card
    {
        protected int _guID;
        protected string? _name;
        protected int _elementID;
        protected int _dmg;
        protected bool _inDeck;
        protected bool _inTrade;
        protected int _ownerID;


        public int GuID{ get { return _guID; } }
        public string? Name { get { return _name; }}
        public int ElementID{ get { return _elementID; } }
        public int Dmg { get { return _dmg; }}
        public bool InDeck { 
            get { return _inDeck; }
            set { _inDeck = value; }
        }
        public bool InTrade {
            get { return _inTrade; }
            set { _inTrade = value; }
        }
        public int OwnerID{
            get { return _ownerID; }
            set { _ownerID = value; }
        }

        public Card()
        {
            //Console.WriteLine("Card is created");
        }

        ~Card() 
        { 
            Console.WriteLine("Card destructor has been called"); 
        }

    }
}
