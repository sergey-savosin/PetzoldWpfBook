﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Petzold.SetFontSizeProperty
{
    class SetFontSizeProperty: Window
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SetFontSizeProperty());
        }

        public SetFontSizeProperty()
        {
            Title = "Set font size property";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            FontSize = 16;
            double[] fntsizes = { 8, 16, 32 };

            // Create grid
            Grid grid = new Grid();
            Content = grid;

            // rows and cols
            for (int i = 0; i<2; i++)
            {
                RowDefinition row = new RowDefinition()
                {
                    Height = GridLength.Auto
                };
                grid.RowDefinitions.Add(row);
            }

            for (int i=0; i < fntsizes.Length; i++)
            {
                ColumnDefinition col = new ColumnDefinition()
                {
                    Width = GridLength.Auto
                };
                grid.ColumnDefinitions.Add(col);
            }

            // 6 buttons
            for (int i = 0; i < fntsizes.Length; i++)
            {
                // 1st row
                Button btn = new Button()
                {
                    Content = new TextBlock(
                        new Run("Set window FontSize to " + fntsizes[i])),
                    Tag = fntsizes[i],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(1)
                };
                btn.Click += WindowFontSizeOnClick;

                grid.Children.Add(btn);
                Grid.SetRow(btn, 0);
                Grid.SetColumn(btn, i);

                // 2nd row
                btn = new Button()
                {
                    Content = new TextBlock(
                        new Run("Set button size to " + fntsizes[i])),
                    Tag = fntsizes[i],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(1)
                };
                btn.Click += ButtonFontSizeOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 1);
                Grid.SetColumn(btn, i);
            }
        }

        private void ButtonFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            btn.FontSize = (double)btn.Tag;
        }

        private void WindowFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            FontSize = (double)btn.Tag;
        }
    }

}
