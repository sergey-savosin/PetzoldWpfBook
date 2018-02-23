using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.Stack30Buttons
{
    class Stack30Buttons : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new Stack30Buttons());
        }

        public Stack30Buttons()
        {
            Title = "Stack 30 buttons";
            SizeToContent = SizeToContent.Width;
            //ResizeMode = ResizeMode.CanMinimize;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            ScrollViewer scroll = new ScrollViewer();
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Margin = new Thickness(5);
            scroll.Content = stack;
            Content = scroll;

            for (int i = 0; i<3; i++)
            {
                StackPanel stackChild = new StackPanel();
                stack.Children.Add(stackChild);
                for (int j=0; j<20; j++)
                {
                    Button btn = new Button();
                    btn.Content = "Button #" + (10 * i + j + 1);
                    btn.Margin = new Thickness(5);
                    stackChild.Children.Add(btn);
                }
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            if (btn != null)
            {
                MessageBox.Show("You clicked the button labeled " + (e.Source as Button).Content);
            }
            else
            {
                MessageBox.Show("Clicked something else: " + e.Source.ToString());
            }
        }
    }
}
