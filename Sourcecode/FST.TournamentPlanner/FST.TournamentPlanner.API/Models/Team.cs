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
        private db.Team _team;
        private Tournament _tournament;
        internal Team(db.Team team)
        {
            _team = team;
        }

        /// <summary>
        /// Id of the team
        /// </summary>
        public int Id => _team.Id;

        /// <summary>
        /// Name of the team
        /// </summary>
        public string Name
        {
            get => _team.Name;
            set => _team.Name = value;
        }
    }
}
