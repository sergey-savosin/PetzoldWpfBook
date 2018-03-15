using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.SelectColor
{
    class SelectColor : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColor());
        }

        public SelectColor()
        {
            Title = "Select Color";
            SizeToContent = SizeToContent.WidthAndHeight;

            // create StackPanel
            StackPanel stack = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Content = stack;

            // create button
            Button btn = new Button()
            {
                Content = "Do-nothing button\nto test tabbing",
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(btn);

            // create ColorGrid
            ColorGrid clrgrid = new ColorGrid()
            {
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            clrgrid.SelectedColorChanged += ColorGridOnSelectedColorChanged;
            stack.Children.Add(clrgrid);

            // other button
            btn = new Button()
            {
                Content = "Do-nothing button\nto test tabbing",
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(btn);

        }

        private void ColorGridOnSelectedColorChanged(object sender, EventArgs e)
        {
            ColorGrid clrgrid = sender as ColorGrid;
            Background = new SolidColorBrush(clrgrid.SelectedColor);
        }
    }
}
