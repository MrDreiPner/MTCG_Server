using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.CardTypes
{
    public class Monster : Card
    {
        public Monster(string guID, string name, int dmg)
        {
            _guID = guID;
            _name = name;
            if(name.Length >= 5)
            {
                if (name.Substring(0, 5) == "Water" || name == "Kraken")
                    _elementID = CardTypes.ElementID.water;
                else if (name.Substring(0, 4) == "Fire" || name == "Dragon")
                    _elementID = CardTypes.ElementID.fire;
                else
                    _elementID = CardTypes.ElementID.normal;

            }
            else
            {
                _elementID = CardTypes.ElementID.normal;
            }
            _dmg = dmg;
            _type = "Monster";
        }
        ~Monster() 
        { 
            Console.WriteLine("Monster destructor has been called"); 
        }
    }
}
