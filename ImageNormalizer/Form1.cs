using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageNormalizer
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Stores all the images containers
        /// </summary>
        private static Dictionary<int, ImageContainer> _containers;

        /// <summary>
        /// Id of the current selected image
        /// </summary>
        private ImageContainer selectedContainer;
        
        public frmMain()
        {
            InitializeComponent();
            panelBox.Size = new Size(Convert.ToInt32(numWidth.Value), Convert.ToInt32(numHeight.Value));

            _containers = new Dictionary<int, ImageContainer>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var images = imageList1.Images;
            var items = lstViewImages.Items;
            var index = _containers.Count;
            foreach (var item in openFileDialog1.FileNames)
            {
                var image = Image.FromFile(item);
                images.Add(image);
                items.Add(new ListViewItem
                {
                    ImageIndex = index,
                    Name = index.ToString(),
                    Text = item.Split('\\').Last(),
                    Checked = true
                });

                InitPicture(index, image);

                index++;
            }
        }

        /// <summary>
        /// Adds a new picture to the container
        /// </summary>
        private void InitPicture(int index, Image image)
        {
            Debug.WriteLine("Image size:" + image.Size);

            var fittedSize = ImageEqualizer.FitImageSizeToContainer(image.Size, panelBox.Size);
            Debug.WriteLine("Image fitted size: " + fittedSize);
            Debug.WriteLine("Container Size: " + panelBox.Size);


            //create child container with image as background
            var container = new ImageContainer();

            container.TopLevel = container.ControlBox = false;
            container.Visible = true;
            container.BackgroundImage = image;
            container.BackgroundImageLayout = ImageLayout.Zoom;
            container.MaximumSize = fittedSize;
            container.Size = fittedSize;
            container.FormBorderStyle = FormBorderStyle.FixedSingle;
            container.Parent = panelBox;
            container.StartPosition = FormStartPosition.CenterParent;                        
            
            _containers.Add(index, container);


        }

        //private void Box_Resize(object sender, EventArgs e)
        //{
        //    var form = sender as Form;
        //    var imageSize = form.BackgroundImage.Size;
        //    var aspectRatio = ImageEqualizer.GetRatio(imageSize);
        //    form.Size = new Size(form.Size.Width, (int)Math.Floor(form.Size.Width * aspectRatio));
        //}

        private void numHeight_ValueChanged(object sender, EventArgs e)
        {
            panelBox.Height = Int32.Parse(numHeight.Value.ToString());
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            panelBox.Width = Int32.Parse(numWidth.Value.ToString());
        }

        private void lstViewImages_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            var index = e.Item.ImageIndex;
            if (_containers.ContainsKey(index))
                _containers[index].Visible = e.Item.Checked;
        }

        private void sliderZoom_Scroll(object sender, EventArgs e)
        {
            txtScale.Text = sliderScale.Value.ToString();
            selectedContainer.ImageScale = sliderScale.Value;
            selectedContainer.Rescale();
        }

        private void txtScale_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (Int32.TryParse(txtScale.Text, out value))
            {
                sliderScale.Value = value;
            }
        }

        private void lstViewImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstViewImages.SelectedIndices.Count > 0)
            {
                var index = lstViewImages.SelectedIndices[0];
                selectedContainer = _containers[index];
                selectedContainer.Opacity = 0.1;                
                selectedContainer.BringToFront();
                sliderScale.Enabled = true;
                txtScale.Enabled = true;
            }
            else
            {
                selectedContainer.Opacity = 1;
                selectedContainer = null;
                sliderScale.Enabled = false;
                txtScale.Enabled = false;
            }
            
        }
    }
}
