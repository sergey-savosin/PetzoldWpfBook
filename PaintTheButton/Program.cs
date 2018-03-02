using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.PaintTheButton
{
    class PaintTheButton: Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new PaintTheButton());
        }

        public PaintTheButton()
        {
            Title = "Paint the button";

            // Create button
            Button btn = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Content = btn;

            // create Canvas
            Canvas cnv = new Canvas()
            {
                Width = 144,
                Height = 144
            };
            btn.Content = cnv;

            // rect
            Rectangle rect = new Rectangle()
            {
                Width = cnv.Width,
                Height = cnv.Height,
                RadiusX = 24,
                RadiusY = 24,
                Fill = Brushes.Blue
            };

            cnv.Children.Add(rect);
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);

            // polygon
            Polygon poly = new Polygon()
            {
                Fill = Brushes.Yellow,
                Points = new PointCollection()
            };
            for (int i = 0; i<5; i++)
            {
                double angle = i * 4 * Math.PI / 5;
                Point pt = new Point(
                    48 * Math.Sin(angle),
                    - 48 * Math.Cos(angle));
                poly.Points.Add(pt);
            }
            cnv.Children.Add(poly);
            Canvas.SetLeft(poly, cnv.Width / 2);
            Canvas.SetTop(poly, cnv.Height / 2);
        }

    }
}
