using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class GenerateWinnerCertificatesMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        public GenerateWinnerCertificatesMessage(string tournamentName, DateTime date, string firstPlace, string secondPlace)
        {
            TournamentName = tournamentName;
            Date = date;    
            FirstPlace = firstPlace;
            SecondPlace = secondPlace;
        }

        public string FirstPlace { get; private set; }

        public string SecondPlace { get; private set; }

        public string TournamentName { get; private set; }
        
        public DateTime Date { get; private set; }
    }
}
