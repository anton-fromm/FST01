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
        private DB.Models.PlayArea  _playarea;
        internal PlayArea(DB.Models.PlayArea playarea)
        {
            _playarea = playarea;
        }
        /// <summary>
        /// Id of the play area
        /// </summary>
        public int Id
        {
            get => _playarea.Id;
        }

        /// <summary>
        /// Name of the play area
        /// </summary>
        public string Name
        {
            get => _playarea.Name;
            set => _playarea.Name = value;
        }

        /// <summary>
        /// Description of the play area
        /// </summary>
        public string Description
        {
            get => _playarea.Description;
            set => _playarea.Description = value;
        }
    }
}
