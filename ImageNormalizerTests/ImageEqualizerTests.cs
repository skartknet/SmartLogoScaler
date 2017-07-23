using System;
using NUnit.Framework;
using ImageNormalizer;
using System.Drawing;

namespace ImageNormalizerTests
{
    [TestFixture]
    public class ImageEqualizerTests
    {


        [TestCase(100, 20, 100, 50)]
        [TestCase(250, 200, 100, 50)]
        [TestCase(310, 20, 100, 50)]
        public void ItShouldFitImageWidth(int imgWidth, int imgHeight, int contWidth, int contHeight)
        {
            var imageSize = new Size(imgWidth, imgHeight);
            var contSize = new Size(contWidth, contHeight);

            var d = ImageEqualizer.FitImageSizeToContainer(imageSize, contSize);

            Assert.AreEqual(d.Width, contWidth);

        }



        [TestCase(100, 200, 100, 50, "25.00")]
        [TestCase(250, 300, 100, 50, "41.66")]
        [TestCase(110, 210, 100, 50, "26.19")]
        public void ItShouldFitImageHeight(int imgWidth, int imgHeight, int contWidth, int contHeight, int expectedWidth)
        {
            //the expected height is the container height
            var imageSize = new Size(imgWidth, imgHeight);
            var contSize = new Size(contWidth, contHeight);

            var d = ImageEqualizer.FitImageSizeToContainer(imageSize, contSize);

            Assert.AreEqual(d.Height.ToString("n2"), expectedWidth);

        }

        [TestCase(10, 5, ExpectedResult = 2)]
        //[TestCase(2, 50, ExpectedResult = 0.04)]
        //[TestCase(4, 3, ExpectedResult = 1.333)]
        public float GetRatioTest(int width, int height)
        {
            var size = new Size(width, height);
            var d = ImageEqualizer.GetRatio(size);

            return d;
        }

        public void ItShouldCenterImage()
        {

        }

        public void ItShouldGetObjectCenter()
        {

        }
    }
}
