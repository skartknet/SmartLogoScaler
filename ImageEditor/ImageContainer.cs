using ImageNormalizer;
using ImageProcessor;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageEditor
{
    public class ImageContainer : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChange(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #region Properties

        private float _scale;

        public float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                NotifyChange(new PropertyChangedEventArgs("Scale"));
            }
        }
        public MemoryStream OrigImage { get; set; }

        private byte _threshold;
        public string EntropyThreshold
        {
            get
            {
                return _threshold.ToString();
            }
            set
            {
                Byte.TryParse(value, out _threshold);

                NotifyChange(new PropertyChangedEventArgs("CroppedImage"));
            }
        }
        public ImageSource CroppedImage
        {
            get
            {
                // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                {

                    var scaledImage = new MemoryStream();

                    imageFactory.Load(OrigImage).EntropyCrop(_threshold).Save(scaledImage);

                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = scaledImage;
                    bmp.EndInit();

                    return bmp;
                }
            }
        }
        public string Name { get; set; }
        public Size OrigSize
        {
            get
            {
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = OrigImage;
                bmp.EndInit();

                return new Size(bmp.Width, bmp.Height);
            }
        }

        public Size FittedSize
        {
            get
            {
                var fittedSize = ImageEqualizer
                                    .FitImageSizeToContainer(OrigSize, new Size(ContainerSize.Width, ContainerSize.Height));

                return fittedSize;
            }
        }
        public Size ContainerSize { get; set; }        

        #endregion



        public ImageContainer(string name, MemoryStream origImage, Size containerSize)
        {
            this.Name = name;
            this.OrigImage = origImage;
            this.ContainerSize = containerSize;

            Scale = 1;
            _threshold = 10;
        }

        

    }

}