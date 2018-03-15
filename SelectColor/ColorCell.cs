using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Petzold.SelectColor
{
    public class ColorCell : FrameworkElement
    {
        // private fields
        static readonly Size sizeCell = new Size(20, 20);
        DrawingVisual visColor;
        Brush brush;

        // dependency props
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsHighlightedProperty;

        static ColorCell()
        {
            IsSelectedProperty =
                DependencyProperty.Register(
                    "IsSelected",
                    typeof(bool),
                    typeof(ColorCell),
                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
            IsHighlightedProperty =
                DependencyProperty.Register(
                    "IsHighlighted",
                    typeof(bool),
                    typeof(ColorCell),
                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        // properties
        public bool IsSelected
        {
            set { SetValue(IsSelectedProperty, value); }
            get { return (bool)GetValue(IsSelectedProperty); }
        }

        public bool IsHighlighted
        {
            set { SetValue(IsHighlightedProperty, value); }
            get { return (bool)GetValue(IsHighlightedProperty); }
        }

        public Brush Brush
        {
            get { return brush; }
        }

        // ctor
        public ColorCell(Color clr)
        {
            // create new DrawingVisual
            visColor = new DrawingVisual();
            DrawingContext dc = visColor.RenderOpen();

            // draw rect
            Rect rect = new Rect(new Point(0, 0), sizeCell);
            rect.Inflate(-4, -4);
            Pen pen = new Pen(SystemColors.ControlTextBrush, 1);
            brush = new SolidColorBrush(clr);
            dc.DrawRectangle(brush, pen, rect);
            dc.Close();

            // call AddVisual
            AddVisualChild(visColor);
            AddLogicalChild(visColor);
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");
            return visColor;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return sizeCell;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(new Point(0, 0), RenderSize);
            rect.Inflate(-1, -1);
            Pen pen = new Pen(SystemColors.HighlightBrush, 1);
            if (IsHighlighted)
                drawingContext.DrawRectangle(SystemColors.ControlDarkBrush, pen, rect);
            else if (IsSelected)
                drawingContext.DrawRectangle(SystemColors.ControlLightBrush, pen, rect);
            else
                drawingContext.DrawRectangle(Brushes.Transparent, null, rect);
        }
    }
}
