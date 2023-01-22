using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.CardTypes
{
    internal class Monster : Card
    {
        public Monster(string guID, string name, int dmg)
        {
            _guID = guID;
            _name = name;
            if(name.Length >= 5)
            {
                if (name.Substring(0, 5) == "Water" || name == "Kraken")
                    _elementID = Server.ElementID.water;
                else if (name.Substring(0, 4) == "Fire" || name == "Dragon")
                    _elementID = Server.ElementID.fire;
                else
                    _elementID = Server.ElementID.normal;

            }
            else
            {
                _elementID = Server.ElementID.normal;
            }
            _dmg = dmg;
            _type = "Monster";
            //Console.WriteLine("Monster is created");
        }
        ~Monster() 
        { 
            Console.WriteLine("Monster destructor has been called"); 
        }
    }
}
