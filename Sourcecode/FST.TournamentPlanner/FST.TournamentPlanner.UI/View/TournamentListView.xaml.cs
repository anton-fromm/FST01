using FST.TournamentPlanner.UI.ViewModel;
using FST.TournamentPlanner.UI.ViewModel.Messages;
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

namespace FST.TournamentPlanner.UI.View
{
    /// <summary>
    /// Interaktionslogik für TournamentListView.xaml
    /// </summary>
    public partial class TournamentListView : UserControl
    {
        public TournamentListView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new OpenTournamentMessage((TournamentViewModel) ((ListViewItem) sender).DataContext));
        }
    }
}
