using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.CardTypes;

namespace MTCG.MTCG.Models
{
    public class TradeDeal
    {
        public string Id { get; set; }
        public string CardToTrade { get; set; }
        public int MinimumDamage { get; set; }
        public string Type { get; set; }

        public TradeDeal(string card, string did, int mindmg, string type)
        {
            Id = did;
            CardToTrade = card;
            MinimumDamage = mindmg;
            Type = type;
        }

    }
}
