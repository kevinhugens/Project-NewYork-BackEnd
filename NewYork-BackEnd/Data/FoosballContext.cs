using Microsoft.EntityFrameworkCore;
using NewYork_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Data
{
    public class FoosballContext : DbContext
    {
        public FoosballContext(DbContextOptions<FoosballContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users {get;set;}
        public DbSet<Table> Table { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<Ranking> Ranking { get; set; }

        public DbSet<Game> Game { get; set; }

        public DbSet<Competition> Competition { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Table>().ToTable("Table");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Ranking>().ToTable("Ranking");
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Competition>().ToTable("Competition");
        }

        
    }
}
