using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FST.TournamentPlanner.UI.View
{
    public class TournamentStateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int res;
            if (int.TryParse(value.ToString(), out res))
            {
                switch (res)
                {
                    case 0:
                        return "Geplant";
                    case 1:
                        return "Gestartet";
                    case 2:
                        return "Beendet";
                }
            }
            return "???";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
