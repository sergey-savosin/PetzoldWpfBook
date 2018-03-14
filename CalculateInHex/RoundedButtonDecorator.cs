using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.CalculateInHex
{
    public class RoundedButtonDecorator : Decorator
    {
        public static readonly DependencyProperty IsPressedProperty;

        static RoundedButtonDecorator()
        {
            IsPressedProperty = DependencyProperty.Register(
                "IsPressed",
                typeof(bool),
                typeof(RoundedButtonDecorator),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public bool IsPressed
        {
            set { SetValue(IsPressedProperty, value); }
            get { return (bool)GetValue(IsPressedProperty); }
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            // return base.MeasureOverride(constraint);
            Size szDesired = new Size(2, 2);
            sizeAvailable.Width -= 2;
            sizeAvailable.Height -= 2;
            if (Child != null)
            {
                Child.Measure(sizeAvailable);
                szDesired.Width += Child.DesiredSize.Width;
                szDesired.Height += Child.DesiredSize.Height;
            }
            return szDesired;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            // return base.ArrangeOverride(arrangeSize);
            if (Child != null)
            {
                Point ptChild = new Point(
                    Math.Max(1, (arrangeSize.Width - Child.DesiredSize.Width) / 2),
                    Math.Max(1, (arrangeSize.Height - Child.DesiredSize.Height) / 2));
                Child.Arrange(new Rect(ptChild, Child.DesiredSize));
            }

            return arrangeSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            // base.OnRender(drawingContext);
            RadialGradientBrush brush = new RadialGradientBrush(
                IsPressed ? SystemColors.ControlDarkColor : SystemColors.ControlLightColor,
                SystemColors.ControlColor);

            brush.GradientOrigin = IsPressed ? new Point(0.75, 0.25) :
                new Point(0.25, 0.75);

            drawingContext.DrawRoundedRectangle(
                brush,
                new Pen(SystemColors.ControlDarkDarkBrush, 1),
                new Rect(new Point(0, 0), RenderSize),
                RenderSize.Height / 2,
                RenderSize.Height / 2);
        }
    }
}
