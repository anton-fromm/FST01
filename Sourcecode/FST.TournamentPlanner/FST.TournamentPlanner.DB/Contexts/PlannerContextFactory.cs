using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FST.TournamentPlanner.DB.Contexts
{
    public class PlannerContextFactory : IDesignTimeDbContextFactory<PlannerContext>
    {

        public PlannerContext CreateDbContext(string[] args)
        {
            PlannerContext context = new PlannerContext(Settings.DbConnection);
            return context;
        }
    }
}