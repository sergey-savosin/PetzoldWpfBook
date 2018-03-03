using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SetSpaceProperty
{
    class SetSpaceProperty : SpaceWindow
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new SetSpaceProperty());
        }

        public SetSpaceProperty()
        {
            Title = "Set space property";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            int[] iSpaces = { 0, 1, 2 };

            Grid grid = new Grid();
            Content = grid;

            for (int i = 0; i < 2; i++)
            {
                RowDefinition row = new RowDefinition()
                {
                    Height = GridLength.Auto
                };
                grid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < iSpaces.Length; i++)
            {
                ColumnDefinition col = new ColumnDefinition()
                {
                    Width = GridLength.Auto
                };
                grid.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < iSpaces.Length; i++)
            {
                SpaceButton btn = new SpaceButton()
                {
                    Text = "Set window Space to " + iSpaces[i],
                    Tag = iSpaces[i],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                btn.Click += WindowPropertyOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 0);
                Grid.SetColumn(btn, i);

                // second btn
                btn = new SpaceButton()
                {
                    Text = "Set button Space to " + iSpaces[i],
                    Tag = iSpaces[i],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                btn.Click += ButtonPropertyOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 1);
                Grid.SetColumn(btn, i);
            }
        }
        private void WindowPropertyOnClick(object sender, RoutedEventArgs e)
        {
            SpaceButton btn = e.Source as SpaceButton;
            Space = (int)btn.Tag;
        }

        private void ButtonPropertyOnClick(object sender, RoutedEventArgs e)
        {
            SpaceButton btn = e.Source as SpaceButton;
            btn.Space = (int)btn.Tag;
        }
    }
}