using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class UserGame
    {
        public int UserGameID { get; set; }
        public int UserID { get; set; }
        public int GameID { get; set; }
    }
}
