using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnimateHanoi
{
    class Disc
    {
        private Brush brush;
        private MainWindow mainWindow;
        private Canvas canvas;
        private double width, height;
        private Rectangle discRectangle;
        private double x, y;
        
        public Disc(double width, double height, MainWindow mainWindow, Canvas canvas, Color color)
        {
            this.mainWindow = mainWindow;
            this.canvas = canvas;
            this.width = width;
            this.height = height;

            mainWindow.Dispatcher.Invoke(new Action(delegate ()
            {
                brush = new SolidColorBrush(color);

                discRectangle = new Rectangle();

                discRectangle.Fill = brush;
                discRectangle.Visibility = Visibility.Visible;

                canvas.Children.Add(discRectangle);
              

            }));
        }

        public double X
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }

        public double Width
        {
            get { return width; }
        }

        public double Height
        {
            get { return height; }
        }

        public void update(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void draw()
        {
            mainWindow.Dispatcher.Invoke(new Action(delegate ()
            {

                discRectangle.InvalidateVisual();
                discRectangle.Width = width;
                discRectangle.Height = height;
                Canvas.SetTop(discRectangle, y);
                Canvas.SetLeft(discRectangle, x);

                discRectangle.Visibility = Visibility.Visible;               

            }));
        }
    }
}
