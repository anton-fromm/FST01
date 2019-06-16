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

        public int? Id => _model?.Id.Value;

        public string Name
        {
            get
            {
                return _model.Name;
            }
            set
            {
                _model.Name = value;
                //App.RestClient.Team
            }
        }

        public bool Equals(TeamViewModel obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj._model.Id == _model.Id;
        }

    }
}
