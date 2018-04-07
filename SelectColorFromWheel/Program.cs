using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SelectColorFromWheel
{
    class SelectColorFromWheel : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromWheel());
        }

        public SelectColorFromWheel()
        {
            Title = "Select Color From Wheel";
            SizeToContent = SizeToContent.WidthAndHeight;

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            // 1st button
            Button btn = new Button()
            {
                Content = "Do-nothing button\nto test tabbing",
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(btn);

            // color wheel
            ColorWheel clrwheel = new ColorWheel()
            {
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(clrwheel);

            clrwheel.SetBinding(ColorWheel.SelectedValueProperty, "Background");
            clrwheel.DataContext = this;

            // 2nd button
            btn = new Button()
            {
                Content = "Do-nothing button\nto test tabbing",
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(btn);


        }
    }
}
