using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FST.TournamentPlanner.API.Models;
using FST.TournamentPlanner.API.Repositories;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult SetScore(int matchId, int scoreOne, int scoreTwo)
        {
            DbModels.Match match = this._repoWrapper.Match.GetById(matchId);
            if (match == null)
            {
                return new NotFoundResult();
            }
            if (match.State == DbModels.MatchState.Finished)
            {
                return new BadRequestResult();
            }
            if (match.TeamOne == null || match.TeamOne.Team == null || match.TeamTwo == null || match.TeamTwo.Team == null)
            {
                return new BadRequestResult();
            }
            if (match.State == DbModels.MatchState.Planned)
            {
                match.State = DbModels.MatchState.Started;
            }
            match.TeamOne.Score = scoreOne;
            match.TeamTwo.Score = scoreTwo;

            this._repoWrapper.Match.SaveChanges();

            return new OkResult();

        }

        public ActionResult<Match> EndMatch(int tournamentId, int matchId)
        {
            DbModels.Tournament tournament = _repoWrapper.Tournament.GetById(tournamentId);
            DbModels.Match match = this._repoWrapper.Match.GetById(matchId);
            if (match == null)
            {
                return new ActionResult<Match>(new NotFoundResult());
            }
            //Can´t end matches, which are allready ended
            if (match.State == DbModels.MatchState.Finished)
            {
                return new ActionResult<Match>(new BadRequestResult() );
            }
            // If teams are not set or they don´t have scores => error
            if (match.TeamOne.Team == null || match.TeamTwo.Team == null || !match.TeamOne.Score.HasValue || !match.TeamTwo.Score.HasValue)
            {
                return new ActionResult<Match>(new BadRequestResult());
            }
            // can´t determinate winner if the score is even...
            if (match.TeamOne.Score == match.TeamTwo.Score)
            {
                return new ActionResult<Match>(new BadRequestResult());
            }
            DB.Models.Team winner = (match.TeamOne.Score > match.TeamTwo.Score) ? match.TeamOne.Team : match.TeamTwo.Team;
            // Winner: TeamOne
            var nextMatchForWinner = new DbModels.MatchResult()
            {
                CreatedAt = DateTime.Now,
                Match = match.Successor,
                Team = winner
            };
            if (match.Successor.TeamOne == null)
            {
                match.Successor.TeamOne = nextMatchForWinner;
            }
            else
            {
                match.Successor.TeamTwo = nextMatchForWinner;
            }
            match.State = DbModels.MatchState.Finished;

            this._repoWrapper.Match.SaveChanges();

            return new ActionResult<Match>(new Models.Match(new Models.Tournament(tournament), match));
        }

        Tournament ITournamentService.Get(int id)
        {
            var tournament = _repoWrapper.Tournament.GetById(id);
            if (tournament == null)
            {
                return null;
            }
            return new Tournament(tournament);
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
        void RemovePlayArea(int tournamentId, PlayArea playArea)
        {

        }
        #endregion

        #region Start

        public IActionResult Start(int tournamentId)
        {
            DbModels.Tournament tournament = _repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }
            if (tournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }
            try
            {
                tournament.State = DbModels.TournamentState.Started;
                GenerateMatchPlan(tournament);
                _repoWrapper.Tournament.SaveChanges();
                _repoWrapper.PlayAreaBooking.SaveChanges();
                return new OkResult();
            }
            catch (Exception e)
            {
                tournament.State = DbModels.TournamentState.Created;
                return new BadRequestObjectResult(e.Message);
            }
        }
        private void GenerateMatchPlan(DbModels.Tournament tournament)
        {
            //
            // Generate match tree
            //
            int depth = (int)Math.Log(tournament.TeamCount, 2);
            DbModels.Match finalMatch = new DbModels.Match() { CreatedAt = DateTime.Now };
            GenerateMatchTree(finalMatch, depth - 1);
            //
            // gather list of matches per round
            //
            var matchesPerRound = GenerateRoundLists(finalMatch);
            // 
            // Add matches to tournament
            //
            tournament.Matches = new List<DbModels.Match>();
            matchesPerRound.ToList().ForEach(kv =>
            {
                kv.Value.ForEach(m => {
                    tournament.Matches.Add(m);
                });
            });
            _repoWrapper.Tournament.SaveChanges();
            //
            // Assign play area booking to each match
            //
            matchesPerRound.OrderByDescending(l => l.Key).ToList().ForEach(l => l.Value.ForEach(m =>
            {
                m.PlayAreaBooking = CreateBookingForPlayArea(tournament);
                //little work-around: without this save, the next call of CreateBookingForPlayArea will not determinate the correct next slot
                _repoWrapper.Tournament.SaveChanges();
            })
            );
            //
            // randomize team list for fairness
            //
            //TODO: Shuffle wieder einschalten
            List<DbModels.Team> teams = tournament.Teams.ToList(); //.ShuffleToNewList();
            //
            // assign teams to matches
            //
            var firstRoundMatches = matchesPerRound.GetValueOrDefault(matchesPerRound.Keys.Max());
            for (int i = 0; i < firstRoundMatches.Count; i++)
            {
                firstRoundMatches[i].TeamOne = new DbModels.MatchResult() { Team = teams[i * 2], CreatedAt = DateTime.Now };
                firstRoundMatches[i].TeamTwo = new DbModels.MatchResult() { Team = teams[i * 2 + 1], CreatedAt = DateTime.Now };
            }
        }

        private void GenerateMatchTree(DbModels.Match match, int depth)
        {
            depth--;

            // Generate predecessors
            if (match.Predecessors == null)
            {
                match.Predecessors = new List<DbModels.Match>();
            }

            DbModels.Match preMatchOne = new DbModels.Match() { Successor = match, CreatedAt = DateTime.Now };
            match.Predecessors.Add(preMatchOne);
            DbModels.Match preMatchTwo = new DbModels.Match() { Successor = match, CreatedAt = DateTime.Now };
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

        protected DbModels.PlayAreaBooking CreateBookingForPlayArea(DbModels.Tournament tournament)
        {
            // Get the earliest available time slot on any play area
            var currentAvailableTimeSlot = new List<KeyValuePair<DbModels.PlayArea, DateTime>>();
            tournament.PlayAreas.ToList().ForEach(pa =>
            {
                var bookings = _repoWrapper.PlayAreaBooking.Filter(b => b.PlayArea.Id == pa.Id);
                // In case there are no bookings for the current play area yet, the start time of the tournament is used
                if (bookings == null ||bookings.Count() == 0)
                {
                    currentAvailableTimeSlot.Add(new KeyValuePair<DbModels.PlayArea, DateTime>(pa, tournament.Start));
                }
                else
                {
                    // otherwise: take the end time of the max booking as start time for this one
                    currentAvailableTimeSlot.Add(new KeyValuePair<DbModels.PlayArea, DateTime>(pa, bookings.Max(b => b.End)));
                }
            });
            var earliestAvailablePlayArea = currentAvailableTimeSlot.OrderBy(c => c.Value).First();
            var booking = new DbModels.PlayAreaBooking()
            {
                CreatedAt = DateTime.Now,
                Start = earliestAvailablePlayArea.Value,
                End = earliestAvailablePlayArea.Value.AddMinutes(tournament.MatchDuration),
                PlayArea = earliestAvailablePlayArea.Key
            };
            return booking;
            
        }

        private void GenerateRoundListRecursion(Dictionary<int, List<DbModels.Match>> matchList, DbModels.Match parentMatch, int round)
        {
            List<DbModels.Match> matchesThisRound;
            if (!matchList.TryGetValue(round, out matchesThisRound))
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
                });
            }

        }

        /// <summary>
        /// Rerurn a match for a tournament
        /// </summary>
        /// <param name="torunamentId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public ActionResult<Match> GetMatch(int torunamentId, int matchId)
        {
            DbModels.Match match = this._repoWrapper.Match.GetById(matchId);
            if (match == null)
            {
                return new ActionResult<Match>(new NotFoundResult());
            }
           
            return new ActionResult<Match>(new Match(new Tournament(this._repoWrapper.Tournament.GetById(torunamentId)), match));
        }

        public ActionResult<PlayArea> AddPlayArea(int tournamentId, string name, string description)
        {
            DbModels.Tournament tournament = _repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }
            
            // Only update while tournament not started
            if (tournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }

            DbModels.PlayArea playarea = new DbModels.PlayArea
            {
                Tournament = tournament,
                Description = description,
                Name = name
            };

            this._repoWrapper.PlayArea.Create(playarea);
            this._repoWrapper.PlayArea.SaveChanges();

            return new ActionResult<PlayArea>(new PlayArea(playarea));
        }

        public ActionResult<PlayArea> GetPlayArea(int tournamentId, int playAreaId)
        {
            DbModels.Tournament tournament = _repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            DbModels.PlayArea playarea = _repoWrapper.PlayArea.GetById(playAreaId);
            if (playarea == null)
            {
                return new NotFoundResult();
            }

            return new ActionResult<PlayArea>(new PlayArea(playarea));
        }

        #endregion
    }
}
