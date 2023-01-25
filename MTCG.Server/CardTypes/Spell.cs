using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.CardTypes
{
    public class Spell : Card
    {
        public Spell(string guID, string name, int dmg)
        {
            _guID = guID;
            _name = name;
            if (name.Substring(0, 5) == "Water")
                _elementID = CardTypes.ElementID.water;
            else if (name.Substring(0, 4) == "Fire")
                _elementID = CardTypes.ElementID.fire;
            else
                _elementID = CardTypes.ElementID.normal;
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
