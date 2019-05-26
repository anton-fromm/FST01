using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Contracts
{
    public interface ITeam
    {
        /// <summary>
        /// Name of the team
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Description of the team
        /// </summary>
        string Description { set; get; }
    }
}
