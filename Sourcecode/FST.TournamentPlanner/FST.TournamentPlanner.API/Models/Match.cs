using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Models
{
    [DebuggerDisplay("{TeamOne} vs {TeamTwo}: {TeamOneScore}:{TeamTwoScore}")]
    public class Match
    {
        private DB.Models.Match _match;
        internal Match(DB.Models.Match match)
        {
            _match = match;
        }
        
        /// <summary>
        /// Id of this match
        /// </summary>
        public int Id => _match.Id;

        /// <summary>
        /// First Team in this match
        /// </summary>
        public Team TeamOne
        {
            get
            {
                if (_match.TeamOne?.Team == null)
                {
                    return null;
                }
                return new Team(_match.TeamOne.Team);
            }
        }

        /// <summary>
        /// Second Team in this match
        /// </summary>
        public Team TeamTwo 
        {
            get
            {
                if (_match.TeamTwo?.Team == null)
                {
                    return null;
                }
                return new Team(_match.TeamTwo.Team);
            }
        }

        /// <summary>
        /// Play area this match is held
        /// </summary>
        public PlayArea PlayArea => new PlayArea(_match.PlayAreaBooking.PlayArea);

        /// <summary>
        /// Start time of this match
        /// </summary>
        public DateTime Start => _match.PlayAreaBooking.Start;

        /// <summary>
        /// End time of this match
        /// </summary>
        public DateTime End => _match.PlayAreaBooking.End;

        /// <summary>
        /// Score of team one
        /// </summary>
        public int? TeamOneScore => _match.TeamOne?.Score;

        /// <summary>
        /// Score of team two
        /// </summary>
        public int? TeamTwoScore => _match.TeamTwo?.Score;

        /// <summary>
        /// State of this match
        /// </summary>
        public MatchState MatchState => _match.State;

        /// <summary>
        /// Successor of this match
        /// </summary>
        internal Match Successor
        {
            get
            {
                if (_match.Successor == null)
                {
                    return null;
                }
                return new Match(_match.Successor);
            }
        }

        /// <summary>
        /// First predecessor game
        /// </summary>
        public Match FirstPredecessor
        {
            get
            {
                DB.Models.Match preMatch = _match.Predecessors?.OrderBy(p => p.Id).FirstOrDefault();
                if (preMatch == null)
                {
                    return null;
                }
                return new Match(preMatch);
            }
        }
        
        /// <summary>
        /// Second predecessor game
        /// </summary>
        public Match SecondPredecessor
        {
            get
            {
                DB.Models.Match preMatch = _match.Predecessors?.OrderBy(p => p.Id).LastOrDefault();
                if (preMatch == null)
                {
                    return null;
                }
                return new Match(preMatch);
            }
        }

        /// <summary>
        /// Looser team of this match
        /// </summary>
        public Team Looser
        {
            get
            {
                if (MatchState != MatchState.Finished)
                {
                    return null;
                }
                if (_match.TeamOne.Score < _match.TeamTwo.Score)
                {
                    return new Team(_match.TeamOne.Team);
                }
                return new Team(_match.TeamTwo.Team);
            }
        }

        /// <summary>
        /// Winner team of this match
        /// </summary>
        public Team Winner
        {
            get
            {
                if (MatchState != MatchState.Finished)
                {
                    return null;
                }
                if (_match.TeamOne.Score > _match.TeamTwo.Score)
                {
                    return new Team(_match.TeamOne.Team);
                }
                return new Team(_match.TeamTwo.Team);
            }
        }
    }
}
