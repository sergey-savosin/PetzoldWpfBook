using Petzold.CircleTheButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.SelectColorFromWheel
{
    public class ColorWheel : ListBox
    {
        public ColorWheel()
        {
            // Items template
            FrameworkElementFactory factoryRadialPanel = new FrameworkElementFactory(typeof(RadialPanel));
            ItemsPanel = new ItemsPanelTemplate(factoryRadialPanel);

            // Data template
            DataTemplate template = new DataTemplate(typeof(Brush));
            ItemTemplate = template;

            // rectangle
            FrameworkElementFactory elRectangle = new FrameworkElementFactory(typeof(Rectangle));
            elRectangle.SetValue(Rectangle.WidthProperty, 4.0);
            elRectangle.SetValue(Rectangle.HeightProperty, 12.0);
            elRectangle.SetValue(Rectangle.MarginProperty, new Thickness(1, 8, 1, 8));
            elRectangle.SetBinding(Rectangle.FillProperty, new Binding(""));

            template.VisualTree = elRectangle;

            // fill ListBox
            PropertyInfo[] props = typeof(Brushes).GetProperties();
            foreach (PropertyInfo prop in props)
                Items.Add((Brush)prop.GetValue(null, null));
        }
    }
}
