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

        private readonly ICell<bool> aiPlays = Cell.Create<bool>(false);
        public ICell<bool> AIPlays { get { return aiPlays; } }

        private readonly ICommand changeShowCaptured;
        public ICommand ChangeShowCaptured { get { return changeShowCaptured; } }

        private readonly ICommand changeAIPlays;
        public ICommand ChangeAIPlays { get { return changeAIPlays; } }

        public SettingsViewModel()
        {
            changeShowCaptured = new ChangeShowCapturedByCommand(this);
            changeAIPlays = new ChangeAIPlaysCommand(this);
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
    }
}
