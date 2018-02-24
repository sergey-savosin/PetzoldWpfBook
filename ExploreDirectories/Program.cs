using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ExploreDirectories
{
    class ExploreDirectories : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ExploreDirectories());
        }

        public ExploreDirectories()
        {
            Title = "Explore directories";

            ScrollViewer scroll = new ScrollViewer();
            Content = scroll;

            WrapPanel wrap = new WrapPanel();
            scroll.Content = wrap;

            wrap.Children.Add(new FileSystemInfoButton());
        }

    }
}
