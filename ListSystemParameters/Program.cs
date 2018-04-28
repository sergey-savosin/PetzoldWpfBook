using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Petzold.ListSystemParameters
{
    class ListSystemParameters : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ListSystemParameters());
        }

        public ListSystemParameters()
        {
            Title = "List system parameters";

            ListView lstvue = new ListView();
            Content = lstvue;

            // Create gridview
            GridView grdvue = new GridView();
            lstvue.View = grdvue;

            // Create two columns
            GridViewColumn col = new GridViewColumn()
            {
                Header = "Property Name",
                Width = 200,
                DisplayMemberBinding = new Binding("Name")
            };
            grdvue.Columns.Add(col);

            // Create data template for 2-nd column
            DataTemplate template = new DataTemplate(typeof(string));
            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Value"));
            template.VisualTree = factoryTextBlock;

            col = new GridViewColumn()
            {
                Header = "Value",
                Width = 200,
                CellTemplate = template
            };
            grdvue.Columns.Add(col);

            // get all system parameters
            PropertyInfo[] props = typeof(SystemParameters).GetProperties();

            SortedList<string, SystemParam> sortlist = new SortedList<string, SystemParam>();
            
            // Fill the ListView
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType != typeof(ResourceKey))
                {
                    SystemParam sysparam = new SystemParam()
                    {
                        Name = prop.Name,
                        Value = prop.GetValue(null, null)
                    };
                    sortlist.Add(prop.Name, sysparam);
                }
            }

            lstvue.ItemsSource = sortlist.Values;
        }
    }
}
