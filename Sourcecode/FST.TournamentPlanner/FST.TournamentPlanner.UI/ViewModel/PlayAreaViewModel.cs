using FST.TournamentPlanner.UI.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class PlayAreaViewModel : ViewModelBase<Model.Models.PlayArea>
    {
        private Model.Models.Tournament _tournament;
        public PlayAreaViewModel(Model.Models.Tournament tournament, Model.Models.PlayArea playArea) : base(playArea)
        {
            _tournament = tournament;
            _name = playArea.Name;
            _description = playArea.Description;
        }

        #region Id
        public int Id
        {
            get
            {
                return Model.Id.Value;
            }
        }
        #endregion

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
                        var newModel = new Model.Models.PlayArea(Model.Id, value, Description);
                        App.RestClient.UpdatePlayAreaWithHttpMessagesAsync(_tournament.Id.Value, newModel.Id.Value, newModel);
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

        #region Description
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        var newModel = new Model.Models.PlayArea(Model.Id, Name, _description);
                        App.RestClient.UpdatePlayAreaWithHttpMessagesAsync(_tournament.Id.Value, newModel.Id.Value, newModel);
                        _description = value;
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
                RaisePropertyChanged(() => Description);
            }
        }
        #endregion

        public bool Equals(PlayAreaViewModel obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj.Model.Id == Model.Id;
        }
    }
}
