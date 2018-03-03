using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

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
            btn.Click += ScrambleOnClick;

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
            Tile tile = sender as Tile;

            int iMove = unigrid.Children.IndexOf(tile);
            int xMove = iMove % NumberCols;
            int yMove = iMove / NumberCols;

            if (xMove == xEmpty)
                while (yMove != yEmpty)
                    MoveTile(xMove, yEmpty + (yMove - yEmpty) / Math.Abs(yMove - yEmpty));
            if (yMove == yEmpty)
                while (xMove != xEmpty)
                    MoveTile(xEmpty + (xMove - xEmpty) / Math.Abs(xMove - xEmpty), yMove);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.Right: MoveTile(xEmpty - 1, yEmpty); break;
                case Key.Left: MoveTile(xEmpty + 1, yEmpty); break;
                case Key.Down: MoveTile(xEmpty, yEmpty - 1); break;
                case Key.Up: MoveTile(xEmpty, yEmpty + 1); break;
            }
        }

        private void MoveTile(int xTile, int yTile)
        {
            if ((xTile == xEmpty && yTile == yEmpty) ||
                xTile < 0 || xTile >= NumberCols ||
                yTile < 0 || yTile >= NumberRows)
                return;
            int iTile = NumberCols * yTile + xTile;
            int iEmpty = NumberCols * yEmpty + xEmpty;

            UIElement elTile = unigrid.Children[iTile];
            UIElement elEmpty = unigrid.Children[iEmpty];

            unigrid.Children.RemoveAt(iTile);
            unigrid.Children.Insert(iTile, elEmptySpare);
            unigrid.Children.RemoveAt(iEmpty);
            unigrid.Children.Insert(iEmpty, elTile);

            xEmpty = xTile;
            yEmpty = yTile;
            elEmptySpare = elEmpty;
        }

        private void ScrambleOnClick(object sender, RoutedEventArgs e)
        {
            rand = new Random();
            iCounter = 16 * NumberCols * NumberRows;
            DispatcherTimer tmr = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(10)
            };
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            for (int i=0; i<5; i++)
            {
                MoveTile(xEmpty, yEmpty + rand.Next(3) - 1);
                MoveTile(xEmpty + rand.Next(3) - 1, yEmpty);
            }

            if (0 == iCounter --)
            {
                (sender as DispatcherTimer).Stop();
            }
        }
    }
}
