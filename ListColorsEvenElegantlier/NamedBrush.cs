using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Petzold.ListNamedColors
{
    public class NamedBrush
    {
        static NamedBrush[] nbrushes;
        Brush brush;
        string str;

        // Static ctor
        static NamedBrush()
        {
            PropertyInfo[] props = typeof(Brushes).GetProperties();
            nbrushes = new NamedBrush[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                nbrushes[i] = new NamedBrush(
                    props[i].Name,
                    (Brush)props[i].GetValue(null, null));
            }
        }

        // private ctor
        private NamedBrush(string str, Brush brush)
        {
            this.str = str;
            this.brush = brush;
        }

        public static NamedBrush[] All => nbrushes;
        public Brush Brush => brush;
        public string Name
        {
            get
            {
                string strSpaced = str[0].ToString();
                for (int i = 1; i < str.Length; i++)
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") +
                        str[i].ToString();

                return strSpaced;
            }
        }

        public override string ToString()
        {
            return str;
        }

    }
}
