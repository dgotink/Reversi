using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGUI
{
    class GameSettingsViewModel
    {
        private readonly SettingsViewModel settings;
        public SettingsViewModel Settings { get { return settings; } }

        private GameViewModel game;
        public GameViewModel Game
        {
            get { return game; }
            set { game = value; }
        }

        public GameSettingsViewModel()
        {
            settings = SettingsViewModel.getInstance();
            game = new GameViewModel(settings);
        }
    }
}
