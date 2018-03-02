using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.Play15
{
    public class Tile : Canvas
    {
        const int SIZE = 64; //2.3''
        const int BORD = 6; // 1/16''
        TextBlock txtblk;

        public Tile()
        {
            Width = SIZE;
            Height = SIZE;

            // left and top border
            Polygon poly = new Polygon()
            {
                Points = new PointCollection(
                    new Point[]
                    {
                        new Point(0, 0),
                        new Point(SIZE, 0),
                        new Point(SIZE-BORD, BORD),
                        new Point(BORD, BORD),
                        new Point(BORD, SIZE-BORD),
                        new Point(0, SIZE)
                    }),
                Fill = SystemColors.ControlLightLightBrush
            };
            Children.Add(poly);

            // text field
            Border bord = new Border()
            {
                Width = SIZE - 2 * BORD,
                Height = SIZE - 2 * BORD,
                Background = SystemColors.ControlBrush
            };
            Children.Add(bord);
            SetLeft(bord, BORD);
            SetTop(bord, BORD);

            // output text
            txtblk = new TextBlock()
            {
                FontSize = 32,
                Foreground = SystemColors.ControlTextBrush,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            bord.Child = txtblk;

        }

        public string Text
        {
            set { txtblk.Text = value; }
            get { return txtblk.Text; }
        }
    }
}
