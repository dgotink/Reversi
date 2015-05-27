using Reversi.Cells;
using Reversi.DataStructures;
using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ReversiGUI
{
    public interface AIGameCombo
    {
        Game CurrentGame { get; }
        ReversiArtificialIntelligence AI { get; }
        bool AIPlays { get; }
    }
    class GameViewModel : AIGameCombo
    {

        public virtual ReversiArtificialIntelligence AI { get { return Settings.AI; } }
        public virtual bool AIPlays { get { return Settings.AIPlays.Value; } }

        private readonly BoardViewModel board;
        public BoardViewModel Board { get { return board; } }

        public virtual SettingsViewModel Settings { get { return SettingsViewModel.getInstance(); } }

        public Color ColorPlayerOne { get { return Settings.ColorPlayerOne; } }
        public Color ColorPlayerTwo { get { return Settings.ColorPlayerTwo; } }
      
        public SolidColorBrush ColorPlayerOneAsBrush { get { return new SolidColorBrush(Settings.ColorPlayerOne); } }
        public SolidColorBrush ColorPlayerTwoAsBrush { get { return new SolidColorBrush(Settings.ColorPlayerTwo); } }

        private readonly Game currentGame;
        public virtual Game CurrentGame { get { return currentGame; } }

        public ICell<int> StonesPlayerOne { get { return CurrentGame.StoneCount(Player.ONE); } }
        public ICell<int> StonesPlayerTwo { get { return CurrentGame.StoneCount(Player.TWO); } }

        public ICell<Player> CurrentPlayer { get { return CurrentGame.CurrentPlayer; } }

        public ICell<bool> IsGameOver { get { return CurrentGame.IsGameOver; } }

        public ICell<Player> PlayerWithMostStones { get { return CurrentGame.PlayerWithMostStones; } }

        private readonly ICell<int> timePlayed;
        public ICell<int> TimePlayed { get { return timePlayed; } }

        private readonly DispatcherTimer timer;

        public GameViewModel()
        {
            currentGame = Game.CreateNew();
            board = new BoardViewModel(CurrentGame.Board, this);
            timePlayed = Cell.Create<int>(0);
            timer = new DispatcherTimer();
            InitTimer();
        }

        private void InitTimer()
        {
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (currentGame.IsGameOver.Value) timer.Stop();
            timePlayed.Value++;
        }

        public void SetCapturedBy(Vector2D position)
        {
            if(Settings.ShowCapturedBy.Value) board.SetCapturedBy(position);
        }

        public void RemoveCapturedBy()
        {
            if (Settings.ShowCapturedBy.Value) board.RemoveCapturedBy();
        }
    }
}
