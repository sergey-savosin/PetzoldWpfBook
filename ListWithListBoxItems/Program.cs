using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListWithListBoxItems
{
    public class ListWithListBoxItems : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ListWithListBoxItems());
        }

        public ListWithListBoxItems()
        {
            Title = "List with ListBoxItems";

            ListBox lstbox = new ListBox()
            {
                Height = 150,
                Width = 150
            };
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // fill the ListBox
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                Color clr = (Color)prop.GetValue(null, null);
                bool isBlack = (0.222 * clr.R + 0.707 * clr.G + 0.071 * clr.B) > 128;
                ListBoxItem item = new ListBoxItem()
                {
                    Content = prop.Name,
                    Background = new SolidColorBrush(clr),
                    Foreground = isBlack ? Brushes.Black : Brushes.White,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Padding = new Thickness(2)
                };
                lstbox.Items.Add(item);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstbox = sender as ListBox;
            ListBoxItem item;

            if (e.RemovedItems.Count > 0)
            {
                item = e.RemovedItems[0] as ListBoxItem;
                String str = item.Content as string;
                item.Content = str.Substring(2, str.Length - 4);
                item.FontWeight = FontWeights.Regular;
                item.FontSize = FontSize;
            }

            if (e.AddedItems.Count > 0)
            {
                item = e.AddedItems[0] as ListBoxItem;
                String str = item.Content as string;
                item.Content = "[ " + str + " ]";
                item.FontWeight = FontWeights.Bold;
                item.FontSize *= 1.1;
            }

            item = lstbox.SelectedItem as ListBoxItem;
            if (item != null)
                Background = item.Background;
        }
    }
}
