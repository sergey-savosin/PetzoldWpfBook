//--------------------------------------------------
// RoutedEventDemo.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.RoutedEventDemo
{
    public partial class RoutedEventDemo : Window
    {
//        [STAThread]
//        public static void Main()
//        {
//            Application app = new Application();
//            app.Run(new RoutedEventDemo());
//        }
        public RoutedEventDemo()
        {
            InitializeComponent();
        }
        void MenuItemOnClick(object sender, RoutedEventArgs args)
        {
            string str = (args.Source as MenuItem).Header as string;
            Color clr = (Color)ColorConverter.ConvertFromString(str);
            txtblk.Foreground = new SolidColorBrush(clr);
        }
    }
}
