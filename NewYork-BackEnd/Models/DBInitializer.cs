using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using NewYork_BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class DBInitializer
    {
        public static void Initialize(FoosballContext context)
        {
            context.Database.EnsureCreated();

            // Look for any user.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            #region GameStatus
            context.AddRange(
                new GameStatus
                {
                    Name = "Gepland"
                });
            context.AddRange(
                new GameStatus
                {
                    Name = "Bezig"
                });
            context.AddRange(
                new GameStatus
                {
                    Name = "Beëindigd"
                });
            context.AddRange(
                new GameStatus
                {
                    Name = "Moderator"
                });
            #endregion
            context.SaveChanges();

            #region Users
            byte[] saltWouter = Hashing.getSalt();
            byte[] saltIebe = Hashing.getSalt();
            byte[] saltKevin = Hashing.getSalt();
            byte[] saltArno = Hashing.getSalt();

            byte[] saltTest = Hashing.getSalt();
            byte[] saltConnie = Hashing.getSalt();
            byte[] saltJustin = Hashing.getSalt();
            byte[] saltJane = Hashing.getSalt();
            byte[] saltJost = Hashing.getSalt();
            byte[] saltIzzy = Hashing.getSalt();
            byte[] saltSonny = Hashing.getSalt();
            byte[] saltBarry = Hashing.getSalt();
            byte[] saltBob = Hashing.getSalt();
            byte[] saltBeau = Hashing.getSalt();
            byte[] saltJill = Hashing.getSalt();
            context.Users.AddRange(
                new User { FirstName = "Wouter", LastName = "Vanaelten", Email = "woutervanaelten@thomasmore.be", Password = Hashing.getHash("test", saltWouter), HashSalt = saltWouter, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 1 },
                new User { FirstName = "Iebe", LastName = "Maes", Email = "iebemaes@thomasmore.be", Password = Hashing.getHash("test", saltIebe), HashSalt = saltIebe, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 1 },
                new User { FirstName = "Kevin", LastName = "Huygens", Email = "kevinhuygens@thomasmore.be", Password = Hashing.getHash("test", saltKevin), HashSalt = saltKevin, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 1 },
                new User { FirstName = "Arno", LastName = "Vangoetsenhoven", Email = "arnovangoetsenhoven@thomasmore.be", Password = Hashing.getHash("test", saltArno), HashSalt = saltArno, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 1 },

                new User { FirstName = "Test", LastName = "User", Email = "test.user@thomasmore.be", Password = Hashing.getHash("test", saltTest), HashSalt = saltTest, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 2 },
                new User { FirstName = "Connie", LastName = "Moeleker", Email = "conniemoeleker@gmail.com", Password = Hashing.getHash("test", saltConnie), HashSalt = saltConnie, DateOfBirth = DateTime.Now.AddYears(-30), Role = "user", TeamID = 2 },
                new User { FirstName = "Justin", LastName = "Cas", Email = "justincas@hotmail.com", Password = Hashing.getHash("test", saltJustin), HashSalt = saltJustin, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 2 },
                new User { FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com", Password = Hashing.getHash("test", saltJane), HashSalt = saltJane, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 3 },
                new User { FirstName = "Jost", LastName = "Tibant", Email = "jostibant@hotmail.com", Password = Hashing.getHash("test", saltJost), HashSalt = saltJost, DateOfBirth = DateTime.Now.AddYears(-25), Role = "user", TeamID = 3 },
                new User { FirstName = "Izzy", LastName = "Van Isteren", Email = "izzyvanisteren@hotmail.com", Password = Hashing.getHash("test", saltIzzy), HashSalt = saltIzzy, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 3 },
                new User { FirstName = "Sonny", LastName = "Day", Email = "sonnyday@gmail.com", Password = Hashing.getHash("test", saltSonny), HashSalt = saltSonny, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 4 },
                new User { FirstName = "Barry", LastName = "Cade", Email = "barricade@hotmail.com", Password = Hashing.getHash("test", saltBarry), HashSalt = saltBarry, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 4 },
                new User { FirstName = "Bob", LastName = "Sleeman", Email = "bobsleeman@yahoot.com", Password = Hashing.getHash("test", saltBob), HashSalt = saltBob, DateOfBirth = DateTime.Now.AddYears(-30), Role = "user", TeamID = 4 },
                new User { FirstName = "Beau", LastName = "Ter Ham", Email = "beauterham@hotmail.com", Password = Hashing.getHash("test", saltBeau), HashSalt = saltBeau, DateOfBirth = DateTime.Now.AddYears(-20), Role = "user", TeamID = 4 },
                new User { FirstName = "Jill", LastName = "Van Rethij", Email = "jillekevanrethij@gmail.com", Password = Hashing.getHash("test", saltJill), HashSalt = saltJill, DateOfBirth = DateTime.Now.AddYears(-80), Role = "user", TeamID = 5 }
            );

            byte[] saltAdmin = Hashing.getSalt();
            context.Users.AddRange(
            new User
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.be",
                Password = Hashing.getHash("admin", saltAdmin),
                HashSalt = saltAdmin,
                DateOfBirth = DateTime.Now,
                Role = "admin",
                TeamID = 2
            });
            byte[] saltUser1 = Hashing.getSalt();
            context.Users.AddRange(
                new User
                {
                    FirstName = "user1",
                    LastName = "user1",
                    Email = "user1@testing.be",
                    Password = Hashing.getHash("user1", saltUser1),
                    HashSalt = saltUser1,
                    DateOfBirth = DateTime.Now,
                    Role = "user",
                    TeamID = 1
                });
            byte[] saltUser2 = Hashing.getSalt();
            context.Users.AddRange(
                new User
                {
                    FirstName = "user2",
                    LastName = "user2",
                    Email = "user2@testing.be",
                    Password = Hashing.getHash("user2", saltUser2),
                    HashSalt = saltUser2,
                    DateOfBirth = DateTime.Now,
                    Role = "user",
                    TeamID = 2
                });
            byte[] saltUser3 = Hashing.getSalt();
            context.Users.AddRange(
                new User
                {
                    FirstName = "user3",
                    LastName = "user3",
                    Email = "user3@testing.be",
                    Password = Hashing.getHash("user3", saltUser3),
                    HashSalt = saltUser3,
                    DateOfBirth = DateTime.Now,
                    Role = "user",
                    TeamID = 1
                });
            byte[] saltUser4 = Hashing.getSalt();
            context.Users.AddRange(
                new User
                {
                    FirstName = "user4",
                    LastName = "user4",
                    Email = "user4@testing.be",
                    Password = Hashing.getHash("user4", saltUser4),
                    HashSalt = saltUser3,
                    DateOfBirth = DateTime.Now,
                    Role = "user",
                    TeamID = null
                });
            byte[] saltUser5 = Hashing.getSalt();
            context.Users.AddRange(
                new User
                {
                    FirstName = "user5",
                    LastName = "user5",
                    Email = "user5@testing.be",
                    Password = Hashing.getHash("user5", saltUser5),
                    HashSalt = saltUser5,
                    DateOfBirth = DateTime.Now,
                    Role = "user",
                    TeamID = null
                });
            byte[] saltUser6 = Hashing.getSalt();
            context.Users.AddRange(
                new User
                {
                    FirstName = "user6",
                    LastName = "user6",
                    Email = "user6@testing.be",
                    Password = Hashing.getHash("user6", saltUser6),
                    HashSalt = saltUser6,
                    DateOfBirth = DateTime.Now,
                    Role = "user",
                    TeamID = null
                });

            #endregion

            #region Team
            //context.AddRange(
            //    new Team { TeamID = 1, TeamName = "The Big App", CompanyName = "Thomas More Geel", Address = "Kleinhoefstraat 4, 2440 Geel", Photo = "Team2.jpg", CaptainID = 4 },
            //    new Team { TeamID = 2, TeamName = "Hawaii", CompanyName = "Thomas De Nayer", Address = "Jan De Nayerlaan 5, 2860 Sint-Katelijne-Waver", Photo = "Team2.jpg", CaptainID = 16 },
            //    new Team { TeamID = 3, TeamName = "Georgia", CompanyName = "Thomas More Lier", Address = "Antwerpsestraat 99, 2500 Lier", Photo = "Team2.jpg", CaptainID = 13 },
            //    new Team { TeamID = 4, TeamName = "Florida", CompanyName = "Thomas More Vorselaar", Address = "Lepelstraat 2, 2290 Vorselaar", Photo = "Team2.jpg", CaptainID = 7 }
            //);
            context.AddRange(
                new Team { TeamName = "The Big App", CompanyName = "Thomas More Geel", Address = "Kleinhoefstraat 4, 2440 Geel", Photo = "Team2.jpg", CaptainID = 4 },
                new Team { TeamName = "Hawaii", CompanyName = "Thomas De Nayer", Address = "Jan De Nayerlaan 5, 2860 Sint-Katelijne-Waver", Photo = "Team2.jpg", CaptainID = 16 },
                new Team { TeamName = "Georgia", CompanyName = "Thomas More Lier", Address = "Antwerpsestraat 99, 2500 Lier", Photo = "Team2.jpg", CaptainID = 13 },
                new Team { TeamName = "Florida", CompanyName = "Thomas More Vorselaar", Address = "Lepelstraat 2, 2290 Vorselaar", Photo = "Team2.jpg", CaptainID = 7 }
            );

            context.AddRange(
                new Team
                {
                    TeamName = "Team1",
                    CompanyName = "Company1",
                    Address = "Address Team 1",
                    Photo = "Team1.jpg",
                    CaptainID = 8
                });
            context.AddRange(
                new Team
                {
                    TeamName = "Team2",
                    CompanyName = "Company2",
                    Address = "Address Team 2",
                    Photo = "Team2.jpg",
                    CaptainID = 4
                });

            //context.Database.OpenConnection();
            //try
            //{
            //    context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[User] ON");
            //    context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Team] ON");
            //    context.SaveChanges();
            //    context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[User] OFF");
            //    context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Team] OFF");
            //}
            //finally
            //{
            //    context.Database.CloseConnection();
            //}

            #endregion
            context.SaveChanges();

            #region Table
            context.AddRange(
                new Table
                {
                    TableName = "Table 1",
                    CompanyName = "Company1",
                    Address = "Address Company 1",
                    Photo = "Table1.jpg",
                    ManagerID = 1
                });
            context.AddRange(
                new Table
                {
                    TableName = "Table 2",
                    CompanyName = "Company2",
                    Address = "Address Company 2",
                    Photo = "Table2.jpg",
                    ManagerID = 2
                });

            context.AddRange(
                new Table { TableName = "Aggora-tafel", CompanyName = "Thomas More Geel", Address = "Kleinhoefstraat 4, 2440 Geel", Photo = "Table3.jpg", ManagerID = 1 },
                new Table { TableName = "Kickeren - The Hive", CompanyName = "Thomas More Geel", Address = "Kleinhoefstraat 4, 2440 Geel", Photo = "Table3.jpg", ManagerID = 1 },
                new Table { TableName = "Foosball", CompanyName = "Thomas De Nayer", Address = "Jan De Nayerlaan 5, 2860 Sint-Katelijne-Waver", Photo = "Table1.jpg", ManagerID = 1 },
                new Table { TableName = "Kick Hard", CompanyName = "Thomas More Lier", Address = "Antwerpsestraat 99, 2500 Lier", ManagerID = 1 },
                new Table { TableName = "Kicker plaats", CompanyName = "Thomas More Vorselaar", Address = "Lepelstraat 2, 2290 Vorselaar", Photo = "Table1.jpg", ManagerID = 1 }
            );

            #endregion
            context.SaveChanges();

            #region competition
            context.AddRange(
                new Competition
                {
                    Address = "Addres competition 1",
                    Name = "Competition 1"
                });
            context.AddRange(
                new Competition
                {
                    Address = "Addres competition 2",
                    Name = "Competition 2"
                });

            #endregion
            context.SaveChanges();
            #region ranking
            context.AddRange(
                new Ranking
                {
                    Points = 5,
                    TeamID = 1,
                    CompetitionID = 1
                });
            context.AddRange(
                new Ranking
                {
                    Points = 2,
                    TeamID = 2,
                    CompetitionID = 1
                });
            #endregion
            context.SaveChanges();

            #region Game
            context.AddRange(
                new Game { GameID = 1, Type = "1vs1", ScoreTeam1 = 11, ScoreTeam2 = 5, Date = DateTime.Now.AddDays(-5), Team1ID = 1, Team2ID = 2, TableID = 1, CompetitionID = 1, GameStatusID = 3},
                new Game { GameID = 2, Type = "1vs1", ScoreTeam1 = 11, ScoreTeam2 = 3, Date = DateTime.Now.AddDays(-5), Team1ID = 3, Team2ID = 4, TableID = 2, CompetitionID = 1, GameStatusID = 3},
                new Game { GameID = 3, Type = "1vs1", ScoreTeam1 = 11, ScoreTeam2 = 5, Date = DateTime.Now.AddDays(-2), Team1ID = 3, Team2ID = 1, TableID = 3, CompetitionID = 1, GameStatusID = 3 },
                new Game { GameID = 4, Type = "1vs1", ScoreTeam1 = 8, ScoreTeam2 = 11, Date = DateTime.Now.AddDays(-3), Team1ID = 2, Team2ID = 4, TableID = 4, CompetitionID = 1, GameStatusID = 3 },

                //Vriendschappelijke wedstrijden
                new Game { GameID = 5, Type = "1vs1", ScoreTeam1 = 5, ScoreTeam2 = 11, Date = DateTime.Now.AddDays(-4), Team1ID = 1, Team2ID = 2, TableID = 5, CompetitionID = null, GameStatusID = 3 },
                new Game { GameID = 6, Type = "1vs1", ScoreTeam1 = 3, ScoreTeam2 = 11, Date = DateTime.Now.AddDays(-2), Team1ID = 3, Team2ID = 4, TableID = 6, CompetitionID = null, GameStatusID = 3 },
                new Game { GameID = 7, Type = "1vs1", ScoreTeam1 = 5, ScoreTeam2 = 11, Date = DateTime.Now.AddDays(-1), Team1ID = 5, Team2ID = 6, TableID = 7, CompetitionID = null, GameStatusID = 3 },
                new Game { GameID = 8, Type = "1vs1", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(10), Team1ID = 1, Team2ID = 3, TableID = 1, CompetitionID = null, GameStatusID = 1 },
                new Game { GameID = 9, Type = "1vs1", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(13), Team1ID = 1, Team2ID = 4, TableID = 2, CompetitionID = null, GameStatusID = 1 },
                new Game { GameID = 10, Type = "1vs1", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(5), Team1ID = 6, Team2ID = 5, TableID = 3, CompetitionID = null, GameStatusID = 1 },

                new Game { GameID = 11, Type = "2vs2", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(25), Team1ID = 2, Team2ID = 4, TableID = 4, CompetitionID = 1, GameStatusID = 1 },
                new Game { GameID = 12, Type = "2vs2", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(18), Team1ID = 3, Team2ID = 4, TableID = 5, CompetitionID = 1, GameStatusID = 1 },
                new Game { GameID = 13, Type = "2vs2", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(6), Team1ID = 5, Team2ID = 4, TableID = 6, CompetitionID = 1, GameStatusID = 1 },
                new Game { GameID = 14, Type = "2vs2", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(2), Team1ID = 6, Team2ID = 4, TableID = 7, CompetitionID = 1, GameStatusID = 1 },
                new Game { GameID = 15, Type = "2vs2", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(5), Team1ID = 2, Team2ID = 1, TableID = 1, CompetitionID = 1, GameStatusID = 1 },
                new Game { GameID = 16, Type = "2vs2", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(10), Team1ID = 2, Team2ID = 2, TableID = 2, CompetitionID = 1, GameStatusID = 1 },
                new Game { GameID = 17, Type = "2vs2", ScoreTeam1 = 0, ScoreTeam2 = 0, Date = DateTime.Now.AddDays(15), Team1ID = 5, Team2ID = 3, TableID = 3, CompetitionID = 1, GameStatusID = 1 }
                );
            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Game] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Game] OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }

            #endregion



            #region UserGame
            context.AddRange(
                // Cannot make good UserGame combination because the userID of users in a team is unknown!
                new UserGame { UserID = 4, GameID = 11 },
                new UserGame { UserID = 4, GameID = 11 },
                new UserGame { UserID = 7, GameID = 11 },
                new UserGame { UserID = 9, GameID = 11 },
                new UserGame { UserID = 5, GameID = 12 },
                new UserGame { UserID = 6, GameID = 12 },
                new UserGame { UserID = 5, GameID = 12 },
                new UserGame { UserID = 12, GameID = 12 },
                new UserGame { UserID = 5, GameID = 13 },
                new UserGame { UserID = 8, GameID = 13 },
                new UserGame { UserID = 9, GameID = 13 },
                new UserGame { UserID = 5, GameID = 13 },
                new UserGame { UserID = 2, GameID = 14 },
                new UserGame { UserID = 3, GameID = 14 },
                new UserGame { UserID = 4, GameID = 14 },
                new UserGame { UserID = 6, GameID = 15 },
                new UserGame { UserID = 4, GameID = 15 },
                new UserGame { UserID = 6, GameID = 15 },
                new UserGame { UserID = 7, GameID = 15 },
                new UserGame { UserID = 5, GameID = 16 },
                new UserGame { UserID = 3, GameID = 16 },
                new UserGame { UserID = 4, GameID = 17 },
                new UserGame { UserID = 12, GameID = 17 },
                new UserGame { UserID = 13, GameID = 17 },
                new UserGame { UserID = 11, GameID = 17 },
                new UserGame { UserID = 20, GameID = 5 },
                new UserGame { UserID = 17, GameID = 5 },
                new UserGame { UserID = 14, GameID = 6 },
                new UserGame { UserID = 21, GameID = 6 },
                new UserGame { UserID = 7, GameID = 7 },
                new UserGame { UserID = 8, GameID = 7 },
                new UserGame { UserID = 9, GameID = 8 },
                new UserGame { UserID = 10, GameID = 8 },
                new UserGame { UserID = 15, GameID = 9 },
                new UserGame { UserID = 1, GameID = 9 },
                new UserGame { UserID = 3, GameID = 10 },
                new UserGame { UserID = 4, GameID = 10 },
                new UserGame { UserID = 4, GameID = 1 },
                new UserGame { UserID = 5, GameID = 1 },
                new UserGame { UserID = 11, GameID = 2 },
                new UserGame { UserID = 13, GameID = 2 },
                new UserGame { UserID = 14, GameID = 3 },
                new UserGame { UserID = 22, GameID = 3 },
                new UserGame { UserID = 16, GameID = 4 },
                new UserGame { UserID = 10, GameID = 4 }
                ); ; ;


            #endregion
            context.SaveChanges();
        }
    }
}
