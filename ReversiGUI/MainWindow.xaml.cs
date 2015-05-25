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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingsView settings;
        private BoardView board;

        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new GameSettingsViewModel();
            settings = new SettingsView();
            board = new BoardView(settings.Settings);
            //board.Settings = settings.Settings;
        }
    }
}
