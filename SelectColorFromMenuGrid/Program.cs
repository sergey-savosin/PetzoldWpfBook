using Petzold.SelectColorFromGrid2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SelectColorFromMenuGrid
{
    class SelectColorFromMenuGrid : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromMenuGrid());
        }

        public SelectColorFromMenuGrid()
        {
            Title = "Select color from menu grid";

            DockPanel dock = new DockPanel();
            Content = dock;

            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // TextBlock
            TextBlock text = new TextBlock()
            {
                Text = Title,
                FontSize = 32,
                TextAlignment = TextAlignment.Center
            };

            dock.Children.Add(text);

            // menu items
            MenuItem itemColor = new MenuItem()
            {
                Header = "_Color"
            };
            menu.Items.Add(itemColor);

            MenuItem itemForeground = new MenuItem()
            {
                Header = "_Foreground"
            };
            itemColor.Items.Add(itemForeground);

            // colorGridBox
            ColorGridBox clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Foreground");
            clrbox.DataContext = this;
            itemForeground.Items.Add(clrbox);

            MenuItem itemBackground = new MenuItem()
            {
                Header = "_Background"
            };
            itemColor.Items.Add(itemBackground);

            clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrbox.DataContext = this;
            itemBackground.Items.Add(clrbox);


        }
    }
}
