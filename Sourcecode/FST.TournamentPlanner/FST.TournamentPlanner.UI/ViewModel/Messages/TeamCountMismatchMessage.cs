using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class TeamCountMismatchMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {

        public TeamCountMismatchMessage(int currentTeamCount, int tournamentSize)
        {
            TournamentSize = tournamentSize;
            CurrentTeamCount = currentTeamCount;
        }
        public int TournamentSize { get; private set; }
        public int CurrentTeamCount { get; private set; }
    }
}
