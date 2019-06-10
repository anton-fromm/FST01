using FST.TournamentPlanner.UI.Model;
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
        public static ModelClient RestClient { get; } = new Model.ModelClient(new AnonymousCredential()) { BaseUri = new Uri(Settings.Default.TournamentApi) };
    }
}
