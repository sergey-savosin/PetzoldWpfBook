using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SelectColorFromGrid
{
    class SelectColorFromGrid : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromGrid());
        }

        public SelectColorFromGrid()
        {
            Title = "Select color from grid";
            SizeToContent = SizeToContent.WidthAndHeight;

            // StackPanel
            StackPanel stack = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Content = stack;

            // a button
            Button btn = new Button()
            {
                Content = "Do nothing button\nto test tabbig",
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(btn);

            // ColorGridBox
            ColorGridBox clrgrid = new ColorGridBox()
            {
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(clrgrid);

            clrgrid.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrgrid.DataContext = this;

            // other button
            btn = new Button()
            {
                Content = "Do-nothing button\tto test tabbing",
                Margin = new Thickness(24),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(btn);

        }
    }
}
