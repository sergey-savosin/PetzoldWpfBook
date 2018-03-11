using System;
using System.Windows;

namespace Petzold.GetMedieval
{
    public class GetMedieval : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new GetMedieval());
        }

        public GetMedieval()
        {
            Title = "Get Medieval";
            MedievalButton btn = new MedievalButton()
            {
                Text = "Click this button",
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5, 20, 5, 20)
            };
            btn.Knock += ButtonOnKnock;
            Content = btn;
        }

        private void ButtonOnKnock(object sender, RoutedEventArgs e)
        {
            MedievalButton btn = e.Source as MedievalButton;
            MessageBox.Show("The button labeled \"" + btn.Text + "\" has been knocked.", Title);
        }
    }
}
