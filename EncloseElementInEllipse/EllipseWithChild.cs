using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Petzold.EncloseElementInEllipse
{
    public class EllipseWithChild : Petzold.RenderTheBetterEllipse.BetterEllipse
    {
        UIElement child;

        public UIElement Child
        {
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }

                if ((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }
            get
            {
                return child;
            }
        }

        protected override int VisualChildrenCount
        {
            get { return Child != null ? 1 : 0; }
        }

        protected override Visual GetVisualChild(int index)
        {
            //return base.GetVisualChild(index);
            if (index > 0 || Child == null)
                throw new ArgumentOutOfRangeException("index");
            return Child;
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            //return base.MeasureOverride(availableSize);
            Size sizeDesired = new Size(0, 0);
            if (Stroke != null)
            {
                sizeDesired.Width += 2 * Stroke.Thickness;
                sizeDesired.Height += 2 * Stroke.Thickness;
                sizeAvailable.Width = Math.Max(0, sizeAvailable.Width - 2 * Stroke.Thickness);
                sizeAvailable.Height = Math.Max(0, sizeAvailable.Height - 2 * Stroke.Thickness);
            }

            if (Child != null)
            {
                Child.Measure(sizeAvailable);
                sizeDesired.Width += Child.DesiredSize.Width;
                sizeDesired.Height += Child.DesiredSize.Height;
            }

            return sizeDesired;
        }

        protected override Size ArrangeOverride(Size sizeFinal)
        {
            // return base.ArrangeOverride(finalSize);
            if (Child != null)
            {
                Rect rect = new Rect(
                    new Point(
                        (sizeFinal.Width - Child.DesiredSize.Width) / 2,
                        (sizeFinal.Height - Child.DesiredSize.Height) / 2),
                    Child.DesiredSize);

                Child.Arrange(rect);
            }

            return sizeFinal;
        }
    }
}
