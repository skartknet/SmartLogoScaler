using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ImageNormalizer;
using System.IO;
using ImageProcessor;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        Size _containerSize;

        /// <summary>
        /// Stores path to the file that will store the data
        /// </summary>        
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                if (this.PropertyChanged != null)

                    PropertyChanged(this, new PropertyChangedEventArgs("FilePath"));

            }
        }

        /// <summary>
        /// Indicates if a file to store the data is already loaded.
        /// </summary>
        private bool _isFileLoaded = false;
        public bool IsFileLoaded
        {
            get { return _isFileLoaded; }
            set
            {
                _isFileLoaded = value;
                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsFileLoaded"));
            }
        }


        /// <summary>
        /// Indicates if the container ratio input is enabled. Only enabled when images not loaded 
        /// or system has been restarted.
        /// </summary>        
        private bool _ratioEnabled;

        public bool RatioEnabled
        {
            get { return _ratioEnabled; }
            set
            {
                _ratioEnabled = value;
                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RatioEnabled"));
            }
        }



        private readonly int _maxImages = 5;

        private ObservableCollection<ImageContainer> _listViewItems = new ObservableCollection<ImageContainer>();


        public ObservableCollection<ImageContainer> ListViewItems
        {
            get { return _listViewItems; }
            set
            {
                _listViewItems = value;
                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ListViewItems"));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            RatioEnabled = true;
        }

        private void initContainer()
        {
            double maxWidth = 150;
            double ratio;

            if (double.TryParse(txtRatio.Text, out ratio))
            {
                double height = maxWidth / ratio;
                _containerSize = new Size(maxWidth, height);

                RatioEnabled = false;
                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RatioEnabled"));
            }
            else
            {
                MessageBox.Show("Ratio only accepts double", "Error");
            }

        }




        private void btnOpenImages_Click(object sender, RoutedEventArgs e)
        {
            initContainer();

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Multiselect = true;
            dlg.Filter = "Images (*.jpg, *.png)|*.jpg;*.png|All files(*.*)|*.*";            


            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                if (ListViewItems.Count + dlg.FileNames.Count() > _maxImages)
                {
                    MessageBox.Show($"Max number of files: {_maxImages}", "Error");
                }
                else
                {
                    LoadImages(dlg); 
                }
            }


        }

        private void LoadImages(OpenFileDialog dlg)
        {
            foreach (var imagePath in dlg.FileNames)
            {
                byte[] photoBytes = File.ReadAllBytes(imagePath);


                var container = new ImageContainer
                (
                    Path.GetFileNameWithoutExtension(imagePath),
                   photoBytes,
                   _containerSize
                );

                _listViewItems.Add(container);
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            ListViewItems.Clear();
            RatioEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_listViewItems.Count < _maxImages)
            {
                MessageBox.Show($"There has to be {_maxImages} items to save.", "Warning");
                return;
            }
            var line = ImageEqualizer.GetLines(_containerSize, _listViewItems);
            try
            {
                File.AppendAllLines(FilePath, new[] { line });
                MessageBox.Show("Line saved.", "Success");
                ListViewItems.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving lines. " + ex.Message, "Error");
            }
            
        }

        /// <summary>
        /// Opens the file where we will save the data and stores the path so we can write later on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Multiselect = false;
            dlg.Filter = "CSV (.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                FilePath = dlg.FileName;
                IsFileLoaded = true;
            }
        }

        private void btnCreateFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.DefaultExt = ".csv";

            Nullable<bool> result = dlg.ShowDialog();


            if (result == true)
            {
                try
                {
                    File.WriteAllLines(dlg.FileName, new[] { ImageEqualizer.GetFileHeader() });
                    FilePath = dlg.FileName;
                    IsFileLoaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating file. " + ex.Message, "Error");
                }
            }
        }
    }
}
