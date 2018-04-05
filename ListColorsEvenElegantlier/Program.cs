using Petzold.ListNamedColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace Petzold.ListColorsEvenElegantlier
{
    class ListColorsEvenElegantlier : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorsEvenElegantlier());
        }

        public ListColorsEvenElegantlier()
        {
            Title = "List colors even elegantlier";

            DataTemplate template = new DataTemplate(typeof(NamedBrush));

            // StackPanel
            FrameworkElementFactory factoryStack = new FrameworkElementFactory(typeof(StackPanel));
            factoryStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            // set root
            template.VisualTree = factoryStack;

            // Rectangle
            FrameworkElementFactory factoryRectangle = new FrameworkElementFactory(typeof(Rectangle));
            factoryRectangle.SetValue(Rectangle.WidthProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.HeightProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.MarginProperty, new Thickness(2));
            factoryRectangle.SetValue(Rectangle.StrokeProperty, SystemColors.WindowTextBrush);
            factoryRectangle.SetBinding(Rectangle.FillProperty, new Binding("Brush"));

            factoryStack.AppendChild(factoryRectangle);

            // TextBlock
            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            factoryTextBlock.SetValue(TextBlock.TextProperty, new Binding("Name"));

            factoryStack.AppendChild(factoryTextBlock);

            // ListBox
            ListBox lstbox = new ListBox()
            {
                Width = 150,
                Height = 150,
                ItemTemplate = template,
                ItemsSource = NamedBrush.All,
                SelectedValuePath = "Brush",
                DataContext = this
            };
            Content = lstbox;
            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
        }
    }
}
