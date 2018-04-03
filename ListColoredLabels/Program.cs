using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColoredLabels
{
    public class ListColoredLabels : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ListColoredLabels());
        }

        public ListColoredLabels()
        {
            Title = "List Colored Labels";

            ListBox lstbox = new ListBox()
            {
                Height = 150,
                Width = 150
            };
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // fill with labels
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Color clr = (Color)prop.GetValue(null, null);
                bool isBlack = (0.222 * clr.R + 0.707 * clr.G + 0.071 * clr.B) > 128;
                Label lbl = new Label()
                {
                    Content = prop.Name,
                    Background = new SolidColorBrush(clr),
                    Foreground = isBlack ? Brushes.Black : Brushes.White,
                    Width = 100,
                    Margin = new Thickness(15, 0, 0, 0),
                    Tag = clr
                };
                lstbox.Items.Add(lbl);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstbox = sender as ListBox;
            Label lbl = lstbox.SelectedItem as Label;
            if (lbl != null)
            {
                Color clr = (Color)lbl.Tag;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
