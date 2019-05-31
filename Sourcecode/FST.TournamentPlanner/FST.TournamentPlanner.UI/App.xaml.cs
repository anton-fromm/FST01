using FST.TournamentPlanner.UI.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FST.TournamentPlanner.UI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private static Model.ModelClient _restClient;
        internal static Model.ModelClient RestClient
        {
            get
            {
                if (_restClient == null)
                {
                    _restClient = new Model.ModelClient(new AnonymousCredential())
                    {
                        BaseUri = new Uri(Settings.Default.TournamentApi)
                    };
                }
                return _restClient; 
            }
        }
    }
}
