using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CalculateInHex
{
    public class RoundedButton : Control
    {
        RoundedButtonDecorator decorator;

        public static readonly RoutedEvent ClickEvent;

        static RoundedButton()
        {
            ClickEvent = EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(RoundedButton));
        }

        public RoundedButton()
        {
            decorator = new RoundedButtonDecorator();
            AddVisualChild(decorator);
            AddLogicalChild(decorator);
        }

        public UIElement Child
        {
            set { decorator.Child = value; }
            get { return decorator.Child; }
        }

        public bool IsPressed
        {
            set { decorator.IsPressed = value; }
            get { return decorator.IsPressed; }
        }

        // public event
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        public bool IsMouseReallyOver
        {
            get
            {
                Point pt = Mouse.GetPosition(this);
                return (pt.X >= 0 && pt.X < ActualWidth &&
                    pt.Y >= 0 && pt.Y < ActualHeight);
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");
            return decorator;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            // return base.MeasureOverride(constraint);
            decorator.Measure(constraint);
            return decorator.DesiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            // return base.ArrangeOverride(arrangeBounds);
            decorator.Arrange(new Rect(new Point(0, 0), arrangeBounds));
            return arrangeBounds;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsMouseCaptured)
                IsPressed = IsMouseReallyOver;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            IsPressed = true;
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (IsMouseCaptured)
            {
                if (IsMouseReallyOver)
                    OnClick();
                Mouse.Capture(null);
                IsPressed = false;
                e.Handled = true;
            }
        }

        private void OnClick()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs()
            {
                RoutedEvent = RoundedButton.ClickEvent,
                Source = this
            };
            RaiseEvent(argsEvent);
        }
    }
}
