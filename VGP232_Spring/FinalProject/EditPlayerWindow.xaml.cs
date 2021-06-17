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
using System.Windows.Shapes;
using PlayerLib;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for EditPlayerWindow.xaml
    /// </summary>
    public partial class EditPlayerWindow : Window
    {
        private Player myPlayer;

        public Player MyPlayer
        {
            get { return myPlayer; }
            set
            {
                myPlayer = value;
                DataContext = myPlayer;
            }
        }

        public EditPlayerWindow()
        {
            InitializeComponent();
            string[] pos = Enum.GetNames(typeof(PlayerPosition));
            cbPosition.ItemsSource = pos;
            myPlayer = new Player();
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
