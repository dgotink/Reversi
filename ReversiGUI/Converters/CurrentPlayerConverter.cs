﻿using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReversiGUI
{
    class CurrentPlayerConverter: IValueConverter
    {
        PlayerColor settings = SettingsViewModel.getInstance();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "Game over!";
            else if (value.Equals(Player.ONE)) return "Current player: Player 1";
            else if (value.Equals(Player.TWO)) return "Current player: Player 2";
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
