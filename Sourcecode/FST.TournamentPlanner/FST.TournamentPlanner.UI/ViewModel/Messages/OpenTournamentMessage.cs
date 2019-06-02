using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class OpenTournamentMessage: GalaSoft.MvvmLight.Messaging.MessageBase
    {

        public OpenTournamentMessage(TournamentViewModel tournament)
        {
            Tournament = tournament;
        }

        public TournamentViewModel Tournament { get; private set; }
    }
}
