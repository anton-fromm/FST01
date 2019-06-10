using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel.Messages 
{
    public class AreYouSureMessage : GalaSoft.MvvmLight.Messaging.MessageBase
    {
        public AreYouSureMessage(string title, string message, Action callback)
        {
            CallBack = callback;
            Message = message;
            Title = title;
        }

        public Action CallBack { get; private set; }

        public string Title { get; private set; }

        public string Message { get; private set; }
    }
}
