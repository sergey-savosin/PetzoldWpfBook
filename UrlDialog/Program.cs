using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.NavigateTheWeb
{
    class NavigateTheWeb : Window
    {
        Frame frm;

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new NavigateTheWeb());
        }

        public NavigateTheWeb()
        {
            Title = "Navigate the web";

            frm = new Frame();
            Content = frm;

            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            UriDialog dlg = new UriDialog();
            dlg.Owner = this;
            dlg.Text = "http://";
            dlg.ShowDialog();

            try
            {
                frm.Source = new Uri(dlg.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }
        }
    }
}
