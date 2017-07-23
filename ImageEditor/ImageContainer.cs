using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageEditor
{
    internal class ImageContainer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChange(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public Guid Id { get; set; }
        public bool Visible { get; set; }

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
        public ImageSource Image { get; set; }
        public string Name { get; set; }
        public Size OrigSize { get; set; }

        public Size FittedSize { get; set; }
        public Size ContainerSize { get; set; }
    }

}