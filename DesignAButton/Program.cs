using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Petzold.DesignAButton
{
    class DesignAButton : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new DesignAButton());
        }

        public DesignAButton()
        {
            Title = "Design a button";

            // Create button object
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;

            // Create StackPanel
            StackPanel stack = new StackPanel();
            btn.Content = stack;

            // Add polyline
            stack.Children.Add(ZigZag(10));

            // Add image
            Uri uri = new Uri("pack://application:,,/book06.ico");
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Margin = new Thickness(0, 10, 0, 0);
            img.Source = bitmap;
            img.Stretch = Stretch.None;
            stack.Children.Add(img);

            // Add label
            Label lbl = new Label();
            lbl.Content = "_Read books!";
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            stack.Children.Add(lbl);

            // Add Polyline
            stack.Children.Add(ZigZag(0));
        }

        private Polyline ZigZag(int offset)
        {
            Polyline poly = new Polyline();
            poly.Stroke = SystemColors.ControlTextBrush;
            poly.Points = new PointCollection();
            for (int x = 0; x <= 100; x+=10)
            {
                poly.Points.Add(new Point(x, (x + offset) % 20));
            }

            return poly;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button has been clicked!", Title);
        }
    }
}
