using Reversi.Cells;
using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReversiGUI
{
    public class PlayerImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string stringPath = "";

            if (value != null)
            {
                if (value.Equals(Player.ONE)) stringPath = "/resources/witReversi.png";
                else if (value.Equals(Player.TWO)) stringPath = "/resources/zwartReversi.png";
            }

            ImageSource result = new BitmapImage(new Uri(stringPath, UriKind.Relative));
            return stringPath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
