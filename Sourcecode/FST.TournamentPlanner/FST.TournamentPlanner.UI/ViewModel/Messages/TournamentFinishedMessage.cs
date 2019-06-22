using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class TournamentFinishedMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        public TournamentFinishedMessage(int tournamentId)
        {
            TournamentId = tournamentId;
        }

        public int TournamentId { get; private set; }
    }
}
