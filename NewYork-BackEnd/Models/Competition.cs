using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class Competition
    {
        public int CompetitionID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
        public ICollection<Ranking> Rankings { get; set; }
    }
}
