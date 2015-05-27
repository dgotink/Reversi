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
            PlayerColor playerColors = SettingsViewModel.getInstance();

            if (value != null && playerColors != null)
            {
                if (value.Equals(Player.ONE)) stringPath = getImagePath(playerColors.ColorPlayerOne);
                else if (value.Equals(Player.TWO)) stringPath = getImagePath(playerColors.ColorPlayerTwo);          
            }

            ImageSource result = new BitmapImage(new Uri(stringPath, UriKind.Relative));
            return stringPath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string getImagePath(Color player)
        {
            if (player.Equals(SettingsViewModel.ColorWhite)) return "/resources/witReversi.png";
            else if (player.Equals(SettingsViewModel.ColorBlack)) return "/resources/zwartReversi.png";
            else if (player.Equals(SettingsViewModel.ColorBlue)) return "/resources/blauwReversi.png";
            else if (player.Equals(SettingsViewModel.ColorGreen)) return "/resources/groenReversi.png";
            else if (player.Equals(SettingsViewModel.ColorRed)) return "/resources/roodReversi.png";
            else if (player.Equals(SettingsViewModel.ColorYellow)) return "/resources/geelReversi.png";
            return "";
        }
    }
}
