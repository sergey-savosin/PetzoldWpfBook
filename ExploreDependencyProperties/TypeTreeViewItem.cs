using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Petzold.ExploreDependencyProperties
{
    public class TypeTreeViewItem : TreeViewItem
    {
        Type typ;

        //public TypeTreeViewItem()
        //{
        //}

        public TypeTreeViewItem(Type typ)
        {
            Type = typ;
        }

        public Type Type
        {
            set
            {
                typ = value;
                if (typ.IsAbstract)
                    Header = typ.Name + " (abstract)";
                else
                    Header = typ.Name;
            }
            get
            {
                return typ;
            }
        }
    }
}
