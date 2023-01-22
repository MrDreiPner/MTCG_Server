using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.CardTypes
{
    internal class Spell : Card
    {
        public Spell(string guID, string name, int dmg)
        {
            _guID = guID;
            _name = name;
            if (name.Substring(0, 5) == "Water")
                _elementID = Server.ElementID.water;
            else if (name.Substring(0, 4) == "Fire")
                _elementID = Server.ElementID.fire;
            else
                _elementID = Server.ElementID.normal;
            _dmg = dmg;
            _type = "Spell";
            //Console.WriteLine("Spell is created");
        }
        ~Spell() 
        { 
            Console.WriteLine("Spell destructor has been called"); 
        }
    }
}
