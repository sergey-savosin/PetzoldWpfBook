using System;
using System.Windows;
using System.Windows.Media;

namespace Petzold.RenderTheBetterEllipse
{
    public class RenderTheBetterEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RenderTheBetterEllipse());
        }

        public RenderTheBetterEllipse()
        {
            Title = "Render the Better Ellipse";

            BetterEllipse ellips = new BetterEllipse()
            {
                Fill = Brushes.Plum,
                Stroke = new Pen(
                    new LinearGradientBrush(
                        Colors.CadetBlue, Colors.Chocolate, new Point(1, 0), new Point(0, 1)),
                    24 /* 1/4 inch */)
            };
            Content = ellips;
        }
    }
}
