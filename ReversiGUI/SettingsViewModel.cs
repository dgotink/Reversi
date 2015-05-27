using Reversi.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ReversiGUI
{
    public interface PlayerColor
    {
        Color ColorPlayerOne {get;set;}
        Color ColorPlayerTwo {get;set;}
    }

    public class SettingsViewModel : PlayerColor
    {
        //singleton
        public static SettingsViewModel instance = null;

        public static Color ColorGreen { get { return Color.FromRgb(120, 174, 94); } }
        public static Color ColorYellow { get { return  Color.FromRgb(240, 235, 158); } }
        public static Color ColorBlue { get { return Color.FromRgb(69, 73, 252); } }
        public static Color ColorRed { get { return Color.FromRgb(246, 118, 118); } }
        public static Color ColorWhite { get { return Color.FromRgb(255, 255, 255); } }
        public static Color ColorBlack { get { return Color.FromRgb(0, 0, 0); } }

        private readonly ICell<bool> showCapturedBy = Cell.Create<bool>(true);
        public ICell<bool> ShowCapturedBy { get { return showCapturedBy; } }

        private readonly ICell<bool> aiPlays = Cell.Create<bool>(false);
        public ICell<bool> AIPlays { get { return aiPlays; } }

        private readonly ICommand changeShowCaptured;
        public ICommand ChangeShowCaptured { get { return changeShowCaptured; } }

        private readonly ICommand changeAIPlays;
        public ICommand ChangeAIPlays { get { return changeAIPlays; } }

        private Color colorPlayerOne = ColorWhite;
        public virtual Color ColorPlayerOne 
        { 
            get { return colorPlayerOne; }
            set { colorPlayerOne = value; }
        }

        private Color colorPlayerTwo = ColorBlack;
        public virtual Color ColorPlayerTwo
        {
            get { return colorPlayerTwo; }
            set { colorPlayerTwo = value; }
        }

        private readonly ICommand changeColorPlayerOne;
        public ICommand ChangeColorPlayerOne { get { return changeColorPlayerOne; } }

        private readonly ICommand changeColorPlayerTwo;
        public ICommand ChangeColorPlayerTwo { get { return changeColorPlayerTwo; } }

        private SettingsViewModel()
        {
            changeShowCaptured = new ChangeShowCapturedByCommand(this);
            changeAIPlays = new ChangeAIPlaysCommand(this);
            changeColorPlayerOne = new ChangeColorPlayerOneCommand(this);
            changeColorPlayerTwo = new ChangeColorPlayerTwoCommand(this);
        }

        public static SettingsViewModel getInstance()
        {
            if (instance == null)
            {
                instance = new SettingsViewModel();
            }
            return instance;
        }

        public void ChangeShowCapturedBy()
        {
            if (ShowCapturedBy.Value == true) ShowCapturedBy.Value = false;
            else ShowCapturedBy.Value = true;
        }

        public void ChangeAIPlayBool()
        {
            if (AIPlays.Value == true) AIPlays.Value = false;
            else AIPlays.Value = true;
        }

        public void ChangePlayerColor(int player, string color)
        {
            if (player == 1) ColorPlayerOne = GetColorFromString(color);
            else if (player == 2) ColorPlayerTwo = GetColorFromString(color);
        }

        private Color GetColorFromString(string color)
        {
            if (color.Equals("white")) return ColorWhite;
            else if(color.Equals("black")) return ColorBlack;
            else if (color.Equals("blue")) return ColorBlue;
            else if (color.Equals("yellow")) return ColorYellow;
            else if (color.Equals("red")) return ColorRed;
            else return ColorGreen;
        }

        public static Color GetTextColor(Color color){
            if(color.Equals(ColorWhite) || color.Equals(ColorYellow) || color.Equals(ColorRed)) return ColorBlack;
            else return ColorWhite;
        }

        private class ChangeShowCapturedByCommand : ICommand {

            private readonly SettingsViewModel model;

            public ChangeShowCapturedByCommand(SettingsViewModel pModel)
            {
                model = pModel;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                model.ChangeShowCapturedBy();
            }
        }

        private class ChangeAIPlaysCommand : ICommand
        {

            private readonly SettingsViewModel model;

            public ChangeAIPlaysCommand(SettingsViewModel pModel)
            {
                model = pModel;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                model.ChangeAIPlayBool();
            }
        }

        private class ChangeColorPlayerOneCommand : ICommand
        {

            private readonly SettingsViewModel model;

            public ChangeColorPlayerOneCommand(SettingsViewModel pModel)
            {
                model = pModel;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                string color = (string) parameter;
                model.ChangePlayerColor(1, color);
            }
        }

        private class ChangeColorPlayerTwoCommand : ICommand
        {

            private readonly SettingsViewModel model;

            public ChangeColorPlayerTwoCommand(SettingsViewModel pModel)
            {
                model = pModel;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                string color = (string)parameter;
                model.ChangePlayerColor(2, color);
            }
        }
    }   
}
