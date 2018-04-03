using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.ListColorsElegantly
{
    public class ColorListBoxItem : ListBoxItem
    {
        string str;
        Rectangle rect;
        TextBlock text;

        public ColorListBoxItem()
        {
            // Create Stackpanel
            StackPanel stack = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Content = stack;

            // Create rect for a sample color
            rect = new Rectangle()
            {
                Width = 16,
                Height = 16,
                Margin = new Thickness(2),
                Stroke = SystemColors.WindowTextBrush
            };
            stack.Children.Add(rect);

            // Textblock for a color name
            text = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(text);
        }

        public string Text
        {
            set
            {
                str = value;
                string strSpaced = str[0].ToString();
                for (int i = 1; i < str.Length; i++)
                    strSpaced += (char.IsUpper(str[i]) ? " " : "")
                        + str[i].ToString();

                text.Text = strSpaced;
            }

            get
            {
                return str;
            }
        }

        public Color Color
        {
            set { rect.Fill = new SolidColorBrush(value); }
            get
            {
                SolidColorBrush brush = rect.Fill as SolidColorBrush;
                return brush == null ? Colors.Transparent : brush.Color;
            }
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            text.FontWeight = FontWeights.Bold;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            text.FontWeight = FontWeights.Regular;
        }

        public override string ToString()
        {
            return str;
        }
    }
}
