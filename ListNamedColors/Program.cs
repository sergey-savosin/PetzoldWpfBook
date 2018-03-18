using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListNamedColors
{
    public class ListNamedColors : Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ListNamedColors());
        }

        public ListNamedColors()
        {
            Title = "List named colors / brushes";

            ListBox lstbox = new ListBox()
            {
                Width = 150,
                Height = 150,
                ItemsSource = NamedBrush.All,
                DisplayMemberPath = "Name",
                SelectedValuePath = "Brush"
            };

            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstbox.DataContext = this;
            //lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstbox = sender as ListBox;
            if (lstbox.SelectedValue != null)
            {
                Brush b = (Brush)lstbox.SelectedValue;
                Background = b;
            }
        }
    }
}
