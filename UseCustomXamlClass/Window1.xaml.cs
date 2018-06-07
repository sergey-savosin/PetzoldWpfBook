//---------------------------------------------------
// UseCustomXamlClass.cs (c) 2006 by Charles Petzold
//---------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.UseCustomXamlClass
{
    public partial class Window1 : Window
    {
//        [STAThread]
//        public static void Main()
//        {
//            Application app = new Application();
//            app.Run(new UseCustomXamlClass());
//        }
        public Window1()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                CenteredButton btn = new CenteredButton();
                btn.Content = "Button No. " + (i + 1);
                stack.Children.Add(btn);
            }
        }
    }
}
