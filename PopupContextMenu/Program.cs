using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Petzold.PopupContextMenu
{
    class PopupContextMenu : Window
    {
        ContextMenu menu;
        MenuItem itemBold, itemItalic;
        MenuItem[] itemDecor;
        Inline inlClicked;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new PopupContextMenu());
        }

        public PopupContextMenu()
        {
            Title = "Popup context menu";
            // context menu
            menu = new ContextMenu();

            itemBold = new MenuItem()
            {
                Header = "Bold"
            };
            menu.Items.Add(itemBold);

            itemItalic = new MenuItem()
            {
                Header = "Italic"
            };
            menu.Items.Add(itemItalic);

            // get all textdecorations
            TextDecorationLocation[] locs =
                (TextDecorationLocation[])Enum.GetValues(typeof(TextDecorationLocation));

            itemDecor = new MenuItem[locs.Length];
            for (int i=0; i < locs.Length; i++)
            {
                TextDecoration decor = new TextDecoration()
                {
                    Location = locs[i]
                };
                itemDecor[i] = new MenuItem()
                {
                    Header = locs[i].ToString(),
                    Tag = decor
                };
                menu.Items.Add(itemDecor[i]);
            }

            // shared handler
            menu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(MenuOnClick));

            // textblock
            TextBlock text = new TextBlock()
            {
                FontSize = 32,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Content = text;

            string strQuote = "To be, or not to be, that is the question";
            string[] strWords = strQuote.Split();

            // Run object
            foreach (string str in strWords)
            {
                Run run = new Run(str)
                {
                    TextDecorations = new TextDecorationCollection()
                };
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }

        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if ((inlClicked = e.Source as Inline) != null)
            {
                itemBold.IsChecked = (inlClicked.FontWeight == FontWeights.Bold);
                itemItalic.IsChecked = (inlClicked.FontStyle == FontStyles.Italic);

                foreach (MenuItem item in itemDecor)
                {
                    item.IsChecked = (inlClicked.TextDecorations.Contains(item.Tag as TextDecoration));
                }

                menu.IsOpen = true;
                e.Handled = true;
            }
        }

        private void MenuOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;
            item.IsChecked ^= true;

            if (item == itemBold)
                inlClicked.FontWeight = (item.IsChecked ? FontWeights.Bold : FontWeights.Normal);
            else if (item == itemItalic)
                inlClicked.FontStyle = (item.IsChecked ? FontStyles.Italic : FontStyles.Normal);
            else
            {
                if (item.IsChecked)
                    inlClicked.TextDecorations.Add(item.Tag as TextDecoration);
                else
                    inlClicked.TextDecorations.Remove(item.Tag as TextDecoration);
            }
            (inlClicked.Parent as TextBlock).InvalidateVisual();
        }
    }
}
