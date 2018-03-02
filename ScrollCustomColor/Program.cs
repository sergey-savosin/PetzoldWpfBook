using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Petzold.ScrollCustomColor
{
    class ScrollCustomColor: Window
    {
        ScrollBar[] scrolls = new ScrollBar[3];
        TextBlock[] txtValue = new TextBlock[3];
        Panel pnlColor;

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new ScrollCustomColor());
        }

        public ScrollCustomColor()
        {
            Title = "Color scroll";
            Width = 500;
            Height = 300;

            // panel gridMain with a splitter
            Grid gridMain = new Grid();
            Content = gridMain;

            // gridMain cols
            ColumnDefinition coldef = new ColumnDefinition()
            {
                Width = new GridLength(200, GridUnitType.Pixel)
            };
            gridMain.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition()
            {
                Width = GridLength.Auto
            };
            gridMain.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition()
            {
                Width = new GridLength(100, GridUnitType.Star)
            };
            gridMain.ColumnDefinitions.Add(coldef);

            // vertical splitter
            GridSplitter split = new GridSplitter()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 6
            };
            gridMain.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);

            // Right panel
            pnlColor = new StackPanel()
            {
                Background = new SolidColorBrush(SystemColors.WindowColor)
            };
            gridMain.Children.Add(pnlColor);
            Grid.SetRow(pnlColor, 0);
            Grid.SetColumn(pnlColor, 2);

            // Left panel
            Grid grid = new Grid();
            gridMain.Children.Add(grid);
            Grid.SetRow(grid, 0);
            Grid.SetColumn(grid, 0);

            // three rows: label, scroll, label
            RowDefinition rowdef = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition()
            {
                Height = new GridLength(100, GridUnitType.Star)
            };
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            grid.RowDefinitions.Add(rowdef);

            // three columns: red, greed, blue
            for (int i = 0; i<3; i++)
            {
                coldef = new ColumnDefinition()
                {
                    Width = new GridLength(33, GridUnitType.Star)
                };
                grid.ColumnDefinitions.Add(coldef);
            }

            for (int i = 0; i<3; i++)
            {
                Label lbl = new Label()
                {
                    Content = new string[] { "Red", "Green", "Blue" }[i],
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, i);

                scrolls[i] = new ScrollBar()
                {
                    Focusable = true,
                    Orientation = Orientation.Vertical,
                    Minimum = 0,
                    Maximum = 255,
                    SmallChange = 1,
                    LargeChange = 16
                };
                scrolls[i].ValueChanged += ScrollOnValueChanged;
                grid.Children.Add(scrolls[i]);
                Grid.SetRow(scrolls[i], 1);
                Grid.SetColumn(scrolls[i], i);

                txtValue[i] = new TextBlock()
                {
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)
                };
                grid.Children.Add(txtValue[i]);
                Grid.SetRow(txtValue[i], 2);
                Grid.SetColumn(txtValue[i], i);
            }

            // Init scrolls
            Color clr = (pnlColor.Background as SolidColorBrush).Color;
            scrolls[0].Value = clr.R;
            scrolls[1].Value = clr.G;
            scrolls[2].Value = clr.B;

            // Set focus
            scrolls[0].Focus();
        }

        private void ScrollOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scroll = sender as ScrollBar;
            Panel pnl = scroll.Parent as Panel;
            TextBlock txt = pnl.Children[
                1 + pnl.Children.IndexOf(scroll)
                ] as TextBlock;

            txt.Text = String.Format("{0}\n0x{0:X2}", (int)scroll.Value);
            pnlColor.Background =
                new SolidColorBrush(
                    Color.FromRgb(
                        (byte)scrolls[0].Value,
                        (byte)scrolls[1].Value,
                        (byte)scrolls[2].Value));
        }
    }
}
