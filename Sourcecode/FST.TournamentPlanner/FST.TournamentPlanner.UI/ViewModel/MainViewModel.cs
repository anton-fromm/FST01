using FST.TournamentPlanner.UI.ViewModel.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FST.TournamentPlanner.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                CurrentDocument = new TournamentViewModel(CreateDesignTimeVm());
                OpenedDocuments.Add(CurrentDocument);
            }
            else
            {
                Tournaments.Add(new TournamentViewModel(App.RestClient.GetWithHttpMessagesAsync(1).Result.Body));
                //OpenedDocuments.Add(Tournaments[0]);
                //CurrentDocument = Tournaments[0];
            }

            MessengerInstance.Register<OpenTournamentMessage>(this, m =>
            {
                OpenedDocuments.Add(m.Tournament);
                CurrentDocument = m.Tournament;
            });

        }

        private ObservableCollection<TournamentViewModel> _tournaments;
        public ObservableCollection<TournamentViewModel> Tournaments
        {
            get
            {
                if (_tournaments == null)
                {
                    _tournaments = new ObservableCollection<TournamentViewModel>();
                }
                return _tournaments;
            }
        }
        public ObservableCollection<Object> OpenedDocuments { get; } = new ObservableCollection<Object>();

        #region OpenTournamentCommand
        private RelayCommand _openTournamentCommand;
        public RelayCommand OpenTournamentCommand
        {
            get
            {
                if (_openTournamentCommand == null)
                {
                    _openTournamentCommand = new RelayCommand(() =>
                    {
                    },
                    true);
                }
                return _openTournamentCommand;
            }
        }
        #endregion

        #region CurrentTournament
        private Object _currentDocument;
        public Object CurrentDocument
        {
            get
            {
                return _currentDocument;
            }
            set
            {
                if ((_currentDocument == null && value == null) || (_currentDocument != null && _currentDocument.Equals(value)))
                {
                    //equal
                    return;
                }
                _currentDocument = value;
                RaisePropertyChanged(() => CurrentDocument);
            }
        }

        #endregion

        private Model.Models.Tournament CreateDesignTimeVm()
        {
            #region Dummy PlayAreas
            var playAreas = new List<Model.Models.PlayArea>();
            playAreas.Add(new Model.Models.PlayArea(1, "Area 1", "Play Area 1"));
            playAreas.Add(new Model.Models.PlayArea(1, "Area 2", "Play Area 2"));
            #endregion

            #region Dummy Teams
            var teams = new List<Model.Models.Team>();
            teams.Add(new Model.Models.Team(1, "Team 1"));
            teams.Add(new Model.Models.Team(2, "Team 2"));
            teams.Add(new Model.Models.Team(3, "Team 3"));
            teams.Add(new Model.Models.Team(4, "Team 4"));
            teams.Add(new Model.Models.Team(5, "Team 5"));
            teams.Add(new Model.Models.Team(6, "Team 6"));
            teams.Add(new Model.Models.Team(7, "Team 7"));
            teams.Add(new Model.Models.Team(8, "Team 8"));
            #endregion
            
            var finalMatch = new Model.Models.Match(1, new Model.Models.Team(1, "Team 1"), new Model.Models.Team(2, "Team 2"), playAreas[0], DateTime.Now, DateTime.Now.AddMinutes(60), null, null, 2, null, null, null, null);
            var tournament = new Model.Models.Tournament(1, "Ping-Pong", "nur die Harten kommen in den Garten...", DateTime.Now, 60, 8, 1, playAreas, teams, finalMatch);
            return tournament;
        }
    }
}