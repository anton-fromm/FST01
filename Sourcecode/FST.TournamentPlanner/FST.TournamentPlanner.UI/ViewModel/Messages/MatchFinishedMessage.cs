using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages
{
    internal class MatchFinishedMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        internal MatchFinishedMessage(MatchViewModel match)
        {
            if (match == null)
            {
                throw new NullReferenceException();
            }
            Match = match;
        }

        internal MatchViewModel Match { get; private set; }
    }
}
