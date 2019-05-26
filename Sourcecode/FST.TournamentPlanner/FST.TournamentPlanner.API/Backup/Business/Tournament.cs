using FST.TournamentPlanner.API.Contracts;
using FST.TournamentPlanner.API;
using db = FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Business
{
    [DebuggerDisplay("{Name} - {TeamCount} Teams - {PlayAreas.Count} PlayAreas")]
    public class Tournament : ITournament
    {
        private TournamentPlanner.DB.Models.Tournament _tournamentDbObject;
        protected Tournament(TournamentPlanner.DB.Models.Tournament tournament)
        {
            _tournamentDbObject = tournament;
        }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime StartTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MatchDuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TeamCount
        {
            get
            {
                return _tournamentDbObject.TeamCount;
            }
            set
            {
                if (Math.Log(value, 2) != 0.0)
                {
                    throw new InvalidOperationException("value must be expressable as 2^x");
                }
                _tournamentDbObject.TeamCount = value;
            }

        }

        public db.TournamentState State
        {
            get
            {
                return _tournamentDbObject.State;
            }
            private set
            {
                //validate team count against existing teams
                if (Teams.Count != TeamCount)
                {
                    throw new InvalidOperationException("Registered teams count does not match tournament team count");
                }
                if (PlayAreas.Count == 0)
                {
                    throw new InvalidOperationException("No play areas defined yet!");
                }
                throw new NotImplementedException();
            }
        }

        public ICollection<IPlayArea> PlayAreas => throw new NotImplementedException();

        public ICollection<ITeam> Teams => throw new NotImplementedException();


        public IPlayArea AddPlayArea(string name, string description)
        {
            throw new NotImplementedException();
        }

        public IPlayArea AddPlayArea(IPlayArea playArea)
        {
            throw new NotImplementedException();
        }

        public ITeam AddTeam(string name)
        {
            throw new NotImplementedException();
        }

        public ITeam AddTeam(ITeam team)
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<IMatch> Matches => throw new NotImplementedException();


        public IMatch RootMatch
        {
            get
            {
                db.Match match = _tournamentDbObject.Matches.SingleOrDefault(m => m.Successor == null);
                if (match == null)
                {
                    return null;
                }
                return new Match(match);
            }
        }

        public int Id => throw new NotImplementedException();

        public void RemovePlayArea(IPlayArea playArea)
        {
            throw new NotImplementedException();
        }

        public void RemoveTeam(ITeam team)
        {
            throw new NotImplementedException();
        }

        public void RemoveTeam(int id)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            if (State != db.TournamentState.Created)
            {
                throw new InvalidOperationException("Tournament already started or finished");
            }
            State = db.TournamentState.Started;
            GenerateMatchPlan();
        }

        protected db.PlayAreaBooking CreateBookingForPlayArea()
        {
            throw new NotImplementedException();
        }

        private void GenerateMatchPlan()
        {
            //
            // Generate match tree
            //
            int depth = (int) Math.Log(TeamCount, 2);
            db.Match finalMatch = new db.Match();
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
            List<db.Team> teams = _tournamentDbObject.Teams.ToList().ShuffleToNewList();
            //
            //assign teams to matches
            //
            var firstRoundMatches = matchesPerRound.GetValueOrDefault(matchesPerRound.Keys.Max());
            for (int i = 0; i < firstRoundMatches.Count; i++)
            {
                firstRoundMatches[i].TeamOne = new db.MatchResult() { Team = teams[i * 2], CreatedAt = DateTime.Now };
                firstRoundMatches[i].TeamTwo = new db.MatchResult() { Team = teams[i * 2 + 1], CreatedAt = DateTime.Now };
            }
        }

        private void GenerateMatchTree(db.Match match, int depth)
        {
            depth--;
            // Generate predecessors
            var preMatchOne = new db.Match();
            match.Predecessors.Add(preMatchOne);
            var preMatchTwo = new db.Match();
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
        private Dictionary<int, List<db.Match>> GenerateRoundLists(db.Match finalMatch)
        {
            var result = new Dictionary<int, List<db.Match>>();
            GenerateRoundListRecursion(result, finalMatch, 0);
            return result;
        }

        private void GenerateRoundListRecursion(Dictionary<int, List<db.Match>> matchList, db.Match parentMatch, int round)
        {
            List<db.Match> matchesThisRound;
            if (matchList.TryGetValue(round, out matchesThisRound))
            {
                matchesThisRound = new List<db.Match>();
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
    }
}
