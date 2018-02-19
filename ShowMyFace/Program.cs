using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Petzold.ShowMyFace
{
    public class ShowMyFace : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShowMyFace());
        }

        public ShowMyFace()
        {
            Title = "Show my face";
            Uri uri = new Uri("http://www.charlespetzold.com/PetzoldTattoo.jpg");
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmap;
            Content = img;
        }
    }
}