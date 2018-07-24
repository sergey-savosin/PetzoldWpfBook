//--------------------------------------------
// RadialPanel.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CircleTheButtons
{
    public class RadialPanel : Panel
    {
        // Dependency Property.
        public static readonly DependencyProperty OrientationProperty;

        // Private fields.
        bool showPieLines;
        double angleEach;       // angle for each child
        Size sizeLargest;       // size of largest child
        double radius;          // radius of circle
        double outerEdgeFromCenter;
        double innerEdgeFromCenter;
        
        // Static constructor to create Orientation dependency property.
        static RadialPanel()
        {
            OrientationProperty =
                DependencyProperty.Register("Orientation", 
                    typeof(RadialPanelOrientation), typeof(RadialPanel),
                    new FrameworkPropertyMetadata(RadialPanelOrientation.ByWidth,
                            FrameworkPropertyMetadataOptions.AffectsMeasure));
        }
        // Orientation property.
        public RadialPanelOrientation Orientation
        {
            set { SetValue(OrientationProperty, value); }
            get { return (RadialPanelOrientation)GetValue(OrientationProperty); }
        }
        // ShowPieLines property.
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
        // Override of MeasureOverride.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            if (InternalChildren.Count == 0)
                return new Size(0, 0);

            angleEach = 360.0 / InternalChildren.Count;
            sizeLargest = new Size(0, 0);

            foreach (UIElement child in InternalChildren)
            {
                // Call Measure for each child ...
                child.Measure(new Size(Double.PositiveInfinity, 
                                       Double.PositiveInfinity));

                // ... and then examine DesiredSize property of child.
                sizeLargest.Width = Math.Max(sizeLargest.Width, 
                                             child.DesiredSize.Width);

                sizeLargest.Height = Math.Max(sizeLargest.Height, 
                                              child.DesiredSize.Height);
            }
            if (Orientation == RadialPanelOrientation.ByWidth)
            {
                // Calculate the distance from the center to element edges.
                innerEdgeFromCenter = sizeLargest.Width / 2 /
                                        Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Height; 

                // Calculate the radius of the circle based on the largest child.
                radius = Math.Sqrt(Math.Pow(sizeLargest.Width / 2, 2) +
                                   Math.Pow(outerEdgeFromCenter, 2));
            }
            else
            {
                // Calculate the distance from the center to element edges.
                innerEdgeFromCenter = sizeLargest.Height / 2 /
                                        Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Width;

                // Calculate the radius of the circle based on the largest child.
                radius = Math.Sqrt(Math.Pow(sizeLargest.Height / 2, 2) +
                                   Math.Pow(outerEdgeFromCenter, 2));
            }
            // Return the size of that circle.
            return new Size(2 * radius, 2 * radius);
        }
        // Override of ArrangeOverride.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            double angleChild = 0;
            Point ptCenter = new Point(sizeFinal.Width / 2, sizeFinal.Height / 2);
            double multiplier = Math.Min(sizeFinal.Width / (2 * radius),
                                         sizeFinal.Height / (2 * radius));

            foreach (UIElement child in InternalChildren)
            {
                // Reset RenderTransform.
                child.RenderTransform = Transform.Identity;

                if (Orientation == RadialPanelOrientation.ByWidth)
                {
                    // Position the child at the top.
                    child.Arrange(
                        new Rect(ptCenter.X - multiplier * sizeLargest.Width / 2,
                                 ptCenter.Y - multiplier * outerEdgeFromCenter,
                                 multiplier * sizeLargest.Width,
                                 multiplier * sizeLargest.Height));
                }
                else
                {
                    // Position the child at the right.
                    child.Arrange(
                        new Rect(ptCenter.X + multiplier * innerEdgeFromCenter,
                                 ptCenter.Y - multiplier * sizeLargest.Height / 2,
                                 multiplier * sizeLargest.Width,
                                 multiplier * sizeLargest.Height));
                }
                // Rotate the child around the center (relative to the child).
                Point pt = TranslatePoint(ptCenter, child);
                child.RenderTransform = 
                                new RotateTransform(angleChild, pt.X, pt.Y);

                // Increment the angle.
                angleChild += angleEach;
            }
            return sizeFinal;
        }
        // Override OnRender to display optional pie lines.
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (ShowPieLines)
            {
                Point ptCenter =
                    new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                double multiplier = Math.Min(RenderSize.Width / (2 * radius),
                                             RenderSize.Height / (2 * radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1);
                pen.DashStyle = DashStyles.Dash;

                // Display circle.
                dc.DrawEllipse(null, pen, ptCenter, multiplier * radius, 
                                                    multiplier * radius);
                // Initialize angle.
                double angleChild = -angleEach / 2;

                if (Orientation == RadialPanelOrientation.ByWidth)
                    angleChild += 90;

                // Loop through each child to draw radial lines from center.
                foreach (UIElement child in InternalChildren)
                {
                    dc.DrawLine(pen, ptCenter, 
                        new Point(ptCenter.X + multiplier * radius * 
                                    Math.Cos(2 * Math.PI * angleChild / 360),
                                  ptCenter.Y + multiplier * radius * 
                                    Math.Sin(2 * Math.PI * angleChild / 360)));
                    angleChild += angleEach;
                }
            }
        }
    }
}
