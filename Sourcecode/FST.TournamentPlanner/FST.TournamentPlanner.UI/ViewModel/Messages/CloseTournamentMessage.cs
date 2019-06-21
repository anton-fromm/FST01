using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    public class CloseTournamentMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        public CloseTournamentMessage(object sender)
        {
            Sender = sender;
        }
    }
}
