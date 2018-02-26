using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Petzold.MeetTheDocker
{
    class MeetTheDocker : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new MeetTheDocker());
        }

        public MeetTheDocker()
        {
            Title = "Meet the docker";

            DockPanel dock = new DockPanel();
            Content = dock;

            // Create menu
            Menu menu = new Menu();
            MenuItem item = new MenuItem();
            item.Header = "Menu";
            menu.Items.Add(item);

            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            // Create toolbar
            ToolBar tool = new ToolBar();
            tool.Header = "Toolbar";

            DockPanel.SetDock(tool, Dock.Top);
            dock.Children.Add(tool);

            // Status bar
            StatusBar status = new StatusBar();
            StatusBarItem statitem = new StatusBarItem();
            statitem.Content = "Status";
            status.Items.Add(statitem);

            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);

            // list box
            ListBox lstbox = new ListBox();
            lstbox.Items.Add("List box item");

            DockPanel.SetDock(lstbox, Dock.Left);
            dock.Children.Add(lstbox);

            // Text box
            TextBox txtbox = new TextBox();
            txtbox.AcceptsReturn = true;

            dock.Children.Add(txtbox);

            txtbox.Focus();
        }
    }
}
