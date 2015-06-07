using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManyWindows
{
    /// <summary>
    /// Логика взаимодействия для BrowsePage.xaml
    /// </summary>
    public partial class BrowsePage : Page
    {
        public BrowsePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileName = RequestFileName();
            if (fileName == "error")
                return;
            var img = LoadImage(fileName);
            var newHeight = 0.0;
            var newWidth = 0.0;

            ImageWithText.Source = new BitmapImage();

            if ((img.Height / img.Width) < (GridImage.ActualHeight / GridImage.ActualWidth))
            {
                newHeight = (float)(img.Height)
                * (GridImage.ActualWidth) / (img.Width);

                newWidth = GridImage.ActualWidth - ImageWithText.Margin.Left;
            }
            else
            {
                newWidth = (float)(img.Width)
                * (GridImage.ActualHeight) / (img.Height);

                newHeight = GridImage.ActualHeight - ImageWithText.Margin.Top;
            }

            HeightAnimation(ImageBorder, (int)newWidth, (int)newHeight, img);
        }

        private void HeightAnimation(Rectangle border, int newWidth, int newHeight, BitmapImage source)
        {
            var animation2 = new DoubleAnimation();
            animation2.From = border.ActualWidth;
            animation2.To = newWidth;
            animation2.Duration = TimeSpan.FromSeconds(0.5);

            var animation = new DoubleAnimation();
            animation.From = border.ActualHeight;
            animation.To = newHeight;
            animation.Duration = TimeSpan.FromSeconds(0.5);

            animation2.Completed += new EventHandler((x, y) =>
            {
                ImageAppearing(ImageWithText, source);
            });

            border.BeginAnimation(HeightProperty, animation);
            border.BeginAnimation(WidthProperty, animation2);
        }

        private void ImageAppearing(Image img, BitmapImage source)
        {
            var animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.To = 1.0;
            animation.Duration = TimeSpan.FromSeconds(0.5);
            animation.Completed += new EventHandler((x, y) =>
            {
                //TODO
                this.ImageBorder.Width = double.NaN;
                this.ImageBorder.Height = double.NaN;
            });

            img.Opacity = 0;
            img.Source = source;
            img.BeginAnimation(OpacityProperty, animation);

        }

        private string RequestFileName()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image file|*.bmp;*.jpg;*.png";

            var result = dlg.ShowDialog();
            if (result == true)
                return dlg.FileName;
            else
                return "error";
        }

        private BitmapImage LoadImage(string fileName)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(fileName);
            img.EndInit();
            return img;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CuttingImagePage first = new CuttingImagePage(ImageWithText.Source as BitmapImage, ImageBorder);
            this.NavigationService.Navigate(first);
        }
    }
}
