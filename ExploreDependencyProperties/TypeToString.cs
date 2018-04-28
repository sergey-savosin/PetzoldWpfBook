using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Petzold.ExploreDependencyProperties
{
    public class TypeToString : IValueConverter
    {
        public object Convert(object obj, Type type, object param, CultureInfo culture)
        {
            return (obj as Type).Name;
        }

        public object ConvertBack(object obj, Type type, object param, CultureInfo culture)
        {
            return null;
        }
    }
}
