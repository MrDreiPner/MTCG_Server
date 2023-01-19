﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.CardTypes
{
    internal class Monster : Card
    {
        public Monster(int guID, string name, int dmg)
        {
            _guID = guID;
            _name = name;
            if (name.Substring(0, 5) == "Water")
                _elementID = Server.ElementID.Water;
            else if (name.Substring(0, 4) == "Fire")
                _elementID = Server.ElementID.Fire;
            else
                _elementID = Server.ElementID.Normal;
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
