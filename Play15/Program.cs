using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Petzold.Play15
{
    class Play15 : Window
    {
        const int NumberRows = 4;
        const int NumberCols = 4;

        UniformGrid unigrid;
        int xEmpty, yEmpty, iCounter;
        Key[] keys = { Key.Left, Key.Right, Key.Up, Key.Down };
        Random rand;
        UIElement elEmptySpare = new Empty();

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new Play15());
        }

        public Play15()
        {
            Title = "Play 15";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            Background = SystemColors.ControlBrush;

            // create StackPanel
            StackPanel stack = new StackPanel();
            Content = stack;

            // top button
            Button btn = new Button()
            {
                Content = "_Scramble",
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            btn.Click += ScramleOnClick;

            stack.Children.Add(btn);

            // create Border
            Border bord = new Border()
            {
                BorderBrush = SystemColors.ControlDarkDarkBrush,
                BorderThickness = new Thickness(1)
            };
            stack.Children.Add(bord);

            // create uniform grid
            unigrid = new UniformGrid()
            {
                Rows = NumberRows,
                Columns = NumberCols
            };
            bord.Child = unigrid;

            // Create Tile for all cells, but one
            for (int i = 0; i < NumberCols * NumberRows - 1; i++)
            {
                Tile tile = new Tile()
                {
                    Text = (i + 1).ToString()
                };
                tile.MouseLeftButtonDown += TileOnMouseLeftButtonDown;
                unigrid.Children.Add(tile);
            }

            // Create empty object for last cell
            unigrid.Children.Add(new Empty());
            xEmpty = NumberCols - 1;
            yEmpty = NumberRows - 1;

        }

        private void TileOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ScramleOnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
