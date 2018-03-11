using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Petzold.RenderTheBetterEllipse
{
    public class BetterEllipse : FrameworkElement
    {
        // dependent properties
        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty StrokeProperty;

        // public interfaces to dependent properties
        public Brush Fill
        {
            set { SetValue(FillProperty, value); }
            get { return (Brush)GetValue(FillProperty); }
        }

        public Pen Stroke
        {
            set { SetValue(StrokeProperty, value); }
            get { return (Pen)GetValue(StrokeProperty); }
        }

        // static constructor
        static BetterEllipse()
        {
            FillProperty = DependencyProperty.Register(
                "Fill",
                typeof(Brush),
                typeof(BetterEllipse),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender));

            StrokeProperty = DependencyProperty.Register(
                "Stroke",
                typeof(Pen),
                typeof(BetterEllipse),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        // override Measure Override
        protected override Size MeasureOverride(Size availableSize)
        {
            Size sizeDesired = base.MeasureOverride(availableSize);
            if (Stroke != null)
                sizeDesired = new Size(Stroke.Thickness, Stroke.Thickness);

            return sizeDesired;
        }

        // override OnRender
        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);

            Size size = RenderSize;

            // size using Pen thickness
            if (Stroke != null)
            {
                size.Width = Math.Max(0, size.Width - Stroke.Thickness);
                size.Height = Math.Max(0, size.Height - Stroke.Thickness);
            }

            // draw ellipse
            drawingContext.DrawEllipse(
                Fill,
                Stroke,
                new Point(RenderSize.Width / 2, RenderSize.Height / 2),
                size.Width / 2,
                size.Height / 2);

            // show text
            Point pt = new Point(size.Width / 2, size.Height / 2);
            FormattedText fmttext = new FormattedText(
                "Hello, Ellipse! В чём дело?",
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Times New Roman Italic"),
                24,
                Brushes.DarkBlue);
            drawingContext.DrawText(fmttext, pt);
        }
    }
}
