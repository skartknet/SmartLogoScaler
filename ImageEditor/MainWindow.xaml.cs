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

namespace ImageEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ImageContainer> _listViewItems;
        Size _containerSize;

        public MainWindow()
        {
            InitializeComponent();

            _listViewItems = new List<ImageContainer>();
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
                if (dlg.FileNames.Count() > 5)
                {
                    lblError.Content = "Max number of files: 5";
                }
                // Open document
                string filename = dlg.FileName;
                OpenImages(dlg);
            }

            ImagesControl.ItemsSource = _listViewItems;
        }

        private void OpenImages(OpenFileDialog dlg)
        {


            foreach (var imagePath in dlg.FileNames)
            {
                byte[] photoBytes = File.ReadAllBytes(imagePath);


                using (MemoryStream inStream = new MemoryStream(photoBytes))
                {
                    MemoryStream outStream = new MemoryStream();

                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {

                        imageFactory.Load(inStream).EntropyCrop(10).Save(outStream);

                        var bpm = new BitmapImage();
                        bpm.BeginInit();
                        bpm.StreamSource = outStream;
                        bpm.EndInit();
                        var origSize = new Size(bpm.Width, bpm.Height);
                        var fittedSize = ImageEqualizer.FitImageSizeToContainer(new Size(bpm.Width, bpm.Height), new Size(_containerSize.Width, _containerSize.Height));

                        var container = new ImageContainer
                        {
                            Name = Path.GetFileNameWithoutExtension(imagePath),
                            Image = bpm,
                            Scale = 1,
                            OrigSize = origSize,
                            FittedSize = fittedSize,
                            ContainerSize = _containerSize
                        };

                        _listViewItems.Add(container);
                    }

                }
            }
        }


    }
}
