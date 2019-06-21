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
using FST.TournamentPlanner.UI.View;
using System.Windows.Xps.Packaging;

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
        private const string MAXIMUM_TEAM_COUNT_REACHED = "Die maximale Anzahl an Teams für dieses Turnier ist erreicht.";
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
            /*
            MessengerInstance.Register<msg.MatchFinishedMessage>(this, false, (m) =>
            {
                MessageBox.Show(this, MATCH_FINISHED, MESSAGEBOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            });
            */
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
            MessengerInstance.Register<msg.OpenTournamentMessage>(this, (m) =>
            {
                tournamentRibbon.IsSelected = true;
            });
            MessengerInstance.Register<msg.MaximumTeamCountReachedMessage>(this, (m) =>
            {
                MessageBox.Show(this, MAXIMUM_TEAM_COUNT_REACHED, MESSAGEBOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
            });

            MessengerInstance.Register<msg.GenerateWinnerCertificatesMessage>(this, (m) =>
            {
                XpsDocument doc= WinnerCertificateHelper.Generate(m.FirstPlace, 1, m.TournamentName, m.TournamentPlace, m.TournamentEndDate.ToShortDateString());

                var popup = new WinnerCertificatesView();
                popup.documentviewWord.Document = doc.GetFixedDocumentSequence();
                popup.Show();

                XpsDocument doc2 = WinnerCertificateHelper.Generate(m.SecondPlace, 2, m.TournamentName, m.TournamentPlace, m.TournamentEndDate.ToShortDateString());
                var popup2 = new WinnerCertificatesView();
                popup2.documentviewWord.Document = doc2.GetFixedDocumentSequence();
                popup2.Show();
            });

            DataContext = new ViewModel.MainViewModel();
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            var popup = new TournamentMatchTreePopup();
            popup.DataContext = ((ViewModel.MainViewModel)DataContext).CurrentDocument;
            popup.Show();
        }
    }
}
