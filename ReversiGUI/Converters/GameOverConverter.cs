using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ReversiGUI
{
    class GameOverConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var flag = false;
            if (value is bool) flag = (bool)value;
            if(parameter != null)//if true switch the flag (used for showing the currentplayer when game is not over vs. showing the winner when game is over
            {
                flag = !flag;
            }
            if (flag) return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
