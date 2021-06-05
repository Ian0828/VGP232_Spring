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

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        AESKeys aesWindow = new AESKeys();
        RSAKeys rsaWindow = new RSAKeys();

        public SelectionWindow()
        {
            InitializeComponent();
        }

        private void NextClicked(object sender, RoutedEventArgs e)
        {
            if (AES.IsChecked == true)
            {
                aesWindow.Show();
            }
            else if (RSA.IsChecked == true)
            {
                rsaWindow.Show();
            }
            Close();
        }
    }
}