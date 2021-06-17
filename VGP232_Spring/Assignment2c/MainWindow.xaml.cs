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
using WeaponLib;

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeaponCollection weaponCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            weaponCollection = new WeaponCollection();

            string[] types = Enum.GetNames(typeof(WeaponType));
            cbTypes.ItemsSource = types;
        }

        private void LoadClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml| JSON files |*.json| CSV files |*.csv";

            if (openFile.ShowDialog() == true)
            {
                if (!weaponCollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load the file.");
                }
                else
                {
                    lbWeapons.ItemsSource = weaponCollection;
                    lbWeapons.Items.Refresh();
                }
            }
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml| JSON files |*.json| CSV files |*.csv";
            if (saveFile.ShowDialog() == true)
            {
                if (!weaponCollection.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save the file.");
                }
            }
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            AddWeaponWindow addWeapon = new AddWeaponWindow();
            if (addWeapon.ShowDialog() == true)
            {
                weaponCollection.Add(addWeapon.MyWeapon);
                if (lbWeapons.ItemsSource == null)
                {
                    lbWeapons.ItemsSource = weaponCollection;
                }
                lbWeapons.Items.Refresh();
            }
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            if (lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            EditWeaponWindow editWeapon = new EditWeaponWindow();
            editWeapon.MyWeapon = lbWeapons.SelectedItem as Weapon;

            if (editWeapon.ShowDialog() == true)
            {
                weaponCollection[lbWeapons.SelectedIndex] = editWeapon.MyWeapon;
                lbWeapons.Items.Refresh();
            }
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            if (lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            weaponCollection.RemoveAt(lbWeapons.SelectedIndex);
            lbWeapons.Items.Refresh();
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("Name");
            lbWeapons.Items.Refresh();
        }

        private void SortByBaseAttack(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("BaseAttack");
            lbWeapons.Items.Refresh();
        }

        private void SortByRarity(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("Rarity");
            lbWeapons.Items.Refresh();
        }

        private void SortByPassive(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("Passive");
            lbWeapons.Items.Refresh();
        }

        private void SortBySecondaryStat(object sender, RoutedEventArgs e)
        {
            weaponCollection.SortBy("SecondaryStat");
            lbWeapons.Items.Refresh();
        }

        private void cbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
