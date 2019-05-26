using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using db = FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Models
{
    [DebuggerDisplay("{Id} - {Name}")]
    public class Team
    {
        private db.Team _team;
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
        public string Name => _team.Name;
    }
}
