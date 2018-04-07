using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.CircleTheButtons
{
    public class RadialPanel : Panel
    {
        // dependency property
        public static readonly DependencyProperty OrientationProperty;

        // private fields
        bool showPieLines;
        double angleEach;
        Size sizeLargest;
        double radius;
        double outerEdgeFromCenter;
        double innerEdgeFromCenter;

        // static ctor
        static RadialPanel()
        {
            OrientationProperty = DependencyProperty.Register(
                "Orientation",
                typeof(RadialPanelOrientation),
                typeof(RadialPanel),
                new FrameworkPropertyMetadata(
                    RadialPanelOrientation.ByWidth,
                    FrameworkPropertyMetadataOptions.AffectsMeasure
                    )
                );
        }

        // property Orientation
        public RadialPanelOrientation Orientation
        {
            set { SetValue(OrientationProperty, value); }
            get { return (RadialPanelOrientation)GetValue(OrientationProperty); }
        }

        public bool ShowPieLines
        {
            set
            {
                if (value != showPieLines)
                    InvalidateVisual();
                showPieLines = value;
            }

            get
            {
                return showPieLines;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            // return base.MeasureOverride(availableSize);
            if (InternalChildren.Count == 0)
                return new Size(0, 0);

            angleEach = 360.0 / InternalChildren.Count;
            sizeLargest = new Size(0, 0);
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                sizeLargest.Width = Math.Max(sizeLargest.Width, child.DesiredSize.Width);
                sizeLargest.Height = Math.Max(sizeLargest.Width, child.DesiredSize.Height);
            }

            if (Orientation == RadialPanelOrientation.ByWidth)
            {
                // distance from center to edge
                innerEdgeFromCenter = sizeLargest.Width / 2 / Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Height;

                // calc circle's radius
                radius = Math.Sqrt(Math.Pow(sizeLargest.Width / 2, 2) + Math.Pow(outerEdgeFromCenter, 2));
            }
            else
            {
                innerEdgeFromCenter = sizeLargest.Height / 2 / Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Width;

                radius = Math.Sqrt(Math.Pow(sizeLargest.Height / 2, 2) + Math.Pow(outerEdgeFromCenter, 2));
            }

            // return circle's size
            return new Size(2 * radius, 2 * radius);
        }

        protected override Size ArrangeOverride(Size sizeFinal)
        {
            // return base.ArrangeOverride(finalSize);
            double angleChild = 0;
            Point ptCenter = new Point(sizeFinal.Width / 2, sizeFinal.Height / 2);
            double multiplier = Math.Min(sizeFinal.Width / (2 * radius), sizeFinal.Height / (2 * radius));

            foreach(UIElement child in InternalChildren)
            {
                // reset renderTransform
                child.RenderTransform = Transform.Identity;
                if (Orientation == RadialPanelOrientation.ByWidth)
                {
                    child.Arrange(
                        new Rect(
                            ptCenter.X - multiplier * sizeLargest.Width / 2,
                            ptCenter.Y - multiplier * outerEdgeFromCenter,
                            multiplier * sizeLargest.Width,
                            multiplier * sizeLargest.Height
                        ));
                }
                else
                {
                    child.Arrange(
                        new Rect(
                            ptCenter.X + multiplier * innerEdgeFromCenter,
                            ptCenter.Y - multiplier * sizeLargest.Height / 2,
                            multiplier * sizeLargest.Width,
                            multiplier * sizeLargest.Height
                        ));

                }

                // Rotate child object around center
                Point pt = TranslatePoint(ptCenter, child);
                child.RenderTransform = new RotateTransform(angleChild, pt.X, pt.Y);

                // rize angle
                angleChild += angleEach;
            }

            return sizeFinal;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (ShowPieLines)
            {
                Point ptCenter = new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                double multiplier = Math.Min(
                    RenderSize.Width / (2 * radius),
                    RenderSize.Height / (2 * radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1);
                pen.DashStyle = DashStyles.Dash;

                // draw a cirle
                dc.DrawEllipse(null, pen, ptCenter, multiplier * radius, multiplier * radius);

                // init an angle
                double angleChild = -angleEach / 2;
                if (Orientation == RadialPanelOrientation.ByWidth)
                    angleChild += 90;

                // radial lines from center
                foreach (UIElement child in InternalChildren)
                {
                    dc.DrawLine(
                        pen,
                        ptCenter,
                        new Point(
                            ptCenter.X + multiplier * radius * Math.Cos(2 * Math.PI * angleChild / 360),
                            ptCenter.Y + multiplier * radius * Math.Sin(2 * Math.PI * angleChild / 360)
                        )
                    );
                    angleChild += angleEach;
                }
            }
        }
    }
}
