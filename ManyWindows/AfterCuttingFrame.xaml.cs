using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManyWindows
{
    /// <summary>
    /// Логика взаимодействия для AfterCuttingFrame.xaml
    /// </summary>
    public partial class AfterCuttingFrame : Page
    {
        private BitmapImage bmpImage;
        public AfterCuttingFrame(BitmapImage img, Int32Rect rect)
        {
            InitializeComponent();
            CutPartOfImage(img, rect);

        }

        private void CutPartOfImage(BitmapImage image, Int32Rect rect)
        {
            CroppedBitmap cb = new CroppedBitmap(image, rect);
            ImageWithText.Source = cb;

            ImageBorder.Stretch = Stretch.Fill;
            /*ImageBorder.Width = ImageWithText.ActualWidth + 10;
            ImageBorder.Height = ImageWithText.ActualHeight + 10;

            ImageBorder.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            ImageBorder.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;*/
            //EvaluateBorder(cb, rect);
        }

        private void EvaluateBorder(CroppedBitmap image, Int32Rect rect)
        {
            var newHeight = 0.0;
            var newWidth = 0.0;

            if ((rect.Height / rect.Width) < (GridImage.ActualHeight / GridImage.ActualWidth))
            {
                newHeight = (float)(rect.Height)
                * (GridImage.ActualWidth) / (rect.Width);

                newWidth = GridImage.ActualWidth - ImageWithText.Margin.Left;
            }
            else
            {
                newWidth = (float)(rect.Width)
                * (GridImage.ActualHeight) / (rect.Height);

                newHeight = GridImage.ActualHeight - ImageWithText.Margin.Top;
            }

            ImageBorder.Width = newWidth;
            ImageBorder.Height = newHeight;
        }

        private BitmapImage BitmapSourceToBitmapImage(BitmapSource bitmapSource)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            BitmapImage bImg = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);

            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(memoryStream.ToArray());
            bImg.EndInit();

            memoryStream.Close();

            return bImg;
        }
    

        private void RecBtn_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = BitmapSourceToBitmapImage(ImageWithText.Source as BitmapSource);
            RecognizePage recFrame = new RecognizePage(image);

            this.NavigationService.Navigate(recFrame);
        }
    }
}
