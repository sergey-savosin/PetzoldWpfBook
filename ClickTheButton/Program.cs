using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ClickTheButton
{
    public class ClickTheButton : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ClickTheButton());
        }

        public ClickTheButton()
        {
            Title = "Click the button";

            Button btn = new Button();
            btn.Content = "_Click me, please!";
            //btn.Margin = new Thickness(96);
            //btn.Padding = new Thickness(96);

            //btn.HorizontalContentAlignment = HorizontalAlignment.Left;
            //btn.VerticalContentAlignment = VerticalAlignment.Bottom;

            //FontFamily = new FontFamily("Times New Roman");
            //FontSize = 48;

            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;

            //btn.Background = Brushes.AliceBlue;
            //btn.Foreground = Brushes.DarkSalmon;
            //btn.BorderBrush = Brushes.Magenta;

            btn.Click += ButtonOnClick;
            Content = btn;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked", Title);
        }
    }
}
