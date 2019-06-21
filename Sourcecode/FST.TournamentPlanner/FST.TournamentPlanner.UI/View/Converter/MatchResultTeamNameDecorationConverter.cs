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
    public class MatchResultTeamNameDecorationConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var team = parameter.ToString();
            var obj = value as MatchViewModel;
            if (obj == null)
            {
                return Binding.DoNothing;
            }
            if (obj.State != 2)
            {
                return Binding.DoNothing;
            }
            if (obj.Winner.Equals(obj.TeamOne))
            {
                // Team One is the winner
                if (team == "One")
                {
                    return Binding.DoNothing;
                }
                if (team == "Two")
                {
                    return TextDecorations.Strikethrough;
                }
            }
            if (obj.Winner.Equals(obj.TeamTwo))
            {
                // Team One is the winner
                if (team == "One")
                {
                    return TextDecorations.Strikethrough;
                }
                if (team == "Two")
                {
                    return Binding.DoNothing;
                }
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var team = parameter.ToString();
            var obj = values[0] as MatchViewModel;
            if (obj == null)
            {
                return Binding.DoNothing;
            }
            if (obj.State != 2)
            {
                return Binding.DoNothing;
            }
            if (obj.Winner.Equals(obj.TeamOne))
            {
                // Team One is the winner
                if (team == "One")
                {
                    return Binding.DoNothing;
                }
                if (team == "Two")
                {
                    return TextDecorations.Strikethrough;
                }
            }
            if (obj.Winner.Equals(obj.TeamTwo))
            {
                // Team One is the winner
                if (team == "One")
                {
                    return TextDecorations.Strikethrough;
                }
                if (team == "Two")
                {
                    return Binding.DoNothing;
                }
            }
            return Binding.DoNothing;
        }

    }
}
