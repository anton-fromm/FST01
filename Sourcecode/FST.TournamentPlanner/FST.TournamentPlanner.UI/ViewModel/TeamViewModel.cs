using FST.TournamentPlanner.UI.Model.Models;
using FST.TournamentPlanner.UI.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class TeamViewModel : ViewModelBase<Team>
    {
        private Model.Models.Tournament _tournament;
        public TeamViewModel(Model.Models.Tournament tournament, Team team) : base(team)
        {
            _tournament = tournament;
            _name = team.Name;
        }

        public int? Id => Model?.Id.Value;

        #region Name
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        var newModel = new Model.Models.Team(Model.Id, value);
                        App.RestClient.UpdateTeamWithHttpMessagesAsync(_tournament.Id.Value, newModel.Id.Value, newModel);
                        _name = value;
                        Model = newModel;
                    }
                    catch (Exception e)
                    {
                        if (e.GetType() == typeof(AggregateException) || e.GetType() == typeof(Microsoft.Rest.HttpOperationException))
                        {
                            MessengerInstance.Send(new CommunicationErrorMessage());
                        }
                        else
                        {
                            throw e;
                        }
                    }
                }
                RaisePropertyChanged(() => Name);
            }
        }
        #endregion

        public bool Equals(TeamViewModel obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj.Model.Id == Model.Id;
        }

    }
}
