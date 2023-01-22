using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_Server.CardTypes;

namespace SWE1.MTCG.Models
{
    internal class Package
    {
        public List <Card> _cards { get; set; }
        public int packID { get; set; }

        public Package(List<Card> cards, int pid)
        {
            _cards = new List <Card>();
            _cards = cards;
            packID = pid;
        }

    }
}
