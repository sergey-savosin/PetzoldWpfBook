using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.DockAroundTheBlock
{
    class DockAroundTheBlock : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new DockAroundTheBlock());
        }

        public DockAroundTheBlock()
        {
            Title = "Dock around the block";

            DockPanel dock = new DockPanel();
            //dock.LastChildFill = false;
            Content = dock;

            for (int i = 0; i<17; i++)
            {
                Button btn = new Button();
                btn.Content = "Button # " + (i + 1);
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                dock.Children.Add(btn);
                btn.SetValue(DockPanel.DockProperty, (Dock)(i % 4));
            }
        }

    }
}
