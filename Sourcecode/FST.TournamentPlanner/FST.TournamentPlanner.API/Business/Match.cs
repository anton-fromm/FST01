using FST.TournamentPlanner.API.Contracts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Business
{
    [DebuggerDisplay("{TeamOne} vs {TeamTwo}: {TeamOneScore}:{TeamTwoScore}")]
    public class Match : IMatch
    {
        private DB.Models.Match _match;
        internal Match(DB.Models.Match match)
        {
            _match = match;
        }
        public ITeam TeamOne => throw new NotImplementedException();

        public ITeam TeamTwo => throw new NotImplementedException();

        public IPlayArea PlayArea => throw new NotImplementedException();

        public DateTime Start => throw new NotImplementedException();

        public DateTime End => throw new NotImplementedException();

        public int? TeamOneScore => throw new NotImplementedException();

        public int? TeamTwoScore => throw new NotImplementedException();

        public DateTime StartTime => throw new NotImplementedException();

        public DateTime EndTime => throw new NotImplementedException();

        public MatchState MatchState => throw new NotImplementedException();

        public IMatch Successor => throw new NotImplementedException();

        public IMatch FirstPredecessor => throw new NotImplementedException();

        public IMatch SecondPredecessor => throw new NotImplementedException();

        public int Id => throw new NotImplementedException();

        public ITeam GetLooser()
        {
            throw new NotImplementedException();
        }

        public ITeam GetWinner()
        {
            throw new NotImplementedException();
        }

        public void SetScore(int scoreTeamOne, int scoreTeamTwo)
        {
            throw new NotImplementedException();
        }

        void IMatch.End()
        {
            throw new NotImplementedException();
        }

        void IMatch.Start()
        {
            throw new NotImplementedException();
        }
    }
}
