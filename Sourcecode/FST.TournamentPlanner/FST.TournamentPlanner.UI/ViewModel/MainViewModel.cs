using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

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
        private TournamentViewModel _currentTournament;
        public TournamentViewModel CurrentTournament
        {
            get
            {
                return _currentTournament;
            }
            set
            {
                if ((_currentTournament == null && value == null) || (_currentTournament != null && _currentTournament.Equals(value)))
                {
                    //equal
                    return;
                }
                _currentTournament = value;
                RaisePropertyChanged(() => CurrentTournament);
            }
        }

        #endregion
    }
}