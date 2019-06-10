using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;
using msg = FST.TournamentPlanner.UI.ViewModel.Messages;

namespace FST.TournamentPlanner.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private readonly GalaSoft.MvvmLight.Messaging.IMessenger MessengerInstance = GalaSoft.MvvmLight.Messaging.Messenger.Default;

        private const string MESSAGEBOX_TITLE = "Turnierverwaltung";
        private const string TOURNAMENT_STARTED = "Die Änderung der Turnierstammdaten ist nach Beginn des Turnieres nicht mehr möglich.";
        private const string TEAMCOUNT_MISMATCH = "Die Anzahl der registrierten Teams stimmt nicht mit der Teilnehmeranzahl überein!\nIst: {0}\nSoll: {1}";
        private const string COMMUNICATION_ERROR = "Fehler bei der Kommunikation mit dem Webdienst";
        private const string MATCH_FINISHED = "";

        public MainWindow()
        {
            InitializeComponent();

            MessengerInstance.Register<msg.TournamentAllreadyStarted>(this, false, (m) => 
            {
                MessageBox.Show(this, TOURNAMENT_STARTED, MESSAGEBOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            });
            MessengerInstance.Register<msg.TeamCountMismatchMessage>(this, false, (m) => 
            {
                MessageBox.Show(this, string.Format(TEAMCOUNT_MISMATCH, m.CurrentTeamCount, m.TournamentSize), MESSAGEBOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
            });
            MessengerInstance.Register<msg.MatchFinishedMessage>(this, false, (m) =>
            {
                MessageBox.Show(this, MATCH_FINISHED, MESSAGEBOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            });
            MessengerInstance.Register<msg.AreYouSureMessage>(this, (m) =>
            {
                if (MessageBox.Show(this, m.Message, m.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    m.CallBack.Invoke();
                }
            });
            MessengerInstance.Register<msg.CommunicationErrorMessage>(this, (m) =>
            {
                MessageBox.Show(this, COMMUNICATION_ERROR, MESSAGEBOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            });



            DataContext = new ViewModel.MainViewModel();
        }
    }
}
