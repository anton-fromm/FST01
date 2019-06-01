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

        #region GetTeamViewModel
        private Dictionary<int, TeamViewModel> _teams = new Dictionary<int, TeamViewModel>();
        internal TeamViewModel GetTeamViewModel(Model.Models.Team team)
        {
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
        internal PlayAreaViewModel GetPlayAreaViewModel(Model.Models.PlayArea playArea)
        {
            PlayAreaViewModel playAreaViewModel;
            if (!_playAreas.TryGetValue(playArea.Id.Value, out playAreaViewModel))
            {
                playAreaViewModel = new PlayAreaViewModel(playArea);
                _playAreas.Add(playArea.Id.Value, playAreaViewModel);
            }
            return playAreaViewModel;
        }
        #endregion


        #region GetMatchViewModel
        private Dictionary<int, MatchViewModel> _matches = new Dictionary<int, MatchViewModel>();
        internal MatchViewModel GetMatchViewModel(Model.Models.Match match, MatchViewModel successor)
        {
            MatchViewModel matchViewModel;
            if (!_matches.TryGetValue(match.Id.Value, out matchViewModel))
            {
                matchViewModel = new MatchViewModel(match, successor);
                _matches.Add(match.Id.Value, matchViewModel);
            }
            return matchViewModel;
        }
        #endregion
    }
}
