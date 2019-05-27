using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FST.TournamentPlanner.API.Models;
using FST.TournamentPlanner.API.Repositories;
using DbModels = FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Services
{
    public class TournamentService : ITournamentService
    {
        private IRepositoryWrapper _repoWrapper;

        public TournamentService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public Tournament GenerateMatchPlan(int id)
        {
            DbModels.Tournament tournament = _repoWrapper.Tournament.GetById(id);
                                          
            //
            // Generate match tree
            //
            int depth = (int)Math.Log(tournament.TeamCount, 2);
            DbModels.Match finalMatch = new DbModels.Match();
            GenerateMatchTree(finalMatch, depth - 1);
            //
            // gather list of matches per round
            //
            var matchesPerRound = GenerateRoundLists(finalMatch);
            //
            // Assign play area booking to each match
            //
            matchesPerRound.OrderByDescending(l => l.Key).ToList().ForEach(l => l.Value.ForEach(m => m.PlayAreaBooking = CreateBookingForPlayArea()));
            //
            // randomize team list for fairness
            //
            List<DbModels.Team> teams = tournament.Teams.ToList().ShuffleToNewList();
            //
            //assign teams to matches
            //
            var firstRoundMatches = matchesPerRound.GetValueOrDefault(matchesPerRound.Keys.Max());
            for (int i = 0; i < firstRoundMatches.Count; i++)
            {
                firstRoundMatches[i].TeamOne = new DbModels.MatchResult() { Team = teams[i * 2], CreatedAt = DateTime.Now };
                firstRoundMatches[i].TeamTwo = new DbModels.MatchResult() { Team = teams[i * 2 + 1], CreatedAt = DateTime.Now };
            }
            
            _repoWrapper.Tournament.SaveChanges();

            return new Tournament(_repoWrapper.Tournament.GetById(id));
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

        #region

        private void GenerateMatchTree(DbModels.Match match, int depth)
        {
            depth--;
            // Generate predecessors
            var preMatchOne = new DbModels.Match();
            match.Predecessors.Add(preMatchOne);
            var preMatchTwo = new DbModels.Match();
            match.Predecessors.Add(preMatchTwo);

            // Generate next level
            if (depth > 0)
            {
                GenerateMatchTree(preMatchOne, depth);
                GenerateMatchTree(preMatchTwo, depth);
            }
        }

        /// <summary>
        /// Convert the tree of matches to a directory, where the key is the current play round and the value is the list of all matches within the round
        /// </summary>
        /// <param name="finalMatch">root element to start navigation from</param>
        /// <returns>dictionary</returns>
        private Dictionary<int, List<DbModels.Match>> GenerateRoundLists(DbModels.Match finalMatch)
        {
            var result = new Dictionary<int, List<DbModels.Match>>();
            GenerateRoundListRecursion(result, finalMatch, 0);
            return result;
        }

        protected DbModels.PlayAreaBooking CreateBookingForPlayArea()
        {
            throw new NotImplementedException();
        }

        private void GenerateRoundListRecursion(Dictionary<int, List<DbModels.Match>> matchList, DbModels.Match parentMatch, int round)
        {
            List<DbModels.Match> matchesThisRound;
            if (matchList.TryGetValue(round, out matchesThisRound))
            {
                matchesThisRound = new List<DbModels.Match>();
                matchList.Add(round, matchesThisRound);
            }
            matchesThisRound.Add(parentMatch);
            if (parentMatch.Predecessors != null)
            {
                parentMatch.Predecessors.ToList().ForEach(c =>
                {
                    GenerateRoundListRecursion(matchList, c, round + 1);
                    GenerateRoundListRecursion(matchList, c, round + 1);
                });
            }

        }

        #endregion
    }
}
