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
    public class MatchResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = value as MatchViewModel;
            if (obj == null)
            {
                return null;
            }
            if (!obj.TeamOneScore.HasValue)
            {
                return "vs.";
            }
            return $"{obj.TeamOneScore.Value} : {obj.TeamTwoScore.Value}";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
