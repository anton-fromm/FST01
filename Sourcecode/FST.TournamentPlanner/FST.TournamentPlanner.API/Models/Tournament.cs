using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using db = FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Models
{
    /// <summary>
    /// Knock-out tournament class
    /// </summary>
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
        public string Name
        {
            get => _tournament.Name;
            set => _tournament.Name = value;
        }
        /// <summary>
        /// Description of the tournament
        /// </summary>
        public string Description
        {
            get => _tournament.Description;
            set => _tournament.Description = value;
        }

        /// <summary>
        /// Start day and time of the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        public DateTime StartTime
        {
            get => _tournament.Start;
            set => _tournament.Start = value;
        }

        /// <summary>
        /// Duration a single match within the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        public int MatchDuration
        {
            get => _tournament.MatchDuration;
            set => _tournament.MatchDuration = value;
        }

        /// <summary>
        /// Number of teams in the tournament.
        /// This number must be expressable as potence of basis 2
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        public int TeamCount
        {
            get => _tournament.TeamCount;
            set => _tournament.TeamCount = value;
        }
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

        /// <summary>
        /// Final match
        /// </summary>
        public Match FinalMatch
        {
            get
            {
                if (_tournament.Matches == null || _tournament.Matches.Count() == 0)
                {
                    return null;
                }
                return new Match(this, _tournament.Matches.Single(m => m.Successor == null)); // Select(m => new Match(m)).ToList();
            }
        }


    }
}
