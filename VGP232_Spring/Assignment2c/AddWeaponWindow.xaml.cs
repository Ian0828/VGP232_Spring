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
using WeaponLib;
namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for AddWeaponWindow.xaml
    /// </summary>
    public partial class AddWeaponWindow : Window
    {
        private Weapon myWeapon;

        public Weapon MyWeapon
        {
            get { return myWeapon; }
            set 
            {
                myWeapon = value;
                DataContext = myWeapon;
            }
        }
        public AddWeaponWindow()
        {
            InitializeComponent();

            string[] types = Enum.GetNames(typeof(WeaponType));
            cbType.ItemsSource = types;

            int[] rarities = { 1, 2, 3, 4, 5 };
            cbRarity.ItemsSource = rarities;
            MyWeapon = new Weapon();
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
