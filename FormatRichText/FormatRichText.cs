using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.FormatRichText
{
    partial class FormatRichText : Window
    {
        RichTextBox txtbox;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            try
            {
                app.Run(new FormatRichText());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Source: " + ex.Source
                    + "\r\nMessage: " + ex.Message
                    + "\r\nTarget Site: " + ex.TargetSite.Name
                    + "\r\nInner Exception: " + ex.InnerException?.Message
                    + "\r\nStack trace: " + ex.StackTrace,
                    "FormatRichText");
            }
        }

        public FormatRichText()
        {
            Title = "Format Rich Text";

            DockPanel dock = new DockPanel();
            Content = dock;

            ToolBarTray tray = new ToolBarTray();
            dock.Children.Add(tray);
            DockPanel.SetDock(tray, Dock.Top);

            txtbox = new RichTextBox()
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            AddFileToolBar(tray, 0, 0);
            AddEditToolBar(tray, 1, 0);
            AddCharToolBar(tray, 2, 0);
            AddParaToolBar(tray, 2, 1);
            AddStatusBar(dock);

            dock.Children.Add(txtbox);
            txtbox.Focus();
        }
    }
}
