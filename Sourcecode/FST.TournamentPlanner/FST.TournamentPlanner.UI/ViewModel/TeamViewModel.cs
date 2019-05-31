using FST.TournamentPlanner.UI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class TeamViewModel : ViewModelBase<Team>
    {
        public TeamViewModel(Team team) : base(team)
        {

        }

        public string Name => _model?.Name;

        public bool Equals(TeamViewModel obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj._model.Id == _model.Id;
        }

        //public static bool operator ==(TeamViewModel team1, TeamViewModel team2)
        //{
        //    if (team1 == null && team2 == null)
        //    {
        //        return true;
        //    }
        //    if (team1 == null)
        //    {
        //        return false;
        //    }
        //    return team1.Equals(team2);
        //}


        //public static bool operator !=(TeamViewModel team1, TeamViewModel team2)
        //{
        //    return !team1.Equals(team2);
        //}
    }
}
