using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ExploreDependencyProperties
{
    class ExploreDependencyProperties : Window
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ExploreDependencyProperties());
        }

        public ExploreDependencyProperties()
        {
            Title = "Explore Dependency Properties";
            Grid grid = new Grid();
            Content = grid;

            ColumnDefinition coldef = new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition()
            {
                Width = GridLength.Auto
            };
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            grid.ColumnDefinitions.Add(coldef);

            // left panel
            ClassHierarchyTreeView treevue = new ClassHierarchyTreeView(typeof(DependencyObject));
            grid.Children.Add(treevue);
            Grid.SetColumn(treevue, 0);

            // center panel
            GridSplitter split = new GridSplitter()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 6,
                ShowsPreview = true
            };
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            // right panel
            DependencyPropertyListView lstvue = new DependencyPropertyListView();
            grid.Children.Add(lstvue);
            Grid.SetColumn(lstvue, 2);

            // set binding
            lstvue.SetBinding(DependencyPropertyListView.TypeProperty, "SelectedItem.Type");
            lstvue.DataContext = treevue;
        }
    }
}
