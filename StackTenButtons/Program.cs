using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.StackTenButtons
{
    class StackTenButtons : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new StackTenButtons());
        }

        public StackTenButtons()
        {
            Title = "Stack ten buttons";

            StackPanel stack = new StackPanel();
            stack.Background = Brushes.Aquamarine;
            //stack.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Margin = new Thickness(5);

            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));
            Content = stack;

            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button();
                //btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.Margin = new Thickness(5);
                btn.Name = ((char)('A' + i)).ToString();
                btn.FontSize += rnd.Next(10);
                btn.Content = "Button " + btn.Name + " says 'Click me'";
                //btn.Click += ButtonOnClick;
                stack.Children.Add(btn);
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            MessageBox.Show("Button " + btn.Name + " has been clicked", Title);
        }
    }
}
