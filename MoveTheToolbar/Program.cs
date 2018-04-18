using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.MoveTheToolbar
{
    class MoveTheToolbar : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new MoveTheToolbar());
        }

        public MoveTheToolbar()
        {
            Title = "Move the toolbar";

            DockPanel dock = new DockPanel();
            Content = dock;

            // ToolBarTray
            ToolBarTray trayTop = new ToolBarTray();
            dock.Children.Add(trayTop);
            DockPanel.SetDock(trayTop, Dock.Top);

            ToolBarTray trayLeft = new ToolBarTray();
            dock.Children.Add(trayLeft);
            DockPanel.SetDock(trayLeft, Dock.Left);

            // TextBox
            TextBox txtbox = new TextBox();
            dock.Children.Add(txtbox);

            // 6 panels
            for (int i = 0; i<6; i++)
            {
                ToolBar toolbar = new ToolBar();
                toolbar.Header = "Toolbar " + (i + 1);
                if (i < 3)
                    trayTop.ToolBars.Add(toolbar);
                else
                    trayLeft.ToolBars.Add(toolbar);

                // and 6 buttons on each
                for (int j = 0; j<6; j++)
                {
                    Button btn = new Button()
                    {
                        FontSize = 16,
                        Content = (char)('A' + j)
                    };
                    toolbar.Items.Add(btn);
                }
            }
        }
    }
}
