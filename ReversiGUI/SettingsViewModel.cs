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
        private readonly ICell<bool> showCapturedBy = Cell.Create<bool>(true);
        public ICell<bool> ShowCapturedBy { get { return showCapturedBy; } }

        private readonly ICell<bool> aiPlays = Cell.Create<bool>(true);
        public ICell<bool> AIPlays { get { return aiPlays; } }

        private readonly ICommand changeShowCaptured;
        public ICommand ChangeShowCaptured { get { return changeShowCaptured; } }

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
