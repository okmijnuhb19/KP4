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
    /// Логика взаимодействия для RecognizeFrame.xaml
    /// </summary>
    /// 
    public partial class RecognizePage : Page
    {
        private struct MatchedSymbol { public int percent; public char symbol; }

        public RecognizePage(BitmapImage image)
        {
            InitializeComponent();
            KohonenNetwork kn = new KohonenNetwork();
            TextDetector td = new TextDetector();
            Recognizer rg = new Recognizer(kn, td);

            System.Drawing.Bitmap bmp = BitmapImage2Bitmap(image);
            RecognizedText.Text = rg.Parse(bmp);
            PrintMatching(rg.GetMatchingPercents());
        }

        private void PrintMatching(double[][] matching)
        {
            
        }

        private MatchedSymbol[] GetMatchedSymbols(double[] matches)
        {
            var ms = new MatchedSymbol[26];
            var symbol = 'A';
            for (var i = 0; i < 26; i++)
            {
                ms[i].symbol = symbol++;
                ms[i].percent = (int)(matches[i] * 100);
            }

            return ms;
        }

        private System.Drawing.Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new System.Drawing.Bitmap(bitmap);
            }
        }
    }
}
