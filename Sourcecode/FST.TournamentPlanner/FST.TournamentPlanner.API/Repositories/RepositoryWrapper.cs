using FST.TournamentPlanner.DB.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private PlannerContext _ctx;
        private TournamentRepository _tournament;
        private MatchRepository _match;
        private PlayAreaBookingRepository _playAreaBooking;
        private PlayAreaRepository _playArea;
        private TeamRepository _team;

        public TournamentRepository Tournament
        {
            get
            {
                if (_tournament == null)
                {
                    _tournament = new TournamentRepository(_ctx);
                }

                return _tournament;
            }
        }



        public MatchRepository Match
        {
            get
            {
                if (_match == null)
                {
                    _match = new MatchRepository(_ctx);
                }

                return _match;
            }
        }

        public PlayAreaBookingRepository PlayAreaBooking
        {
            get
            {
                if (_playAreaBooking == null)
                {
                    _playAreaBooking = new PlayAreaBookingRepository(_ctx);
                }
                return _playAreaBooking;
            }
        }

        public PlayAreaRepository PlayArea
        {
            get
            {
                if (_playArea == null)
                {
                    _playArea = new PlayAreaRepository(_ctx);
                }
                return _playArea;
            }
        }

        public TeamRepository Team
        {
            get
            {
                if (_team == null)
                {
                    _team = new TeamRepository(_ctx);
                }
                return _team;
            }
        }

        public RepositoryWrapper(PlannerContext context)
        {
            _ctx = context;
        }
    }
}
