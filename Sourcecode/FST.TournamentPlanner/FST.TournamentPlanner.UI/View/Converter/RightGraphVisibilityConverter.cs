using FST.TournamentPlanner.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FST.TournamentPlanner.UI.View
{
    public class RightGraphVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MatchViewModel)
            {
                var vm = (MatchViewModel)value;
                if (vm.Successor == null)
                {
                    // No Successor = no graph needed
                    return Visibility.Hidden;
                }
                if (vm.Successor.FirstPredecessor == vm)
                {
                    // First predecessor needs the graph
                    return Visibility.Visible;
                }
                // Second predecesor does not need the graph
                return Visibility.Hidden;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
