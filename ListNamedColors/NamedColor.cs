using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Petzold.ListNamedColors
{
    class NamedColor
    {
        static NamedColor[] nclrs;
        Color clr;
        string str;

        // Static ctor
        static NamedColor()
        {
            PropertyInfo[] props = typeof(Colors).GetProperties();
            nclrs = new NamedColor[props.Length];
            for (int i= 0; i<props.Length; i++)
            {
                nclrs[i] = new NamedColor(
                    props[i].Name,
                    (Color)props[i].GetValue(null, null));
            }
        }

        // private ctor
        private NamedColor(string str, Color clr)
        {
            this.str = str;
            this.clr = clr;
        }

        public static NamedColor[] All => nclrs;
        public Color Color => clr;
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
