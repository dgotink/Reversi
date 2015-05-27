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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private readonly SettingsViewModel settings;
        public SettingsViewModel Settings { get { return settings; } }

        public SettingsView()
        {
            InitializeComponent();
            settings = SettingsViewModel.getInstance();
            DataContext = settings;
        }

        private void ColorChangedPopup(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please restart the game to play with your new selected color.", "Color changed");
        }
    }
}
