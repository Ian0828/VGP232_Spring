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
    /// Interaction logic for AESKeys.xaml
    /// </summary>
    public partial class AESKeys : Window
    {
        Crypto crypto;
        
        public AESKeys()
        {
            InitializeComponent();
            crypto = new Crypto();
            crypto.Initialize(CryptoAlgorithm.AES);
        }

        private void ImportSharedKeyClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Binary File|*.bin";
            if (openFile.ShowDialog() == true)
            {
                crypto.ImportPrivateKey(openFile.FileName);
            }
        }

        private void ImportIVClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Binary File|*.bin";
            if (openFile.ShowDialog() == true)
            {
                crypto.ImportPublicKey(openFile.FileName);
            }
        }

        private void ExportSharedKeyClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Binary File|*.bin";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportAESKey(saveFile.FileName);
            }
        }

        private void ExportIVClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Binary File|*.bin";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportAESIV(saveFile.FileName);
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
