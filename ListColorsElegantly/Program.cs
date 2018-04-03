using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Petzold.ListColorsElegantly
{
    class ListColorsElegantly : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorsElegantly());
        }

        public ListColorsElegantly()
        {
            Title = "List Colors Elegantly";
            ColorListBox lstbox = new ColorListBox()
            {
                Height = 150,
                Width = 150
            };
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // init listbox
            lstbox.SelectedColor = SystemColors.WindowColor;
        }

        private void ListBoxOnSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ColorListBox lstbox = sender as ColorListBox;
            Background = new SolidColorBrush(lstbox.SelectedColor);
        }
    }
}
