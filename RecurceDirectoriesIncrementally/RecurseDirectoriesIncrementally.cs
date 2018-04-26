using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.RecurceDirectoriesIncrementally
{
    class RecurseDirectoriesIncrementally : Window
    {
        StackPanel stack;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            try
            {
                app.Run(new RecurseDirectoriesIncrementally());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Source: " + ex.Source
                    + "\r\nMessage: " + ex.Message
                    + "\r\nTarget Site: " + ex.TargetSite.Name
                    + "\r\nInner Exception: " + ex.InnerException?.Message
                    + "\r\nStack trace: " + ex.StackTrace,
                    "Recurse directories incrementally");
            }
        }

        public RecurseDirectoriesIncrementally()
        {
            Title = "Recurse directories incrementally";

            Grid grid = new Grid();
            Content = grid;

            // columns
            ColumnDefinition coldef = new ColumnDefinition()
            {
                Width = new GridLength(50, GridUnitType.Star)
            };
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition()
            {
                Width = GridLength.Auto
            };
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition()
            {
                Width = new GridLength(50, GridUnitType.Star)
            };
            grid.ColumnDefinitions.Add(coldef);

            // DirectoryTreeView at left
            DirectoryTreeView tree = new DirectoryTreeView();
            tree.SelectedItemChanged += TreeView_OnSelectedItemChanged;
            grid.Children.Add(tree);
            Grid.SetColumn(tree, 0);

            // GridSplitter object in the middle
            GridSplitter split = new GridSplitter()
            {
                Width = 6,
                ResizeBehavior = GridResizeBehavior.PreviousAndNext
            };
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            // StackPanel with scroll - in the right
            stack = new StackPanel();
            ScrollViewer scroll = new ScrollViewer()
            {
                Content = stack
            };
            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, 2);
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // get selected node
            DirectoryTreeViewItem item = e.NewValue as DirectoryTreeViewItem;

            stack.Children.Clear();

            // filling a stack panel
            FileInfo[] infos;
            try
            {
                infos = item.DirectoryInfo.GetFiles();
            }
            catch
            {
                return;
            }

            foreach(FileInfo info in infos)
            {
                TextBlock text = new TextBlock()
                {
                    Text = info.Name
                };
                stack.Children.Add(text);
            }
        }
    }
}
