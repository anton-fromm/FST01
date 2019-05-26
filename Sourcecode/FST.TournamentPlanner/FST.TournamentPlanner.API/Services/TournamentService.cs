using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FST.TournamentPlanner.API.Contracts;
using FST.TournamentPlanner.API.Model;

namespace FST.TournamentPlanner.API.Services
{
    public class TournamentService : ITournamentService
    {
        private IRepositoryWrapper _repoWrapper;

        public TournamentService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public Tournament GenerateMatchPlan()
        {
            DB.Models.Tournament tournament = _repoWrapper.Tournament.GetById(1);

            //
            // Generate match tree
            //
            tournament.State = DB.Models.TournamentState.Created;


            _repoWrapper.Tournament.SaveChanges();

            //Map DB Model to local outputmodel

            return null;
        }
    }
}
