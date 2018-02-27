using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SplitNine
{
    class SplitNine: Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SplitNine());
        }

        public SplitNine()
        {
            Title = "Split nine";
            Grid grid = new Grid();
            Content = grid;

            // rows and cols
            for (int i = 0; i<3; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }

            // create 9 buttons
            for (int x = 0; x<3; x++)
                for (int y = 0; y<3; y++)
                {
                    Button btn = new Button()
                    {
                        Content = "Row " + y + " and Column " + x
                    };
                    grid.Children.Add(btn);
                    Grid.SetRow(btn, y);
                    Grid.SetColumn(btn, x);
                }

            // create splitter
            GridSplitter split = new GridSplitter()
            {
                Width = 6
            };
            grid.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 1);
        }
    }
}
