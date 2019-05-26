using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FST.TournamentPlanner.API.Models;
using FST.TournamentPlanner.API.Repositories;

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

            return new Tournament(_repoWrapper.Tournament.GetById(1));
        }

        public Tournament Get(int id)
        {
            DB.Models.Tournament tournament = _repoWrapper.Tournament.GetById(id);
            if (tournament == null)
            {
                return null;
            }
            return new Tournament(tournament);
        }

        public IEnumerable<Models.Tournament> GetAll()
        {
            var bla = _repoWrapper.Tournament.GetAll().ToList();
            var blubb = bla.Select(t => new Tournament(t));
            return blubb;
        }

        Tournament ITournamentService.Get(int id)
        {
            throw new NotImplementedException();
        }

        #region PlayArea stuff

        /// <summary>
        /// Adds a new play area to this tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="name">name of the play area</param>
        /// <param name="description">description of the play area</param>
        //PlayArea AddPlayArea(string name, string description);

        /// <summary>
        /// Adds a new play area to this tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="playArea">play area to add</param>
        //PlayArea AddPlayArea(PlayArea playArea)
        //{

        //}

        /// <summary>
        /// Remove the given play area from the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="playArea">play area to remove</param>
        void RemovePlayArea(PlayArea playArea)
        {

        }
        #endregion
    }
}
