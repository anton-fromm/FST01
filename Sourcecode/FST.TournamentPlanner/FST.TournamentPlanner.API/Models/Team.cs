using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using db = FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Models
{
    /// <summary>
    /// Team class
    /// </summary>
    [DebuggerDisplay("{Id} - {Name}")]
    public class Team
    {
        internal Team() { }

        internal Team(db.Team team)
        {
            Id = team.Id;
            Name = team.Name;
        }

        /// <summary>
        /// Id of the team
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the team
        /// </summary>
        public string Name { get; set; }
    }
}
