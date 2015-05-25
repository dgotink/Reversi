using Reversi.Cells;
using Reversi.DataStructures;
using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReversiGUI
{
    class GameViewModel
    {
        private readonly BoardViewModel board;
        public BoardViewModel Board { get { return board; } }

        private readonly SettingsViewModel settings;
        public SettingsViewModel Settings { get { return settings; } }

        private Game currentGame;

        public ICell<int> StonesPlayerOne { get { return currentGame.StoneCount(Player.ONE); } }
        public ICell<int> StonesPlayerTwo { get { return currentGame.StoneCount(Player.TWO); } }

        public ICell<Player> CurrentPlayer { get { return currentGame.CurrentPlayer; } }

        public ICell<bool> IsGameOver { get { return currentGame.IsGameOver; } }

        public ICell<Player> PlayerWithMostStones { get { return currentGame.PlayerWithMostStones; } }

        public GameViewModel(SettingsViewModel settings)
        {
            currentGame = Game.CreateNew();
            board = new BoardViewModel(currentGame.Board);
            this.settings = settings;
        }

        public void SetCapturedBy(Vector2D position)
        {
            if(settings.ShowCapturedBy.Value) board.SetCapturedBy(position);
        }

        public void RemoveCapturedBy()
        {
            if (settings.ShowCapturedBy.Value) board.RemoveCapturedBy();
        }
    }
}
