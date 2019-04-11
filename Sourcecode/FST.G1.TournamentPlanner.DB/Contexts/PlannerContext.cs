using FST.G1.TournamentPlanner.DB.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FST.G1.TournamentPlanner.DB.Contexts
{
    public class PlannerContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

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
    }
}
