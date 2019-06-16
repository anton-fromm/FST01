using FST.TournamentPlanner.UI.Model;
using FST.TournamentPlanner.UI.Properties;
using FST.TournamentPlanner.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    internal class ViewModelLocator
    {
        private static ModelClient _restClient;

        private ViewModelLocator()
        {
            _restClient = new ModelClient(new AnonymousCredential())
            {
                BaseUri = new Uri(Settings.Default.TournamentApi)
            };

        }

        #region Singleton
        private static ViewModelLocator _instance;
        public static ViewModelLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ViewModelLocator();
                }
                return _instance;
            }
        }
        #endregion

        #region GetTournamentViewModel
        private Dictionary<int, TournamentViewModel> _tournament = new Dictionary<int, TournamentViewModel>();
        internal TournamentViewModel GetTournamentViewModel(Model.Models.Tournament tournament)
        {
            if (tournament == null)
            {
                return null;
            }
            TournamentViewModel tournamentViewModel;
            if (!_tournament.TryGetValue(tournament.Id.Value, out tournamentViewModel))
            {
                tournamentViewModel = new TournamentViewModel(tournament);
                _tournament.Add(tournament.Id.Value, tournamentViewModel);
            }
            return tournamentViewModel;
        }
        #endregion
        #region GetTeamViewModel
        private Dictionary<int, TeamViewModel> _teams = new Dictionary<int, TeamViewModel>();
        internal TeamViewModel GetTeamViewModel(Model.Models.Team team)
        {
            if (team == null)
            {
                return null;
            }
            TeamViewModel teamViewModel;
            if (!_teams.TryGetValue(team.Id.Value, out teamViewModel))
            {
                teamViewModel = new TeamViewModel(team);
                _teams.Add(team.Id.Value, teamViewModel);
            }
            return teamViewModel;
        }
        #endregion

        #region GetPlayAreaViewModel
        private Dictionary<int, PlayAreaViewModel> _playAreas = new Dictionary<int, PlayAreaViewModel>();
        internal PlayAreaViewModel GetPlayAreaViewModel(Model.Models.Tournament tournament, Model.Models.PlayArea playArea)
        {
            PlayAreaViewModel playAreaViewModel;
            if (!_playAreas.TryGetValue(playArea.Id.Value, out playAreaViewModel))
            {
                playAreaViewModel = new PlayAreaViewModel(tournament, playArea);
                _playAreas.Add(playArea.Id.Value, playAreaViewModel);
            }
            return playAreaViewModel;
        }
        #endregion

        #region GetMatchViewModel
        private Dictionary<int, MatchViewModel> _matches = new Dictionary<int, MatchViewModel>();
        internal MatchViewModel GetMatchViewModel(Model.Models.Tournament tournament, Model.Models.Match match, MatchViewModel successor)
        {
            MatchViewModel matchViewModel;
            if (!_matches.TryGetValue(match.Id.Value, out matchViewModel))
            {
                matchViewModel = new MatchViewModel(tournament, match, successor);
                _matches.Add(match.Id.Value, matchViewModel);
            }
            return matchViewModel;
        }
        #endregion
    }
}
