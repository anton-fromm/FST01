using FST.TournamentPlanner.API.Contracts;
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
        public ITeam TeamOne => throw new NotImplementedException();

        public ITeam TeamTwo => throw new NotImplementedException();

        public IPlayArea PlayArea => throw new NotImplementedException();

        public DateTime Start => throw new NotImplementedException();

        public DateTime End => throw new NotImplementedException();

        public int? TeamOneScore => throw new NotImplementedException();

        public int? TeamTwoScore => throw new NotImplementedException();

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
    }
}
