using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class GenerateWinnerCertificatesMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        public GenerateWinnerCertificatesMessage(string firstPlace, string secondPlace, string tournamentName, string tournamentPlace, DateTime tournamentEndDate)
        {
            FirstPlace = firstPlace;
            SecondPlace = secondPlace;
            TournamentName = tournamentName;
            TournamentPlace = tournamentPlace;
            TournamentEndDate = tournamentEndDate;
        }

        public string FirstPlace { get; private set; }

        public string SecondPlace { get; private set; }
        public string TournamentName { get; private set; }
        public string TournamentPlace { get; private set; }
        public DateTime TournamentEndDate { get; private set; }

    }
}
