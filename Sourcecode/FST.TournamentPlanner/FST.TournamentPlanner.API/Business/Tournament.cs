using FST.TournamentPlanner.API.Contracts;
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
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime StartTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MatchDuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TeamCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TournamentState TournamentState => throw new NotImplementedException();

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

        public ICollection<IMatch> GetMatches()
        {
            throw new NotImplementedException();
        }

        public IMatch GetRootMatch()
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }
    }
}
