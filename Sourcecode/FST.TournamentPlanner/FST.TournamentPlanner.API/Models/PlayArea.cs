using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Models
{
    /// <summary>
    /// Class representing a play area
    /// </summary>
    [DebuggerDisplay("{Name} ({Description})")]
    public class PlayArea
    {
        /// <summary>
        /// empty c'tor for API Model mapping
        /// </summary>
        internal PlayArea() { }

        internal PlayArea(DB.Models.PlayArea playarea)
        {
            Name = playarea.Name;
            Description = playarea.Description;
            Id = playarea.Id;
        }
        /// <summary>
        /// Id of the play area
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the play area
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the play area
        /// </summary>
        public string Description { get; set; }
    }
}
