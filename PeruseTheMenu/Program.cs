using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.PeruseTheMenu
{
    class PeruseTheMenu : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new PeruseTheMenu());
        }

        public PeruseTheMenu()
        {
            Title = "Peruse the menu";

            DockPanel dock = new DockPanel();
            Content = dock;

            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            TextBlock text = new TextBlock()
            {
                Text = Title,
                FontSize = 32, // 24 points
                TextAlignment = TextAlignment.Center
            };
            dock.Children.Add(text);

            // File menu
            MenuItem itemFile = new MenuItem() { Header = "_File" };
            menu.Items.Add(itemFile);

            MenuItem itemNew = new MenuItem() { Header = "_New" };
            itemNew.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemNew);

            MenuItem itemOpen = new MenuItem() { Header = "_Open" };
            itemOpen.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemOpen);

            MenuItem itemSave = new MenuItem() { Header = "_Save" };
            itemSave.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemSave);

            itemFile.Items.Add(new Separator());

            MenuItem itemExit = new MenuItem() { Header = "E_xit" };
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);

            // Window menu
            MenuItem itemWindow = new MenuItem() { Header = "_Window" };
            menu.Items.Add(itemWindow);

            MenuItem itemTaskbar = new MenuItem() { Header = "_Show in Taskbar", IsCheckable = true };
            // itemTaskbar.IsChecked = ShowInTaskbar;
            // itemTaskbar.Click += TaskbarOnClick;
            itemTaskbar.SetBinding(MenuItem.IsCheckedProperty, "ShowInTaskbar");
            itemTaskbar.DataContext = this;
            itemWindow.Items.Add(itemTaskbar);

            MenuItem itemSize = new MenuItem() { Header = "Size to _Content", IsCheckable = true, IsChecked = SizeToContent == SizeToContent.WidthAndHeight };
            itemSize.Checked += SizeOnCheck;
            itemSize.Unchecked += SizeOnCheck;
            itemWindow.Items.Add(itemSize);

            MenuItem itemResize = new MenuItem() { Header = "_Resizable", IsCheckable = true, IsChecked = ResizeMode == ResizeMode.CanResize };
            itemResize.Checked += ResizeOnCheck;
            itemResize.Unchecked += ResizeOnCheck;
            itemWindow.Items.Add(itemResize);

            MenuItem itemTopmost = new MenuItem() { Header = "_Topmost", IsCheckable = true, IsChecked = Topmost };
            itemTopmost.Checked += TopmostOnCheck;
            itemTopmost.Unchecked += TopmostOnCheck;
            itemWindow.Items.Add(itemTopmost);
        }

        private void TopmostOnCheck(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Topmost = item.IsChecked;
        }

        private void ResizeOnCheck(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ResizeMode = item.IsChecked ? ResizeMode.CanResize : ResizeMode.NoResize;
        }

        private void SizeOnCheck(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            SizeToContent = item.IsChecked ? SizeToContent.WidthAndHeight : SizeToContent.Manual;
        }

        private void TaskbarOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ShowInTaskbar = item.IsChecked;
        }

        private void UnimplementedOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            string strItem = item.Header.ToString().Replace("_", "");
            MessageBox.Show("The " + strItem + " option has not yet been implemented", Title);
        }

        private void ExitOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
