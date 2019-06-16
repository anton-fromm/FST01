using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using Path = System.IO.Path;
using Word = Microsoft.Office.Interop.Word;

namespace FST.TournamentPlanner.UI.View
{
    /// <summary>
    /// Interaktionslogik für WinnerCertificatesView.xaml
    /// </summary>
    public partial class WinnerCertificatesView : System.Windows.Window
    {
        public WinnerCertificatesView()
        {
            InitializeComponent();
        }
    }
}
