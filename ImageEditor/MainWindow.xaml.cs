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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChange(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            initContainer();
        }

        private void initContainer()
        {
            double maxWidth = 150;
            double ratio;

            if (double.TryParse(txtRatio.Text, out ratio))
            {
                double height = maxWidth / ratio;
                _containerSize = new Size(maxWidth, height);
            }
            else
            {
                lblError.Content = "Ratio only accepts double.";
            }

        }

        private ObservableCollection<ImageContainer> _listViewItems = new ObservableCollection<ImageContainer>();

        public ObservableCollection<ImageContainer> ListViewItems
        {
            get { return _listViewItems; }
            set {
                _listViewItems = value;
                if (this.PropertyChanged != null)
                    NotifyChange(new PropertyChangedEventArgs("ListViewItems"));
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                if (ListViewItems.Count + dlg.FileNames.Count() >= 5)
                {
                    lblError.Content = "Max number of files: 5";
                }
                else
                {                                        
                    LoadImages(dlg);
                    if (this.PropertyChanged != null)
                        NotifyChange(new PropertyChangedEventArgs("ListViewItems"));
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
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("ListViewItems"));
        }
    }
}
