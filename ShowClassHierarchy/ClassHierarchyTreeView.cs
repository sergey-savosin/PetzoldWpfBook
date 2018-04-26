using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ShowClassHierarchy
{
    public class ClassHierarchyTreeView : TreeView
    {
        public ClassHierarchyTreeView(Type typeRoot)
        {
            // for load PresentationCore
            UIElement dummy = new UIElement();

            List<Assembly> assemblies = new List<Assembly>();

            AssemblyName[] anames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            foreach(AssemblyName aname in anames)
            {
                assemblies.Add(Assembly.Load(aname));
            }

            // save all in sorted list
            SortedList<string, Type> classes = new SortedList<string, Type>();
            classes.Add(typeRoot.Name, typeRoot);

            // get all type in assembly
            foreach (Assembly assembly in assemblies)
                foreach (Type typ in assembly.GetTypes())
                    if (typ.IsPublic && typ.IsSubclassOf(typeRoot))
                        classes.Add(typ.Name, typ);

            // create root node
            TypeTreeViewItem item = new TypeTreeViewItem(typeRoot);
            Items.Add(item);

            // recurse fill items
            CreateLinkedItems(item, classes);
        }

        private void CreateLinkedItems(TypeTreeViewItem itemBase, SortedList<string, Type> list)
        {
            foreach(KeyValuePair<string, Type> kvp in list)
            {
                if (kvp.Value.BaseType == itemBase.Type)
                {
                    TypeTreeViewItem item = new TypeTreeViewItem(kvp.Value);
                    itemBase.Items.Add(item);
                    CreateLinkedItems(item, list);
                }
            }
        }
    }
}
