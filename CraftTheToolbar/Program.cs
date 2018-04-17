using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.CraftTheToolbar
{
    class CraftTheToolbar : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new CraftTheToolbar());
        }

        public CraftTheToolbar()
        {
            Title = "Craft The Toolbar";
            RoutedUICommand[] comm =
            {
                ApplicationCommands.New, ApplicationCommands.Open,
                ApplicationCommands.Save, ApplicationCommands.Print,
                ApplicationCommands.Cut, ApplicationCommands.Copy,
                ApplicationCommands.Paste, ApplicationCommands.Delete
            };

            string[] strImages =
            {
                "inspect.png", "gototool.png", "watch.png",
                "camera.png", "snapshotGlyph.png", "TimelineMarkPurple.png",
                "Usermark.png", "delete.png"
            };

            DockPanel dock = new DockPanel();
            dock.LastChildFill = false;
            Content = dock;

            // up toolbar
            ToolBar toolbar = new ToolBar();
            dock.Children.Add(toolbar);
            DockPanel.SetDock(toolbar, Dock.Top);

            // Create toolbar buttons
            for (int i = 0; i < 8; i++)
            {
                if (i == 4)
                    toolbar.Items.Add(new Separator());

                // image
                Image img = new Image()
                {
                    Source = new BitmapImage(
                        new Uri("pack://application:,,/images/" + strImages[i])),
                    Stretch = Stretch.None
                };

                // tooltip
                ToolTip tip = new ToolTip()
                {
                    Content = comm[i].Text
                };

                // button
                Button btn = new Button()
                {
                    Command = comm[i],
                    Content = img,
                    ToolTip = tip
                };
                toolbar.Items.Add(btn);

                // bindings
                CommandBindings.Add(
                    new CommandBinding(comm[i], ToolBarButtonOnClick));
            }
        }

        private void ToolBarButtonOnClick(object sender, ExecutedRoutedEventArgs e)
        {
            RoutedUICommand comm = e.Command as RoutedUICommand;
            MessageBox.Show(comm.Name + " command not yet implemented", Title);
        }
    }
}
