
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageNormalizer
{
    public class ImageContainer : Form
    {
        /// <summary>
        /// Relation between the initial size and the current one. 0-100 range
        /// </summary>
        /// 
        private int _imageScale;
        public int ImageScale
        {
            get { return _imageScale; }
            set
            {
                if (value >= 0 || value <= 100)
                    _imageScale = value;
            }
        }

        /// <summary>
        /// It refreshes the size of the container. Tipically called after assigning a new scale by using ImageScale.
        /// </summary>
        public void Rescale()
        {
            this.Size = new Size(this.MaximumSize.Width * ImageScale / 100, this.MaximumSize.Height * ImageScale / 100);
        }
    }
}
