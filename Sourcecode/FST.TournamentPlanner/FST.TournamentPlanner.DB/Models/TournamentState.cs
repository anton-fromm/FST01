using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.DB.Models
{
    public enum TournamentState
    {
        [Description("Created")]
        Created,
        [Description("Started")]
        Started,
        [Description("Finished")]
        Finished
    }
}
