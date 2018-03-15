using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.SelectColor
{
    public class ColorGrid : Control
    {
        // number of rows and cols
        const int yNum = 5;
        const int xNum = 8;

        // output colors
        string[,] strColors = new string[yNum, xNum]
        {
            { "Black", "Brown", "DarkGreen", "MidnightBlue",
                "Navy", "DarkBlue", "Indigo", "DimGray" },
            { "DarkRed", "OrangeRed", "Olive", "Green",
                "Teal", "Blue", "SlateGray", "Gray" },
            { "Red", "Orange", "YellowGreen", "SeaGreen",
                "Aqua", "LightBlue", "Violet", "DarkGray" },
            { "Pink", "Gold", "Yellow", "Lime",
                "Turquoise", "SkyBlue", "Plum", "LightGray" },
            { "LightPink", "Tan", "LightYellow", "LightGreen",
                "LightCyan", "LightSkyBlue", "Lavender", "White" }
        };

        // color cells
        ColorCell[,] cells = new ColorCell[yNum, xNum];
        ColorCell cellSelected;
        ColorCell cellHighlighted;

        // interface elements
        Border bord;
        UniformGrid unigrid;

        // current color
        Color clrSelected = Colors.Black;

        // event "Changed"
        public event EventHandler SelectedColorChanged;

        public ColorGrid()
        {
            // create Border
            bord = new Border()
            {
                BorderBrush = SystemColors.ControlDarkDarkBrush,
                BorderThickness = new Thickness(1)
            };
            AddVisualChild(bord);
            AddLogicalChild(bord);

            // create uniform grid
            unigrid = new UniformGrid()
            {
                Background = SystemColors.WindowBrush,
                Columns = xNum
            };
            bord.Child = unigrid;

            // fill UniformGrid with ColorCell
            for (int y = 0; y < yNum; y++)
                for (int x = 0; x < xNum; x++)
                {
                    Color clr = (Color) typeof(Colors).GetProperty(strColors[y, x]).GetValue(null, null);
                    cells[y, x] = new ColorCell(clr);
                    unigrid.Children.Add(cells[y, x]);
                    if (clr == SelectedColor)
                    {
                        cellSelected = cells[y, x];
                        cells[y, x].IsSelected = true;
                    }
                    ToolTip tip = new ToolTip()
                    {
                        Content = strColors[y, x]
                    };
                    cells[y, x].ToolTip = tip;
                }
        }

        public Color SelectedColor
        {
            get { return clrSelected; }
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");
            return bord;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            bord.Measure(constraint);
            return bord.DesiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            bord.Arrange(new Rect(new Point(0, 0), arrangeBounds));
            return arrangeBounds;
        }

        // mouse event
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ColorCell cell = e.Source as ColorCell;
            if (cell != null)
            {
                if (cellHighlighted != null)
                    cellHighlighted.IsHighlighted = false;
                cellHighlighted = cell;
                cellHighlighted.IsHighlighted = true;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (cellHighlighted != null)
            {
                cellHighlighted.IsSelected = false;
                cellHighlighted = null;
            }
            
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            ColorCell cell = e.Source as ColorCell;
            if (cell != null)
            {
                if (cellHighlighted != null)
                    cellHighlighted.IsSelected = false;
                cellHighlighted = cell;
                cellHighlighted.IsSelected = true;
            }
            Focus();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            ColorCell cell = e.Source as ColorCell;
            if (cell != null)
            {
                if (cellSelected != null)
                    cellSelected.IsSelected = false;
                cellSelected = cell;
                cellSelected.IsSelected = true;

                clrSelected = (cellSelected.Brush as SolidColorBrush).Color;
                OnSelectedColorChanged(EventArgs.Empty);
            }
        }

        // KeyBoard events
        // ToDo

        protected virtual void OnSelectedColorChanged(EventArgs args)
        {
            if (SelectedColorChanged != null)
                SelectedColorChanged(this, args);
        }
    }
}
