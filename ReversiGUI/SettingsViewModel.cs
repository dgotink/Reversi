using Reversi.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReversiGUI
{
    public class SettingsViewModel
    {
        /*public static string WHITE = "white";
        public static string BLACK = "black";
        public static string GREEN = "green";
        public static string RED = "red";
        public static string YELLOW = "yellow";
        public static string BLUE = "blue";

        private readonly ICell<string> colorPlayer1 = Cell.Create<String>("white");
        public ICell<String> ColorPlayer1 { get { return colorPlayer1; } }


        private readonly ICell<string> colorPlayer2 = Cell.Create<String>("black");
        public ICell<String> ColorPlayer2 { get { return colorPlayer2; } }*/

        private readonly ICell<bool> showCapturedBy = Cell.Create<bool>(false);
        public ICell<bool> ShowCapturedBy { get { return showCapturedBy; } }

        private readonly ICommand changeShowCaptured;

        public SettingsViewModel()
        {
            changeShowCaptured = new ChangeShowCapturedByCommand(this);
        }

        public void changeShowCapturedBy()
        {
            if (ShowCapturedBy.Value == true) ShowCapturedBy.Value = false;
            else ShowCapturedBy.Value = true;
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
                model.changeShowCapturedBy();
            }
        }
    }


}
