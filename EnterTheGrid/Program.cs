using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.EnterTheGrid
{
    class EnterTheGrid: Window
    {

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new EnterTheGrid());
        }

        public EnterTheGrid()
        {
            Title = "Enter the grid";
            MinWidth = 300;
            SizeToContent = SizeToContent.WidthAndHeight;

            // create StackPanel
            StackPanel stack = new StackPanel();
            Content = stack;

            // create grid
            Grid grid1 = new Grid();
            grid1.Margin = new Thickness(5);
            stack.Children.Add(grid1);

            // grid rows
            for (int i = 0; i < 5; i++)
            {
                RowDefinition rowdef = new RowDefinition()
                {
                    Height = GridLength.Auto
                };
                grid1.RowDefinitions.Add(rowdef);
            }

            // grid cols
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid1.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            grid1.ColumnDefinitions.Add(coldef);

            // create labels
            string[] strLabels = { "_First name:",
                "_Last name:",
                "_Social security number:",
                "_Credit card number:",
                "_Other personal stuff:" };
            for (int i = 0; i < strLabels.Length; i++)
            {
                Label lbl = new Label()
                {
                    Content = strLabels[i],
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                grid1.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);

                TextBox txtbox = new TextBox()
                {
                    Margin = new Thickness(5)
                };
                grid1.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
            }

            // Second grid for buttons
            Grid grid2 = new Grid()
            {
                Margin = new Thickness(10)
            };
            stack.Children.Add(grid2);

            // for 1 row - can be without RowDefinition
            // star - by default
            grid2.ColumnDefinitions.Add(new ColumnDefinition());
            grid2.ColumnDefinitions.Add(new ColumnDefinition());

            // create buttons
            Button btn = new Button()
            {
                Content = "Submit",
                HorizontalAlignment = HorizontalAlignment.Center,
                IsDefault = true,
            };
            btn.Click += delegate { Close(); };
            grid2.Children.Add(btn); // row = col = 0

            btn = new Button()
            {
                Content = "Cancel",
                HorizontalAlignment = HorizontalAlignment.Center,
                IsCancel = true
            };
            btn.Click += delegate { Close(); };
            grid2.Children.Add(btn);
            Grid.SetColumn(btn, 1); //row 0

            // set focus to first text field
            (stack.Children[0] as Panel).Children[1].Focus();

        }
    }
}
