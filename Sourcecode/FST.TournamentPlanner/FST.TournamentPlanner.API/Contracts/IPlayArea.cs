using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Contracts
{
    public interface IPlayArea
    {
        /// <summary>
        /// Name of the play area
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Description of the play area
        /// </summary>
        string Description { get; set; }
    }
}
