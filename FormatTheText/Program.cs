using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.FormatTheText
{
    public class FormatTheText : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FormatTheText());
        }

        public FormatTheText()
        {
            Title = "Format the text";
            TextBlock txt = new TextBlock();
            txt.FontSize = 32; //24 points
            txt.Inlines.Add("This is some ");

            var r1 = new Run("italic");
            r1.MouseDown += RunOnMouseDown;
            txt.Inlines.Add(new Italic(r1));

            txt.Inlines.Add(" text, and this is some ");

            var r2 = new Run("bold");
            r2.MouseDown += RunOnMouseDown;
            txt.Inlines.Add(new Bold(r2));

            txt.Inlines.Add(" text, and this is some ");

            var r3 = new Run("bold italic");
            r3.MouseDown += RunOnMouseDown;
            txt.Inlines.Add(new Bold(new Italic(r3)));

            txt.Inlines.Add(" text.");
            txt.TextWrapping = TextWrapping.Wrap;
            Foreground = Brushes.CornflowerBlue;

            Content = txt;
        }

        void RunOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            Run run = sender as Run;

            if (args.ChangedButton == MouseButton.Left)
            {
                run.FontStyle = run.FontStyle == FontStyles.Italic ?
                    FontStyles.Normal : FontStyles.Italic;
            }
            else if (args.ChangedButton == MouseButton.Right)
            {
                run.FontWeight = run.FontWeight == FontWeights.Bold ?
                    FontWeights.Normal : FontWeights.Bold;
            }
        }
    }
}
