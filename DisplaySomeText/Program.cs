using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.DisplaySomeText
{
    public class DisplaySomeText : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DisplaySomeText());
        }

        public DisplaySomeText()
        {
            FontFamily = new FontFamily("Times New Roman");
            FontSize = 32;
            FontStyle = FontStyles.Oblique;
            Brush brush = new LinearGradientBrush(Colors.Black, Colors.White,
                new Point(0, 0), new Point(1, 1));
            Background = brush;
            Foreground = brush;
            Title = "Display Some Text";
            //Content = "Content can be russian!";
            Content = new int[57];
            //SizeToContent = SizeToContent.WidthAndHeight;

            BorderBrush = Brushes.SaddleBrown;
            BorderThickness = new Thickness(25, 50, 75, 100);
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            string str = Content as string;
            if (e.Text == "\b")
            {
                if (str.Length > 0)
                    str = str.Substring(0, str.Length - 1);
            }
            else
            {
                str += e.Text;
            }
            Content = str;
        }

    }
}
