using FST.TournamentPlanner.API.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Business
{
    [DebuggerDisplay("{Name} ({Description})")]
    public class PlayArea : IPlayArea
    {
        private Tournament _tournament;
        protected PlayArea(Tournament tournament)
        {
            _tournament = tournament;
        }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
