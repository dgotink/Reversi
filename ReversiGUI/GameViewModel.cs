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
        SettingsViewModel Settings { get; }
    }
    class GameViewModel : AIGameCombo
    {

        private readonly ReversiArtificialIntelligence ai;
        public virtual ReversiArtificialIntelligence AI { get { return ai; } }

        private readonly BoardViewModel board;
        public BoardViewModel Board { get { return board; } }

        private readonly SettingsViewModel settings;
        public virtual SettingsViewModel Settings { get { return settings; } }

        public Color ColorPlayerOne { get { return settings.ColorPlayerOne; } }
        public Color ColorPlayerTwo { get { return settings.ColorPlayerTwo; } }
      
        public SolidColorBrush ColorPlayerOneAsBrush { get { return new SolidColorBrush(settings.ColorPlayerOne); } }
        public SolidColorBrush ColorPlayerTwoAsBrush { get { return new SolidColorBrush(settings.ColorPlayerTwo); } }

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

        public GameViewModel(SettingsViewModel settings)
        {
            currentGame = Game.CreateNew();
            this.settings = settings;
            ai = ReversiArtificialIntelligence.CreateMinimax(new WeightedStoneCountHeuristic(), 2);
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
            if(settings.ShowCapturedBy.Value) board.SetCapturedBy(position);
        }

        public void RemoveCapturedBy()
        {
            if (settings.ShowCapturedBy.Value) board.RemoveCapturedBy();
        }
    }
}
