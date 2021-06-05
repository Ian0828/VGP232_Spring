using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EncryptionTool.xaml
    /// </summary>
    public partial class EncryptionTool : Window
    {
        Crypto crypto;

        public EncryptionTool()
        {
            InitializeComponent();
            crypto = new Crypto();
            crypto.Initialize(CryptoAlgorithm.AES);
        }

        private void LoadTextClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text files |*.txt";
            if (openFile.ShowDialog() == true)
            {
                string textMessage = tbTextMessage.Text;
                byte[] data = Convert.FromBase64String(textMessage);
                data = File.ReadAllBytes(openFile.FileName);
                string text = Convert.ToBase64String(data);
                tbTextMessage.Text = text;
            }
        }

        private void EncryptClicked(object sender, RoutedEventArgs e)
        {
            string plainText = tbTextMessage.Text;
            plainText = plainText.Replace(' ', '+');
            int padding = plainText.Length % 4;
            if (padding != 0)
            {
                plainText += new string('+', 4 - padding);
            }

            byte[] data = Convert.FromBase64String(plainText);
            byte[] cipherData = crypto.Encrypt(data);
            string cipherText = Convert.ToBase64String(cipherData);
            tbTextResult.Text = cipherText;
        }

        private void EncryptSaveToFileClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Binary files |*.bin";
            string cipherText = tbTextResult.Text;
            byte[] cipherData = Convert.FromBase64String(cipherText);
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllBytes(saveFile.FileName, cipherData);
            }
        }

        private void LoadCipherClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Binary files |*.bin";
            if (openFile.ShowDialog() == true)
            {
                string cipherText = tbCipherMessage.Text;
                byte[] cipherData = Convert.FromBase64String(cipherText);
                cipherData = File.ReadAllBytes(openFile.FileName);
                string text = Convert.ToBase64String(cipherData);
                tbCipherMessage.Text = text;
            }
        }

        private void DecryptClicked(object sender, RoutedEventArgs e)
        {
            string cipherText = tbCipherMessage.Text;
            byte[] cipherData = Convert.FromBase64String(cipherText);
            byte[] plainData = crypto.Decrypt(cipherData);
            string plainText = Convert.ToBase64String(plainData);
            plainText = plainText.Replace('+', ' ');
            tbCipherResult.Text = plainText;
        }

        private void DecryptSaveToFileClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text files |*.txt";
            string text = tbCipherResult.Text;
            byte[] data = Convert.FromBase64String(text);
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllBytes(saveFile.FileName, data);
            }
        }
    }
}
