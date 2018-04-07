using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.CircleTheButtons
{
    class CircleTheButtons : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new CircleTheButtons());
        }

        public CircleTheButtons()
        {
            Title = "Circle the buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            RadialPanel pnl = new RadialPanel()
            {
                Orientation = RadialPanelOrientation.ByHeight,
                ShowPieLines = true
            };

            Content = pnl;

            Random rnd = new Random();
            for (int i = 0; i<6; i++)
            {
                Button btn = new Button()
                {
                    Content = "Button number " + (i + 1),
                    VerticalAlignment = VerticalAlignment.Center
                    
                };
                btn.FontSize += rnd.Next(10);
                pnl.Children.Add(btn);
            }
        }
    }
}
