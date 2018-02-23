using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Petzold.ToggleTheButton
{
    class ToggleTheButton : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ToggleTheButton());
        }

        public ToggleTheButton()
        {
            Title = "Toggle the button";
            CheckBox btn = new CheckBox();
            btn.Content = "Can _Resize";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            //btn.IsChecked = (ResizeMode == ResizeMode.CanResize);
            //btn.Checked += ButtonOnChecked;
            //btn.Unchecked += ButtonOnChecked;

            Binding bind = new Binding("Topmost");
            bind.Source = this;
            btn.SetBinding(ToggleButton.IsCheckedProperty, bind);

            ToolTip tt = new ToolTip();
            tt.Content = "Toggle the button on to make " +
                "the window topmost on the desktop";
            btn.ToolTip = tt;

            Content = btn;
        }

        private void ButtonOnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;
            ResizeMode = btn.IsChecked.Value ? ResizeMode.CanResizeWithGrip :
                ResizeMode.NoResize;
        }
    }
}
