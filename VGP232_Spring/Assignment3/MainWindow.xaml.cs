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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using TextureAtlasLib;

namespace Assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Spritesheet mySpriteSheet { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            mySpriteSheet = new Spritesheet();
            mySpriteSheet.InputPaths = new List<string>();
            DataContext = mySpriteSheet;

            lbImages.ItemsSource = mySpriteSheet.InputPaths;
        }

        private void NewClicked(object sender, RoutedEventArgs e)
        { 
            if (MessageBoxResult.Yes == MessageBox.Show("You have unsaved changes.\nWould you like to save them?", "Warning", MessageBoxButton.YesNo))
            {
                SaveClicked(this, null);
            }
            
            tbOutputDir.Text = string.Empty;
            tbOutputFile.Text = string.Empty;
            tbColumns.Text = "0";
            mySpriteSheet.InputPaths.Clear();
            lbImages.Items.Refresh();
            //lbImages.ItemsSource = new List<string>();
        }

        private void OpenClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml";

            if (openFile.ShowDialog() == true)
            {
                if (!mySpriteSheet.LoadXML(openFile.FileName))
                {
                    MessageBox.Show("Unable to load file.");
                }
                else
                {
                    tbColumns.Text = mySpriteSheet.Columns.ToString();
                    tbOutputFile.Text = mySpriteSheet.OutputFile;
                    tbOutputDir.Text = mySpriteSheet.OutputDirectory;
                    lbImages.ItemsSource = mySpriteSheet.InputPaths;
                    lbImages.Items.Refresh();
                }
            }
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml";

            if (saveFile.ShowDialog() == true)
            {
                if (!mySpriteSheet.SaveAsXML(saveFile.FileName))
                {
                    MessageBox.Show("Error, unable to save file.");
                }
            }
        }

        private void SaveAsClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveAsFile = new SaveFileDialog();
            string filePath = saveAsFile.FileName;
            saveAsFile.InitialDirectory = tbOutputDir.Text;
            saveAsFile.Filter = "XML files |*.xml";
            
            if (saveAsFile.ShowDialog() == true)
            {
                if (!mySpriteSheet.SaveAsXML(saveAsFile.FileName))
                {
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    tbOutputDir.Text = filePath;
                }
            }
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("You have unsaved changes.\nWould you like to save them?", "Warning", MessageBoxButton.YesNo))
            {
                SaveClicked(this, null);
            }
            Application.Current.Shutdown();
        }

        private void BrowseClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog browseFile = new SaveFileDialog();
            browseFile.Filter = "PNG Images| *.png";
            mySpriteSheet.OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                
            if (browseFile.ShowDialog() == true)
            {
                tbOutputDir.Text = System.IO.Path.GetDirectoryName(browseFile.FileName);
                mySpriteSheet.OutputDirectory = tbOutputDir.Text;

                tbOutputFile.Text = System.IO.Path.GetFileName(browseFile.FileName);
                mySpriteSheet.OutputFile = tbOutputFile.Text;
            }
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PNG Images| *.png";
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == true)
            {
                foreach(string imagePath in openFile.FileNames)
                {
                    mySpriteSheet.InputPaths.Add(imagePath);
                }
                lbImages.Items.Refresh();
            }
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            if (lbImages.SelectedIndex == -1)
            {
                return;
            }
            mySpriteSheet.InputPaths.RemoveAt(lbImages.SelectedIndex);
            lbImages.Items.Refresh();
        }

        private void GenerateClicked(object sender, RoutedEventArgs e)
        {
            mySpriteSheet.IncludeMetaData = false;
            mySpriteSheet.OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            mySpriteSheet.OutputFile = "spriteSheet.png";

            try
            {
                mySpriteSheet.Generate(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
