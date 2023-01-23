using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.CardTypes
{
    abstract class Card
    {
        protected string _guID;
        protected string _name;
        protected Server.ElementID _elementID;
        protected int _dmg;
        protected bool _inDeck;
        protected bool _inTrade;
        protected string _ownerID;
        protected int _packID;
        protected string _type;


        public string GuID { get { return _guID; } }
        public string Name { get { return _name; }}
        public Server.ElementID ElementID { get { return _elementID; } set { _elementID = value; } }
        public int Dmg { get { return _dmg; }}
        public bool InDeck { 
            get { return _inDeck; }
            set { _inDeck = value; }
        }
        public bool InTrade {
            get { return _inTrade; }
            set { _inTrade = value; }
        }
        public string OwnerID
        {
            get { return _ownerID; }
            set { _ownerID = value; }
        }

        public int PackID { 
            get { return _packID; }
            set { _packID = value; }
        }

        public string Type{
            get { return _type; }
            set { _type = value; }
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
