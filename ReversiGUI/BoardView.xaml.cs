using Reversi.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReversiGUI
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        GameViewModel vm;
        private SettingsViewModel settings;
        public SettingsViewModel Settings
        {
            get { return settings;}
            set { settings = value; }
        }

        public BoardView()
        {
            InitializeComponent();
            CreateNewGame();
        }

        public BoardView(SettingsViewModel pSettings)
        {
            InitializeComponent();
            settings = pSettings;
            CreateNewGame();
        }

        private void CreateNewGame(object sender = null, RoutedEventArgs e = null)
        {
           vm = new GameViewModel(settings);
           DataContext = vm;
        }

        private void ShowCapturedBy(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var tag = (Vector2D) button.Tag;
            vm.SetCapturedBy(tag);
        }

        private void RemoveCapturedBy(object sender, MouseEventArgs e)
        {
            vm.RemoveCapturedBy();
        }
    }
}
