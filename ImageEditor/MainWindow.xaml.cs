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

        Size _containerSize;

        /// <summary>
        /// Stores path to the file that will store the data
        /// </summary>
        private string filePath;

        /// <summary>
        /// Indicates if the container ratio input is enabled. Only enabled when images not loaded 
        /// or system has been restarted.
        /// </summary>
        public bool RatioEnabled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
                lblError.Content = "Ratio only accepts double.";
            }

        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            initContainer();

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Multiselect = true;
            dlg.Filter = "Image (.jpg)|*.jpg";
            dlg.Filter = "Image (.png)|*.png";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                if (ListViewItems.Count + dlg.FileNames.Count() > 5)
                {
                    lblError.Content = "Max number of files: 5";
                }
                else
                {
                    LoadImages(dlg);
                    if (this.PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ListViewItems"));
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
                   new MemoryStream(photoBytes),
                   _containerSize
                );

                _listViewItems.Add(container);
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            _listViewItems.Clear();
            RatioEnabled = true;
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ListViewItems"));
                PropertyChanged(this, new PropertyChangedEventArgs("RatioEnabled"));
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ImageEqualizer.SaveToFile(filePath, _containerSize, _listViewItems);
        }
   

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Multiselect = false;
            dlg.Filter = "CSV (.csv)|*.jpg";
        }
    }
}
