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
using Microsoft.Win32;
using PlayerLib;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlayerPool playerPool { get; set; }

        public int playerCount;

        public MainWindow()
        {
            InitializeComponent();
            playerPool = new PlayerPool();

            string[] pos = Enum.GetNames(typeof(PlayerPosition));
            cbPosition.ItemsSource = pos;
        }

        private void LoadClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml| JSON files |*.json| CSV files |*.csv";

            if (openFile.ShowDialog() == true)
            {
                if (!playerPool.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load the file.");
                }
                else
                {
                    lbPlayers.ItemsSource = playerPool;
                    lbPlayers.Items.Refresh();
                }
            }
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml| JSON files |*.json| CSV files |*.csv";
            if (saveFile.ShowDialog() == true)
            {
                if (!playerPool.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save the file.");
                }
            }
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            if (lbPlayers.SelectedIndex == -1)
            {
                return;
            }

            if (playerCount < 10)
            {
                lbTeam.Items.Add(lbPlayers.SelectedItem);
                playerPool.RemoveAt(lbPlayers.SelectedIndex);
                lbPlayers.Items.Refresh();
                lbTeam.Items.Refresh();
                playerCount++;
            }
            else if (playerCount >= 10)
            {
                MessageBox.Show("Already 10 players in your team.");
            }
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            if (lbTeam.SelectedIndex == -1)
            {
                return;
            }
            playerPool.Add(lbTeam.SelectedItem as Player);
            lbTeam.Items.Remove(lbTeam.SelectedItem);
            lbPlayers.Items.Refresh();
            lbTeam.Items.Refresh();
            playerCount--;
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            if (lbTeam.SelectedIndex == -1)
            {
                return;
            }

            EditPlayerWindow editPlayer = new EditPlayerWindow();
            editPlayer.MyPlayer = lbTeam.SelectedItem as Player;

            if (editPlayer.ShowDialog() == true)
            {
                playerPool[lbTeam.SelectedIndex] = editPlayer.MyPlayer;
                lbTeam.Items.Refresh();
            }
        }

        private void SortByShooting(object sender, RoutedEventArgs e)
        {
            playerPool.SortBy("Shooting");
            lbPlayers.Items.Refresh();
        }

        private void SortByOverall(object sender, RoutedEventArgs e)
        {
            playerPool.SortBy("Overall");
            lbPlayers.Items.Refresh();
        }

        private void SortBySpeed(object sender, RoutedEventArgs e)
        {
            playerPool.SortBy("Speed");
            lbPlayers.Items.Refresh();
        }

        private void SortByHeight(object sender, RoutedEventArgs e)
        {
            playerPool.SortBy("Height");
            lbPlayers.Items.Refresh();
        }

        private void cbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pos = Enum.Parse<PlayerPosition>(cbPosition.SelectedItem.ToString());
            lbPlayers.ItemsSource = playerPool.GetAllPlayerOfPosition(pos);
            lbPlayers.Items.Refresh();
        }

        private void AllClicked(object sender, RoutedEventArgs e)
        {
            lbPlayers.ItemsSource = playerPool;
            playerPool.players = lbPlayers.ItemsSource as List<Player>;
            lbPlayers.Items.Refresh();
        }
    }
}
