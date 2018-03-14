using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.EncloseElementInEllipse
{
    public class EncloseElementInEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EncloseElementInEllipse());
        }

        public EncloseElementInEllipse()
        {
            Title = "Enclose element in ellipse";
            EllipseWithChild elips = new EllipseWithChild()
            {
                Fill = Brushes.ForestGreen,
                Stroke = new Pen(Brushes.Magenta, 24)
            };
            Content = elips;

            TextBlock text = new TextBlock()
            {
                FontFamily = new FontFamily("Times New Roman"),
                FontSize = 48,
                Text = "Text inside ellipse"
            };

            elips.Child = text;
        }

    }
}
