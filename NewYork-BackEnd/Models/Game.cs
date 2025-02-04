﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string Type { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
        public DateTime Date { get; set; }

        //Relations
        public int Team1ID { get; set; }
        public Team Team1 { get; set; }
        public int? Team2ID { get; set; }
        public Team Team2 { get; set; }
        public int? CompetitionID { get; set; }
        //public Competition Competition { get; set; }
        public int TableID { get; set; }
        public Table Table { get; set; }

        public int? GameStatusID { get; set; }
        public GameStatus GameStatus { get; set; }

        public ICollection<UserGame> UserGames { get; set; }
    }
}
        