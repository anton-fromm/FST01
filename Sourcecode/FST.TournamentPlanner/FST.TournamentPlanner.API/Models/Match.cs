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
        private Tournament _tournament;

        internal Match() { }

        internal Match(Tournament tournament, DB.Models.Match match)
        {
            _match = match;
            Id = match.Id;
            _tournament = tournament;
        }

        /// <summary>
        /// Id of this match
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First Team in this match
        /// </summary>
        public Team TeamOne
        {
            get
            {
                if (_match == null || _match.TeamOne == null || _match.TeamOne.Team == null)
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
                if (_match == null || _match.TeamTwo == null || _match.TeamTwo.Team == null)
                {
                    return null;
                }
                return new Team(_match.TeamTwo.Team);
            }
        }

        /// <summary>
        /// Play area this match is held
        /// </summary>
        public PlayArea PlayArea
        {
            get
            {
                if (_match == null || _match.PlayAreaBooking == null)
                {
                    return null;
                }
                return new PlayArea(_match.PlayAreaBooking.PlayArea);
            }
        }

        /// <summary>
        /// Start time of this match
        /// </summary>
        public DateTime? Start
        {
            get
            {
                if (_match == null || _match.PlayAreaBooking == null)
                {
                    return null;
                }
                return _match.PlayAreaBooking.Start;
            }
        }

        /// <summary>
        /// End time of this match
        /// </summary>
        public DateTime? End
        {
            get
            {
                if (_match == null || _match.PlayAreaBooking == null)
                {
                    return null;
                }
                return _match.PlayAreaBooking.End;
            }
        }

        /// <summary>
        /// Score of team one
        /// </summary>
        public int? TeamOneScore
        {
            get
            {
                if (_match == null || _match.TeamOne == null)
                {
                    return null;
                }
                return _match.TeamOne.Score;
            }
        }        

        /// <summary>
        /// Score of team two
        /// </summary>
        public int? TeamTwoScore
        {
            get
            {
                if (_match == null || _match.TeamTwo == null)
                {
                    return null;
                }
                return _match.TeamTwo.Score;
            }
        }

        /// <summary>
        /// State of this match
        /// </summary>
        public MatchState MatchState
        {
            get
            {
                if (_match == null)
                {
                    return MatchState.Planned;
                }
                return _match.State;
            }
        }

        /// <summary>
        /// Successor of this match
        /// </summary>
        internal Match Successor
        {
            get
            {
                if (_match == null || _match.Successor == null)
                {
                    return null;
                }
                return new Match(_tournament, _match.Successor);
            }
        }

        /// <summary>
        /// First predecessor game
        /// </summary>
        public Match FirstPredecessor
        {
            get
            {
                if (_match == null )
                {
                    return null;
                }

                DB.Models.Match preMatch = _match.Predecessors?.OrderBy(p => p.Id).FirstOrDefault();
                if (preMatch == null)
                {
                    return null;
                }
                return new Match(_tournament, preMatch);
            }
        }

        /// <summary>
        /// Second predecessor game
        /// </summary>
        public Match SecondPredecessor
        {
            get
            {
                if (_match == null)
                {
                    return null;
                }

                DB.Models.Match preMatch = _match.Predecessors?.OrderBy(p => p.Id).LastOrDefault();
                if (preMatch == null)
                {
                    return null;
                }
                return new Match(_tournament, preMatch);
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
