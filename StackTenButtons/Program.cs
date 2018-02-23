using System;
using System.Windows;
using System.Windows.Controls;

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
            Content = stack;

            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button();
                btn.Name = ((char)('A' + i)).ToString();
                btn.FontSize += rnd.Next(10);
                btn.Content = "Button " + btn.Name + " says 'Click me'";
                btn.Click += ButtonOnClick;
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
