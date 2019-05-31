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
    public class BottomGraphVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TournamentMatchViewModel)
            {
                var vm = (TournamentMatchViewModel)value;
                if (vm.Predecessors != null && vm.Predecessors.Count() > 0)
                {
                    // If there Predecessors, we need this graph
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
