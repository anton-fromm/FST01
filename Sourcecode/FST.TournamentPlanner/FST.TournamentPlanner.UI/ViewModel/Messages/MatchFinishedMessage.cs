using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    internal class MatchFinishedMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        internal MatchFinishedMessage(int tournamentID, MatchViewModel match)
        {
            Match = match ?? throw new NullReferenceException();
            TournamentId = tournamentID;
        }

        internal MatchViewModel Match { get; private set; }
        public int TournamentId { get; private set; }
    }
}
