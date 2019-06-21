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
    /// <summary>
    /// Business Logic
    /// </summary>
    public class TournamentService : ITournamentService
    {
        private readonly IRepositoryWrapper _repoWrapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repoWrapper"></param>
        public TournamentService(IRepositoryWrapper repoWrapper)
        {
            this._repoWrapper = repoWrapper;
        }

        #region Tournament Handling

        /// <summary>
        /// Get a specific tournament
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tournament Get(int id)
        {
            DB.Models.Tournament tournament = this._repoWrapper.Tournament.GetById(id);
            if (tournament == null)
            {
                return null;
            }
            return new Tournament(tournament);
        }

        /// <summary>
        /// Get all tournaments
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tournament> GetAll()
        {
            return this._repoWrapper.Tournament.GetAll().Select(t => new Tournament(t)).ToList();
        }

        Tournament ITournamentService.Get(int id)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(id);
            if (tournament == null)
            {
                return null;
            }
            return new Tournament(tournament);
        }

        /// <summary>
        /// Add a new tournament
        /// </summary>
        /// <param name="tournament"></param>
        /// <returns></returns>
        public ActionResult<Tournament> AddTournament(Tournament tournament)
        {
            DbModels.Tournament tmnt = new DbModels.Tournament
            {
                Name = tournament.Name,
                Description = tournament.Description,
                MatchDuration = tournament.MatchDuration,
                Start = tournament.StartTime,
                State = DbModels.TournamentState.Created,
                TeamCount = tournament.TeamCount,
            };

            this._repoWrapper.Tournament.Create(tmnt);
            this._repoWrapper.Tournament.SaveChanges();

            this._repoWrapper.Tournament.SaveChanges();

            return new ActionResult<Tournament>(new Tournament(tmnt));
        }

        /// <summary>
        /// remove a tournament
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <returns></returns>
        public IActionResult RemoveTournament(int tournamentId)
        {
            DbModels.Tournament dbTournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (dbTournament == null)
            {
                return new NotFoundResult();
            }

            // Only update while tournament not started
            if (dbTournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }

            this._repoWrapper.Tournament.Delete(dbTournament);
            this._repoWrapper.Tournament.SaveChanges();

            return new OkResult();
        }

        /// <summary>
        /// Update Tournament
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="tournament"></param>
        /// <returns></returns>
        public IActionResult UpdateTournament(int tournamentId, Tournament tournament)
        {
            DbModels.Tournament dbTournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (dbTournament == null)
            {
                return new NotFoundResult();
            }

            // Only update while tournament not started
            if (dbTournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }

            dbTournament.Name = tournament.Name;
            dbTournament.MatchDuration = tournament.MatchDuration;
            dbTournament.Description = tournament.Description;
            dbTournament.Start = tournament.StartTime;
            dbTournament.TeamCount = tournament.TeamCount;

            this._repoWrapper.Team.SaveChanges();

            return new OkResult();
        }

        #endregion

        #region Start

        /// <summary>
        /// Start tournament
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <returns></returns>
        public IActionResult Start(int tournamentId)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
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
                this.GenerateMatchPlan(tournament);
                this._repoWrapper.Tournament.SaveChanges();
                this._repoWrapper.PlayAreaBooking.SaveChanges();
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
            this.GenerateMatchTree(finalMatch, depth - 1);
            //
            // gather list of matches per round
            //
            Dictionary<int, List<DbModels.Match>> matchesPerRound = this.GenerateRoundLists(finalMatch);
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
            this._repoWrapper.Tournament.SaveChanges();
            
            //
            // Assign play area booking to each match
            //
            matchesPerRound.OrderByDescending(l => l.Key).ToList().ForEach(l => l.Value.ForEach(m =>
            {
                m.PlayAreaBooking = this.CreateBookingForPlayArea(tournament);
                //little work-around: without this save, the next call of CreateBookingForPlayArea will not determinate the correct next slot
                this._repoWrapper.Tournament.SaveChanges();
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
            List<DbModels.Match> firstRoundMatches = matchesPerRound.GetValueOrDefault(matchesPerRound.Keys.Max());
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
                this.GenerateMatchTree(preMatchOne, depth);
                this.GenerateMatchTree(preMatchTwo, depth);
            }
        }

        /// <summary>
        /// Convert the tree of matches to a directory, where the key is the current play round and the value is the list of all matches within the round
        /// </summary>
        /// <param name="finalMatch">root element to start navigation from</param>
        /// <returns>dictionary</returns>
        private Dictionary<int, List<DbModels.Match>> GenerateRoundLists(DbModels.Match finalMatch)
        {
            Dictionary<int, List<DbModels.Match>> result = new Dictionary<int, List<DbModels.Match>>();
            this.GenerateRoundListRecursion(result, finalMatch, 0);
            return result;
        }

        /// <summary>
        /// create bookings
        /// </summary>
        /// <param name="tournament"></param>
        /// <returns></returns>
        protected DbModels.PlayAreaBooking CreateBookingForPlayArea(DbModels.Tournament tournament)
        {
            // Get the earliest available time slot on any play area
            List<KeyValuePair<DbModels.PlayArea, DateTime>> currentAvailableTimeSlot = new List<KeyValuePair<DbModels.PlayArea, DateTime>>();
            tournament.PlayAreas.ToList().ForEach(pa =>
            {
                IEnumerable<DbModels.PlayAreaBooking> bookings = this._repoWrapper.PlayAreaBooking.Filter(b => b.PlayArea.Id == pa.Id);
                // In case there are no bookings for the current play area yet, the start time of the tournament is used
                if (bookings == null || (bookings != null && bookings.Count() == 0))
                {
                    currentAvailableTimeSlot.Add(new KeyValuePair<DbModels.PlayArea, DateTime>(pa, tournament.Start));
                }
                else
                {
                    // otherwise: take the end time of the max booking as start time for this one
                    currentAvailableTimeSlot.Add(new KeyValuePair<DbModels.PlayArea, DateTime>(pa, bookings.Max(b => b.End)));
                }
            });
            KeyValuePair<DbModels.PlayArea, DateTime> earliestAvailablePlayArea = currentAvailableTimeSlot.OrderBy(c => c.Value).First();

            DbModels.PlayAreaBooking booking = new DbModels.PlayAreaBooking()
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
            if (!matchList.TryGetValue(round, out List<DbModels.Match> matchesThisRound))
            {
                matchesThisRound = new List<DbModels.Match>();
                matchList.Add(round, matchesThisRound);
            }

            matchesThisRound.Add(parentMatch);
            if (parentMatch.Predecessors != null)
            {
                parentMatch.Predecessors.ToList().ForEach(c =>
                {
                    this.GenerateRoundListRecursion(matchList, c, round + 1);
                });
            }

        }

        #endregion

        #region Match Handling

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

        /// <summary>
        /// Set or update a score on match 
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="scoreOne"></param>
        /// <param name="scoreTwo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// End a match
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public ActionResult<Match> EndMatch(int tournamentId, int matchId)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            DbModels.Match match = this._repoWrapper.Match.GetById(matchId);
            if (match == null)
            {
                return new ActionResult<Match>(new NotFoundResult());
            }
            //Can´t end matches, which are allready ended
            if (match.State == DbModels.MatchState.Finished)
            {
                return new ActionResult<Match>(new BadRequestResult());
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
            DbModels.MatchResult nextMatchForWinner = new DbModels.MatchResult()
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

        #endregion

        #region PlayArea Handling
        
        /// <summary>
        /// Add a new play area
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public ActionResult<PlayArea> AddPlayArea(int tournamentId, string name, string description)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
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

        /// <summary>
        /// Get an existing play area
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="playAreaId"></param>
        /// <returns></returns>
        public ActionResult<PlayArea> GetPlayArea(int tournamentId, int playAreaId)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            DbModels.PlayArea playarea = this._repoWrapper.PlayArea.GetById(playAreaId);
            if (playarea == null)
            {
                return new NotFoundResult();
            }

            return new ActionResult<PlayArea>(new PlayArea(playarea));
        }

        /// <summary>
        /// Remove the given play area from the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="DbModels.TournamentState"/>
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="playAreaId"></param>
        /// <returns></returns>
        public IActionResult RemovePlayArea(int tournamentId, int playAreaId)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            // Only update while tournament not started
            if (tournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }

            DbModels.PlayArea playarea = this._repoWrapper.PlayArea.GetById(playAreaId);
            if (playarea == null)
            {
                return new NotFoundResult();
            }

            this._repoWrapper.PlayArea.Delete(playarea);
            this._repoWrapper.PlayArea.SaveChanges();

            return new OkResult();
        }

        /// <summary>
        /// update a play area
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="playAreaId"></param>
        /// <param name="playArea"></param>
        /// <returns></returns>
        public IActionResult UpdatePlayArea(int tournamentId, int playAreaId, Models.PlayArea playArea)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            // Only update while tournament not started
            if (tournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }

            DbModels.PlayArea dbPlayArea = this._repoWrapper.PlayArea.GetById(playAreaId);
            if (dbPlayArea == null)
            {
                return new NotFoundResult();
            }

            dbPlayArea.Name = playArea.Name;
            dbPlayArea.Description = playArea.Description;

            this._repoWrapper.PlayArea.SaveChanges();

            return new OkResult();
        }

        #endregion

        #region Team Handling

        /// <summary>
        /// Update an existing team
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="teamId"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public ActionResult<Team> UpdateTeam(int tournamentId, int teamId, Team team)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            // Only update while tournament not started
            if (tournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }

            DbModels.Team dbTeam = this._repoWrapper.Team.GetById(teamId);
            if (dbTeam == null)
            {
                return new NotFoundResult();
            }

            dbTeam.Name = team.Name;

            this._repoWrapper.Team.SaveChanges();

            return new OkResult();
        }

        /// <summary>
        /// Remove a team
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public ActionResult RemoveTeam(int tournamentId, int teamId)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            // Only update while tournament not started
            if (tournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }

            DbModels.Team dbTeam = this._repoWrapper.Team.GetById(teamId);
            if (dbTeam == null)
            {
                return new NotFoundResult();
            }

            this._repoWrapper.Team.Delete(dbTeam);
            this._repoWrapper.Team.SaveChanges();

            return new OkResult();
        }

        /// <summary>
        /// Get a team
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public ActionResult<Team> GetTeam(int tournamentId, int teamId)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            DbModels.Team dbTeam = this._repoWrapper.Team.GetById(teamId);
            if (dbTeam == null)
            {
                return new NotFoundResult();
            }

            return new ActionResult<Team>(new Team(dbTeam));
        }

        /// <summary>
        /// Add a new team
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult<Team> AddTeam(int tournamentId, string name)
        {
            DbModels.Tournament tournament = this._repoWrapper.Tournament.GetById(tournamentId);
            if (tournament == null)
            {
                return new NotFoundResult();
            }

            // Only update while tournament not started
            if (tournament.State != DbModels.TournamentState.Created)
            {
                return new BadRequestObjectResult("Tournament allready started of finished");
            }


            DbModels.Team team = new DbModels.Team
            {
                Name = name
            };

            this._repoWrapper.Team.Create(team);
            this._repoWrapper.Team.SaveChanges();

            tournament.Teams.Add(team);
            this._repoWrapper.Tournament.SaveChanges();

            return new ActionResult<Team>(new Team(team));
        }

        #endregion
    }
}
