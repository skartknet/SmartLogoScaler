
using ImageEditor;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageNormalizer
{
    public static class ImageEqualizer
    {


        /// <summary>
        /// returns a size fitted into a container
        /// </summary>
        /// <param name="imageSize"></param>
        /// <param name="containerSize"></param>
        /// <returns></returns>
        public static Size FitImageSizeToContainer(Size imageSize, Size containerSize)
        {
            if (imageSize == null) throw new ArgumentNullException("imageSize");
            if (containerSize == null) throw new ArgumentNullException("containerSize");

            float imageRatio = GetRatio(imageSize);
            float contRatio = GetRatio(containerSize);

            Size newSize = new Size();
            if (imageRatio >= contRatio)
            {
                newSize.Width = containerSize.Width;
                newSize.Height = (int)Math.Floor((float)newSize.Width / imageRatio);
            }
            else
            {
                newSize.Height = containerSize.Height;
                newSize.Width = (int)Math.Floor((float)newSize.Height * imageRatio);
            }

            return newSize;
        }

        public static float GetRatio(Size size)
        {
            return (float)size.Width / (float)size.Height;
        }



        /// <summary>
        /// Centers obj1 to obj2
        /// </summary>
        /// <param name="ob1Size"></param>
        /// <param name="obj2Size"></param>
        /// <param name="obj2Location"></param>
        /// <returns></returns>
        public static Point CenterObjects(Size obj1Size, Size obj2Size, Point obj2Location)
        {
            //obj1 is the object we want to center to obj2

            if (obj1Size == null) throw new ArgumentNullException("obj1Size");
            if (obj2Size == null) throw new ArgumentNullException("obj2Size");
            if (obj2Location == null) throw new ArgumentNullException("obj2Location");


            var newOrigin = new Point();

            var CenterX = (float)obj2Size.Width / 2f + obj2Location.X;
            var CenterY = (float)obj2Size.Height / 2f + obj2Location.Y;

            newOrigin.X = (int)Math.Floor(CenterX - obj1Size.Width / 2f);
            newOrigin.Y = (int)Math.Floor(CenterY - obj2Size.Height / 2f);


            return newOrigin;

        }

        /// <summary>
        /// Returns the center point of an object.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Point ObjectCenterPoint(Size size, Point location)
        {
            var x = size.Width / 2 + location.X;
            var y = size.Height / 2 + location.Y;

            return new Point(x, y);
        }




        public static string GetLines(Size container, IEnumerable<ImageContainer> images)
        {

            /* for each image Save:
             * Container Height
             * Container Width
             * Container Ratio             
             * Orig Image Height
             * Orig Image Width
             * Fitted Image Height
             * Fitted Image Width             
             * Image Ratio
             * Scale of image
            */
            StringBuilder line = new StringBuilder();
            line.Append($"{container.Height},{container.Width},{GetRatio(container).ToString()}");
            foreach (var i in images)
            {
                line.Append($",{i.OrigSize.Height},{i.OrigSize.Width},{i.FittedSize.Height},{i.FittedSize.Width},{GetRatio(i.FittedSize).ToString()},{i.Scale.ToString()}");
            }


            return line.ToString();            
        }

        public static string GetFileHeader(int images = 5)
        {
            var builder = new StringBuilder();
            builder.Append("Container Height, Container Width,Container Ratio");

            for (int i = 0; i < images; i++)
            {
                builder.Append($",Orig Image Height {i},Orig Image Width {i},Fitted Image Height {i},Fitted Image Width {i},Image Ratio {i}, Scale of image {i}");
            }

            return builder.ToString();
        }
    }
}
