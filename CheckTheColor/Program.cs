using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.CheckTheColor
{
    public class CheckTheColor : Window
    {
        TextBlock text;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new CheckTheColor());
        }

        public CheckTheColor()
        {
            Title = "Check the color";

            DockPanel dock = new DockPanel();
            Content = dock;

            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // TextBlock
            text = new TextBlock()
            {
                Text = Title,
                TextAlignment = TextAlignment.Center,
                FontSize = 32,
                Background = SystemColors.WindowBrush,
                Foreground = SystemColors.WindowTextBrush
            };
            dock.Children.Add(text);

            // menu items
            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);
            MenuItem itemForeground = new MenuItem()
            {
                Header = "_Foreground"
            };
            itemForeground.SubmenuOpened += ForegroundOnOpened;
            itemColor.Items.Add(itemForeground);
            FillWithColors(itemForeground, ForegroundOnClick);

            MenuItem itemBackground = new MenuItem()
            {
                Header = "_Background"
            };
            itemBackground.SubmenuOpened += BackgroundOnOpended;
            itemColor.Items.Add(itemBackground);
            FillWithColors(itemBackground, BackgroundOnClick);
        }

        private void BackgroundOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Background = new SolidColorBrush(clr);
        }

        private void BackgroundOnOpended(object sender, RoutedEventArgs e)
        {
            MenuItem itemParent = sender as MenuItem;
            foreach (MenuItem item in itemParent.Items)
            {
                item.IsChecked =
                    ((text.Background as SolidColorBrush).Color == (Color)item.Tag);
            }
        }

        private void ForegroundOnOpened(object sender, RoutedEventArgs e)
        {
            MenuItem itemParent = sender as MenuItem;
            foreach (MenuItem item in itemParent.Items)
            {
                item.IsChecked =
                    ((text.Foreground as SolidColorBrush).Color == (Color)item.Tag);
            }
        }

        private void ForegroundOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Foreground = new SolidColorBrush(clr);
        }

        private void FillWithColors(MenuItem itemParent, RoutedEventHandler handler)
        {
            foreach(PropertyInfo prop in typeof(Colors).GetProperties())
            {
                Color clr = (Color)prop.GetValue(null, null);
                int iCount = 0;
                iCount += clr.R == 0 || clr.R == 255 ? 1 : 0;
                iCount += clr.G == 0 || clr.G == 255 ? 1 : 0;
                iCount += clr.B == 0 || clr.B == 255 ? 1 : 0;
                if (clr.A == 255 && iCount > 1)
                {
                    Rectangle rect = new Rectangle()
                    {
                        Fill = new SolidColorBrush(clr),
                        Height = 12,
                        Width = 24
                    };

                    MenuItem item = new MenuItem()
                    {
                        Header = "_" + prop.Name,
                        Tag = clr,
                        Icon = rect
                    };
                    item.Click += handler;
                    itemParent.Items.Add(item);
                }
            }
        }
    }
}
