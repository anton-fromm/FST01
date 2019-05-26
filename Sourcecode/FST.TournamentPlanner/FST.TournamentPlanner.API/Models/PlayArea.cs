using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Models
{
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
        public int Id => _playarea.Id;

        /// <summary>
        /// Name of the play area
        /// </summary>
        public string Name => _playarea.Name;

        /// <summary>
        /// Description of the play area
        /// </summary>
        public string Description => _playarea.Description;
    }
}
