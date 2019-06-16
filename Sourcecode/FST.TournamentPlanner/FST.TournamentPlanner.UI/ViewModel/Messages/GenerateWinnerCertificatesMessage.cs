using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class GenerateWinnerCertificatesMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        public GenerateWinnerCertificatesMessage(string firstPlace, string secondPlace)
        {
            FirstPlace = firstPlace;
            SecondPlace = secondPlace;
        }

        public string FirstPlace { get; private set; }

        public string SecondPlace { get; private set; }
    }
}
