using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Petzold.ExploreDependencyProperties
{
    public class DependencyPropertyListView : ListView
    {
        // dependency property "Type"
        public static DependencyProperty TypeProperty;

        // register d.p.
        static DependencyPropertyListView()
        {
            TypeProperty = DependencyProperty.Register(
                "Type",
                typeof(Type),
                typeof(DependencyPropertyListView),
                new PropertyMetadata(null,
                    new PropertyChangedCallback(OnTypePropertyChanged)));
        }

        static void OnTypePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DependencyPropertyListView lstvue = obj as DependencyPropertyListView;
            Type type = args.NewValue as Type;

            // clear ListView
            lstvue.ItemsSource = null;

            // get all DependencyProperty fields of type
            if (type != null)
            {
                SortedList<string, DependencyProperty> list = new SortedList<string, DependencyProperty>();
                FieldInfo[] infos = type.GetFields();
                foreach (FieldInfo info in infos)
                {
                    if (info.FieldType == typeof(DependencyProperty))
                    {
                        list.Add(
                            info.Name,
                            (DependencyProperty)info.GetValue(null));
                    }
                }
                lstvue.ItemsSource = list.Values;
            }
        }

        // public property
        public Type Type
        {
            set { SetValue(TypeProperty, value); }
            get { return (Type)GetValue(TypeProperty); }
        }

        // ctor
        public DependencyPropertyListView()
        {
            GridView grdvue = new GridView();
            this.View = grdvue;

            // 1st col
            GridViewColumn col = new GridViewColumn()
            {
                Header = "Name",
                Width = 150,
                DisplayMemberBinding = new Binding("Name")
            };
            grdvue.Columns.Add(col);

            // 2nd col "OwnerType"
            Binding bind = new Binding("OwnerType")
            {
                Converter = new TypeToString()
            };

            FrameworkElementFactory elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);

            DataTemplate template = new DataTemplate()
            {
                VisualTree = elTextBlock
            };

            col = new GridViewColumn()
            {
                Header = "Owner",
                Width = 100,
                CellTemplate = template
            };
            grdvue.Columns.Add(col);

            // 3-d col "PropertyType"
            bind = new Binding("PropertyType")
            {
                Converter = new TypeToString()
            };
            elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);

            template = new DataTemplate()
            {
                VisualTree = elTextBlock
            };

            col = new GridViewColumn()
            {
                Header = "Type",
                Width = 100,
                CellTemplate = template
            };
            grdvue.Columns.Add(col);

            // 4d col = "Default"
            bind = new Binding("DefaultMetadata.DefaultValue");
            col = new GridViewColumn()
            {
                Header = "Default",
                Width = 75,
                DisplayMemberBinding = bind
            };
            grdvue.Columns.Add(col);

            // 5d col "Read-Only"
            bind = new Binding("DefaultMetadata.ReadOnly");
            col = new GridViewColumn()
            {
                Header = "Read-Only",
                Width = 75,
                DisplayMemberBinding = bind
            };
            grdvue.Columns.Add(col);

            // 6 col "Usage"
            bind = new Binding("DefaultMetadata.AttachedPropertyUsage");
            col = new GridViewColumn()
            {
                Header = "Usage",
                Width = 75,
                DisplayMemberBinding = bind
            };
            grdvue.Columns.Add(col);

            // 7 col - metadata flags
            bind = new Binding("DefaultMetadata")
            {
                Converter = new MetadataToFlags()
            };

            elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);

            template = new DataTemplate()
            {
                VisualTree = elTextBlock
            };

            col = new GridViewColumn()
            {
                Header = "Flags",
                Width = 250,
                CellTemplate = template
            };
            grdvue.Columns.Add(col);
        }
    }
}
