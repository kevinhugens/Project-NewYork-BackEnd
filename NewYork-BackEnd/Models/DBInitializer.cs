using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
            byte[] saltAdmin = Hashing.getSalt();
            context.Users.AddRange(
                new User
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.be",
                    Password = Hashing.getHash("admin",saltAdmin),
                    HashSalt = saltAdmin,
                    DateOfBirth = DateTime.Now,
                    Role = "admin",
                    TeamID = null
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
                    TeamID = null
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
            context.AddRange(
                new Team
                {
                    TeamName = "Team1",
                    CompanyName = "Company1",
                    Address = "Address Team 1",
                    Photo = "Team1.jpg",
                    CaptainID = 2
                });
            context.AddRange(
                new Team
                {
                    TeamName = "Team2",
                    CompanyName = "Company2",
                    Address = "Address Team 2",
                    Photo = "Team2.jpg",
                    CaptainID = 3
                });

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
            #endregion
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
                new Game
                {
                    Type = "1vs1",
                    ScoreTeam1 = 0,
                    ScoreTeam2 = 0,
                    Date = DateTime.Now,
                    Address = "Address game 1",
                    Team1ID = 1,
                    Team2ID = 2,
                    CompetitionID=1,
                    GameStatusID = 1
                });
            #endregion
            context.SaveChanges();
            #region UserGame
            context.AddRange(
                new UserGame
                {
                    UserID = 1,
                    GameID = 1
                });
            context.AddRange(
                new UserGame
                {
                    UserID = 2,
                    GameID = 1
                });
            #endregion
            context.SaveChanges();
        }
    }
}
