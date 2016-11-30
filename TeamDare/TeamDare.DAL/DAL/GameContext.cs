using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TeamDare.Core;
using TeamDare.DAL.DAL;

namespace TeamDare.DAL
{
    public class GameContext : DbContext
    {
        public DbSet<GameMaster> GameMasters { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<Challenge> Challenges { get; set; }

        public GameContext():base("GameContext")
        {
            Database.SetInitializer(new GameContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Challenge>()
                .HasRequired(c => c.Hero)
                .WithMany()
                .WillCascadeOnDelete(false);

            /*modelBuilder.Entity<Player>()
                .HasRequired(c => c.GameMaster)
                .WithOptional()
                .WillCascadeOnDelete(false);*/

            modelBuilder.Entity<Player>()
                   .HasRequired<GameMaster>(s => s.GameMaster) // Student entity requires Standard 
                   .WithMany(s => s.Players) // Standard entity includes many Students entities
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Challenge>()
                   .HasRequired<Adventure>(s => s.Adventure) // Student entity requires Standard 
                   .WithMany(s => s.Challenges) // Standard entity includes many Students entities
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Adventure>()
                   .HasRequired<Player>(s => s.Hero) // Student entity requires Standard 
                   .WithMany(s => s.GamesHistory) // Standard entity includes many Students entities
                   .WillCascadeOnDelete(false);
        }
    }

}