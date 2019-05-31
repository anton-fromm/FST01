using FST.TournamentPlanner.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FST.TournamentPlanner.UI.View
{
    public class ToListConverter : IMultiValueConverter
    {
        
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is TournamentViewModel))
            {
                return Binding.DoNothing;
            }
            var list = new List<TournamentViewModel>();
            list.Add((TournamentViewModel)values[0]);
            list.Add((TournamentViewModel)values[1]);
            return list;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
