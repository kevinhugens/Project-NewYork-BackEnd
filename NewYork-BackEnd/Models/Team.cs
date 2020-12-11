using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }

        //Relations
        public int? CaptainID { get; set; }
        public User? Captain { get; set; }
        public ICollection<User> TeamMembers { get; set; }
        //public ICollection<Game> Games { get; set; }
    }
}
