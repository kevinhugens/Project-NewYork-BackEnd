using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewYork_BackEnd.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Photo { get; set; }
        public byte[] HashSalt { get; set; }
        public string Role { get; set; }
        //Relations
        public int? TeamID { get; set; }
        //public Team Team { get; set; }

        //public ICollection<UserGame> UserGames { get; set; }
    }
}   
    