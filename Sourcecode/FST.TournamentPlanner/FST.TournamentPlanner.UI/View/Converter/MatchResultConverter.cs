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
    public class MatchResultConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var teamOne = values[0] as TeamViewModel;
            var teamTwo = values[1] as TeamViewModel;
            var resTeamOne = values[2] as int?;
            var resTeamTwo = values[3] as int?;
            if (teamOne == null && teamTwo == null)
            {
                return String.Empty;
            }
            if (!resTeamOne.HasValue || !resTeamTwo.HasValue)
            {
                return "vs.";
            }
            return $"{resTeamOne.Value} : {resTeamTwo.Value}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
