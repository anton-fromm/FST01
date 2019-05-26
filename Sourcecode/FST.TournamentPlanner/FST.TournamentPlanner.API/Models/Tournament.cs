using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using db = FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Models
{
    [Serializable]
    public class Tournament
    {
        db.Tournament _tournament;
        internal Tournament(db.Tournament tournament)
        {
            _tournament = tournament;
        }

        #region Master data stuff
        /// <summary>
        /// Id of the tournament
        /// </summary>
        public int Id => _tournament.Id;

        /// <summary>
        /// Name of the tournament
        /// </summary>
        public string Name => _tournament.Name;
        /// <summary>
        /// Description of the tournament
        /// </summary>
        public string Description => _tournament.Description;

        /// <summary>
        /// Start day and time of the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        public DateTime StartTime => _tournament.Start;

        /// <summary>
        /// Duration a single match within the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        public int MatchDuration => _tournament.MatchDuration;

        /// <summary>
        /// Number of teams in the tournament.
        /// This number must be expressable as potence of basis 2
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        public int TeamCount => _tournament.TeamCount;
        #endregion

        #region Tournament state
        /// <summary>
        /// Current state of the tournament
        /// </summary>
        public db.TournamentState State => _tournament.State;
        #endregion

        /// <summary>
        /// List of play areas within this tournament
        /// </summary>
        public List<PlayArea> PlayAreas
        {
            get
            {
                return _tournament.PlayAreas.Select(p => new PlayArea(p)).ToList();
            }
        }

        /// <summary>
        /// List of teams
        /// </summary>
        public List<Team> Teams => _tournament.Teams.Select(t => new Team(t)).ToList();


    }
}
