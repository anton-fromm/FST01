using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class TeamCountInvalidMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        public TeamCountInvalidMessage(int tournamentID)
        {
            TournamentId = tournamentID;
        }

        public int TournamentId { get; private set; }
    }
}
