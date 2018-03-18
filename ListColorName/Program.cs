using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColorName
{
    public class ListColorName : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorName());
        }

        public ListColorName()
        {
            Title = "List color name";

            StackPanel pnl = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Content = pnl;

            ListBox lstbox = new ListBox()
            {
                Width = 150,
                Height = 150,
                Margin = new Thickness(24)
            };
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            pnl.Children.Add(lstbox);

            // fill colors
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach (PropertyInfo prop in props)
                lstbox.Items.Add(prop.Name);

            lstbox = new ListBox()
            {
                Width = 150,
                Height = 150,
                Margin = new Thickness(24)
            };
            lstbox.SelectionChanged += ListBox2OnSelectionChanged;
            pnl.Children.Add(lstbox);

            // fill colors 2
            foreach (PropertyInfo prop in props)
                lstbox.Items.Add(prop.GetValue(null, null));
        }

        private void ListBox2OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstbox = sender as ListBox;
            if (lstbox.SelectedIndex != -1)
            {
                Color clr = (Color)lstbox.SelectedItem;
                Background = new SolidColorBrush(clr);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstbox = sender as ListBox;
            string str = lstbox.SelectedItem as string;
            if (str != null)
            {
                Color clr =
                    (Color)typeof(Colors).GetProperty(str).GetValue(null, null);
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
