using Microsoft.Win32;
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

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for RSAKeys.xaml
    /// </summary>
    public partial class RSAKeys : Window
    {
        Crypto crypto;

        public RSAKeys()
        {
            InitializeComponent();
            crypto = new Crypto();
            crypto.Initialize(CryptoAlgorithm.RSA);
        }

        private void ImportPrivateKeyClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Xml File|*.xml";
            if (openFile.ShowDialog() == true)
            {
                crypto.ImportPrivateKey(openFile.FileName);
            }
        }

        private void ImportPublicKeyClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Xml File|*.xml";
            if (openFile.ShowDialog() == true)
            {
                crypto.ImportPublicKey(openFile.FileName);
            }
        }

        private void ExportPrivateKeyClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportPrivateKey(saveFile.FileName);
            }
        }

        private void ExportPublicKeyClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportPublicKey(saveFile.FileName);
            }
        }

        private void NextClicked(object sender, RoutedEventArgs e)
        {
            EncryptionTool encryptionTool = new EncryptionTool();
            encryptionTool.Show();
            Close();
        }
    }
}
