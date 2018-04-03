using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColorsElegantly
{
    public class ColorListBox : ListBox
    {
        public ColorListBox()
        {
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                ColorListBoxItem item = new ColorListBoxItem()
                {
                    Text = prop.Name,
                    Color = (Color)prop.GetValue(null, null)
                };
                Items.Add(item);
            }
            SelectedValuePath = "Color";
        }

        public Color SelectedColor
        {
            set { SelectedValue = value; }
            get { return (Color)(SelectedValue != null ? SelectedValue : SystemColors.WindowColor); }
        }
    }
}
