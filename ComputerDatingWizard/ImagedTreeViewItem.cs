using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Petzold.RecurceDirectoriesIncrementally
{
    public class ImagedTreeViewItem : TreeViewItem
    {
        TextBlock text;
        Image img;
        ImageSource srcSelected, srcUnselected;

        public ImagedTreeViewItem()
        {
            StackPanel stack = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Header = stack;

            img = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 2, 0)
            };
            stack.Children.Add(img);

            text = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(text);
        }

        // public properties
        public string Text
        {
            set { text.Text = value; }
            get { return text.Text; }
        }

        public ImageSource SelectedImage
        {
            set
            {
                srcSelected = value;
                if (IsSelected)
                    img.Source = srcSelected;
            }
            get { return srcSelected; }
        }

        public ImageSource UnselectedImage
        {
            set
            {
                srcUnselected = value;
                if (!IsSelected)
                    img.Source = srcUnselected;
            }
            get { return srcUnselected; }
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            img.Source = srcSelected;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            img.Source = srcUnselected;
        }
    }
}
