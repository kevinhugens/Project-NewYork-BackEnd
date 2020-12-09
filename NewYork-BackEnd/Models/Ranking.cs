using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class Ranking
    {
        public int RankingID { get; set; }
        public int Points { get; set; }

        //Relations
        public int TeamID { get; set; }
        public Team Team { get; set; }
        public int CompetitionID { get; set; }
        //public Competition Competition { get; set; }
    }
}
    