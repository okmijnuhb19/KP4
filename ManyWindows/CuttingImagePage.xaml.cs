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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManyWindows
{
    /// <summary>
    /// Логика взаимодействия для First.xaml
    /// </summary>
    public partial class CuttingImagePage : Page
    {
        Rectangle[] BlueRectangles;
        const int RectangleWidth = 15;
        BitmapImage img; 

        public CuttingImagePage(BitmapImage img, Rectangle border)
        {
            InitializeComponent();
            this.img = img;
            BuildEnviroment(img, border);
        }

        public void BuildEnviroment(BitmapImage img, Rectangle border)
        {
            this.ImageWithText.Source = img;
            this.ImageBorder.Height = border.ActualHeight;
            this.ImageBorder.Width = border.ActualWidth;

            Canvas.Width = border.ActualWidth;
            Canvas.Height = border.ActualHeight;

            InitializeBlueRectangles();
        }

        private void InitializeBlueRectangles()
        {
            CreateBlueRectangles();
            SetListenersForBlueRectangles();
            AddBlueRectanglesToCanvas();
            SetMouseMoveRecivers();
            LocateRectangles();
            ConnectRectangles();
        }

        private void CreateBlueRectangles()
        {
            BlueRectangles = new Rectangle[4];
            for (var i = 0; i < 4; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Stroke = new SolidColorBrush(Colors.Blue);
                rect.Fill = new SolidColorBrush(Colors.DarkBlue);
                rect.Width = RectangleWidth;
                rect.Height = RectangleWidth;
                BlueRectangles[i] = rect;
            }
        }

        private void SetListenersForBlueRectangles()
        {
            for (var i = 0; i < 4; i++)
            {
                BlueRectangles[i].MouseDown += this.Mouse_Down;
                BlueRectangles[i].MouseMove += this.Mouse_Move;
            }
        }

        private void AddBlueRectanglesToCanvas()
        {
            for (var i = 0; i < 4; i++)
                Canvas.Children.Add(BlueRectangles[i]);        
        }

        private void SetMouseMoveRecivers()
        {
            Canvas.MouseMove += this.Mouse_Move;
            ImageWithText.MouseMove += this.Mouse_Move;
            ImageBorder.MouseMove += this.Mouse_Move;
            GridImage.MouseMove += this.Mouse_Move;
        }

        private void LocateRectangles()
        {
            BlueRectangles[0].SetValue(Canvas.TopProperty, 0.0 - RectangleWidth / 2);
            BlueRectangles[0].SetValue(Canvas.LeftProperty, 0.0 - RectangleWidth / 2);

            BlueRectangles[1].SetValue(Canvas.TopProperty, 0.0 - RectangleWidth / 2);
            BlueRectangles[1].SetValue(Canvas.LeftProperty, (double)Canvas.Width - RectangleWidth / 2);

            BlueRectangles[2].SetValue(Canvas.TopProperty, (double)Canvas.Height - RectangleWidth / 2);
            BlueRectangles[2].SetValue(Canvas.LeftProperty, (double)Canvas.Width - RectangleWidth / 2);

            BlueRectangles[3].SetValue(Canvas.TopProperty, (double)Canvas.Height - RectangleWidth / 2);
            BlueRectangles[3].SetValue(Canvas.LeftProperty, 0.0 - RectangleWidth / 2);
        }

        private void CheckRectanglesPosition(Rectangle rect)
        {
            if (BlueRectangles[0] == rect)
                AcceptFirstRectChanges();
            if (BlueRectangles[1] == rect)
                AcceptSecondRectChanges();
            if (BlueRectangles[2] == rect)
                AcceptThirdRectChanges();
            if (BlueRectangles[3] == rect)
                AcceptFourthRectChanges();
        }

        private void AcceptFirstRectChanges()
        {
            var top = Canvas.GetTop(BlueRectangles[0]);
            var left = Canvas.GetLeft(BlueRectangles[0]);

            BlueRectangles[1].SetValue(Canvas.TopProperty, top);
            BlueRectangles[3].SetValue(Canvas.LeftProperty, left);
        }

        private void AcceptSecondRectChanges()
        {
            var top = Canvas.GetTop(BlueRectangles[1]);
            var left = Canvas.GetLeft(BlueRectangles[1]);

            BlueRectangles[0].SetValue(Canvas.TopProperty, top);
            BlueRectangles[2].SetValue(Canvas.LeftProperty, left);
        }

        private void AcceptThirdRectChanges()
        {
            var top = Canvas.GetTop(BlueRectangles[2]);
            var left = Canvas.GetLeft(BlueRectangles[2]);

            BlueRectangles[3].SetValue(Canvas.TopProperty, top);
            BlueRectangles[1].SetValue(Canvas.LeftProperty, left);
        }

        private void AcceptFourthRectChanges()
        {
            var top = Canvas.GetTop(BlueRectangles[3]);
            var left = Canvas.GetLeft(BlueRectangles[3]);

            BlueRectangles[2].SetValue(Canvas.TopProperty, top);
            BlueRectangles[0].SetValue(Canvas.LeftProperty, left);
        }

        private void ConnectRectangles()
        {
            for (var i = 0; i < 4; i++)
            {
                Line line = new Line();
                line.X1 = Canvas.GetLeft(BlueRectangles[i]) + RectangleWidth/2;
                line.Y1 = Canvas.GetTop(BlueRectangles[i]) + RectangleWidth/2;
                line.X2 = Canvas.GetLeft(BlueRectangles[(i+1)%4]) + RectangleWidth / 2;
                line.Y2 = Canvas.GetTop(BlueRectangles[(i+1)%4]) + RectangleWidth / 2;

                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.Blue;
                line.StrokeThickness = 1;
                line.Stroke = redBrush;

                Canvas.Children.Add(line);
            }
        }

        private void AcceptBlueRectangleChanges(Rectangle rect)
        {
            CheckRectanglesPosition(pressedObject);
            ClearLines();
            ConnectRectangles();
        }

        private void ClearLines()
        {
            Canvas.Children.RemoveRange(4, Canvas.Children.Count - 4);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rect = EvaluateCuttingRectangle();
            AfterCuttingFrame nextFrame = new AfterCuttingFrame(img, rect);
            this.NavigationService.Navigate(nextFrame);
        }

        private Int32Rect EvaluateCuttingRectangle()
        {
            double x = Canvas.GetLeft(BlueRectangles[0]);
            double y = Canvas.GetTop(BlueRectangles[0]);

            double alpha = img.PixelHeight / ImageBorder.Height;
            double beta = img.PixelWidth / ImageBorder.Width;

            double width =  Canvas.GetLeft(BlueRectangles[1]) - x;
            double height = Canvas.GetTop(BlueRectangles[3]) - y;

            x += RectangleWidth / 2;
            y += RectangleWidth / 2;

            int _x = (int)(x * beta);
            int _y = (int)(y * alpha);

            int _width = (int)(width * beta);
            int _height = (int)(height * alpha);

            return new Int32Rect(_x, _y, _width, _height);
        }

        Rectangle pressedObject;
        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            if (sender is Rectangle)
                pressedObject = sender as Rectangle;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(Canvas);

            if(point.X < 0 || point.Y < 0 || point.Y > Canvas.Height || point.X > Canvas.Width)
                return;

            if(pressedObject != null && e.LeftButton == MouseButtonState.Pressed)
            {
                pressedObject.SetValue(Canvas.TopProperty, point.Y - pressedObject.Height/2);
                pressedObject.SetValue(Canvas.LeftProperty, point.X - pressedObject.Width/2);

                AcceptBlueRectangleChanges(pressedObject);
            }
        }

        private void Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            pressedObject = null;
        }
    }
}
