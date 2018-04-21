using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.FormatRichText
{
    public class ColorGridBox : ListBox
    {
        string[] strColors =
        {
            "Black", "Brown", "DarkGreen", "MidnightBlue",
            "Navy", "DarkBlue", "Indigo", "DimGray",
            "DarkRed", "OrangeRed", "Olive", "Green",
            "Teal", "Blue", "SlateGray", "Gray",
            "Red", "Orange", "YellowGreen", "SeaGreen",
            "Aqua", "LightBlue", "Violet", "DarkGray",
            "Pink", "Gold", "Yellow", "Lime",
            "Turquoise", "SkyBlue", "Plum", "LightGray",
            "LightPink", "Tan", "LightYellow", "LightGreen",
            "LightCyan", "LightSkyBlue", "Lavender", "White"
        };

        public ColorGridBox()
        {
            // ItemsPanel template
            FrameworkElementFactory factoryUnigrid = new FrameworkElementFactory(typeof(UniformGrid));
            factoryUnigrid.SetValue(UniformGrid.ColumnsProperty, 8);
            ItemsPanel = new ItemsPanelTemplate(factoryUnigrid);

            // fill the list
            foreach(string strColor in strColors)
            {
                // rect
                Rectangle rect = new Rectangle()
                {
                    Width = 12,
                    Height = 12,
                    Margin = new Thickness(4),
                    Fill = (Brush)typeof(Brushes).GetProperty(strColor).GetValue(null, null)
                };
                Items.Add(rect);

                // tooltip
                ToolTip tip = new ToolTip()
                {
                    Content = strColor
                };
                rect.ToolTip = tip;
            }

            SelectedValuePath = "Fill";
        }
    }
}
