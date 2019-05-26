using FST.TournamentPlanner.DB.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FST.TournamentPlanner.DB.Contexts
{
    public class PlannerContext : DbContext
    {
        public DbSet<Match> Match { get; set; }
        public DbSet<MatchResult> MatchResult { get; set; }
        public DbSet<PlayArea> PlayArea { get; set; }
        public DbSet<PlayAreaBooking> PlayAreaBooking { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Tournament> Tournament { get; set; }

        private string connectionString = Settings.DbConnection;

        public PlannerContext(DbContextOptions options) : base(options) { }

        public PlannerContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().HasOne(m => m.TeamOne);
            modelBuilder.Entity<Match>().HasOne(m => m.TeamTwo);
            modelBuilder.Entity<MatchResult>().HasOne(mr => mr.Match);
        }
    }
}
