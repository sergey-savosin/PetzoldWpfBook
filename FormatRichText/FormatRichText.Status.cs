using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace Petzold.FormatRichText
{
    partial class FormatRichText : Window
    {
        StatusBarItem itemDateTime;

        void AddStatusBar(DockPanel dock)
        {
            StatusBar status = new StatusBar();
            dock.Children.Add(status);
            DockPanel.SetDock(status, Dock.Bottom);

            itemDateTime = new StatusBarItem()
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            status.Items.Add(itemDateTime);

            // create timer for refresh
            DispatcherTimer tmr = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            tmr.Tick += TmrOnTick;
            tmr.Start();
        }

        private void TmrOnTick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            itemDateTime.Content = dt.ToLongDateString() + " " + dt.ToLongTimeString();
        }
    }
}
