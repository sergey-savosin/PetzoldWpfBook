using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Petzold.TemplateTheTree
{
    class TemplateTheTree : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new TemplateTheTree());
        }

        public TemplateTheTree()
        {
            Title = "TemplateTheTree";

            TreeView treevue = new TreeView();
            Content = treevue;

            // create HierarchicalDataTemplate
            HierarchicalDataTemplate template = new HierarchicalDataTemplate(typeof(DiskDirectory));
            template.ItemsSource = new Binding("Subdirectories");

            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Name"));

            template.VisualTree = factoryTextBlock;

            // create DiskDirectory
            DiskDirectory dir = new DiskDirectory(
                new DirectoryInfo(
                    Path.GetPathRoot(Environment.SystemDirectory)));

            // create root for TreeView
            TreeViewItem item = new TreeViewItem()
            {
                Header = dir.Name,
                ItemsSource = dir.Subdirectories,
                ItemTemplate = template
            };

            treevue.Items.Add(item);
            item.IsExpanded = true;
        }
    }
}
