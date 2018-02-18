using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Petzold.ClickTheGradientCenter
{
    class ClickTheGradientCenter : Window
    {
        RadialGradientBrush brush;
        double angle;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ClickTheGradientCenter());
        }

        public ClickTheGradientCenter()
        {
            Title = "Click The Gradient Center";
            Width = 384; // 4 inch
            Height = 384;

            brush = new RadialGradientBrush(Colors.White, Colors.Red);
            brush.RadiusX = brush.RadiusY = 0.10;
            brush.SpreadMethod = GradientSpreadMethod.Repeat;
            Background = brush;

            BorderBrush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
            BorderThickness = new Thickness(25);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            //base.OnMouseDown(e);

            double width = ActualWidth
                - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight
                - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight
                - SystemParameters.CaptionHeight;

            Point ptMouse = e.GetPosition(this);
            ptMouse.X /= width;
            ptMouse.Y /= height;

            if (e.ChangedButton == MouseButton.Left)
            {
                brush.Center = ptMouse;
                brush.GradientOrigin = ptMouse;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                brush.GradientOrigin = ptMouse;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //base.OnMouseMove(e);
            double width = ActualWidth
                - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight
                - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight
                - SystemParameters.CaptionHeight;

            Point ptMouse = e.GetPosition(this);
            ptMouse.X /= width;
            ptMouse.Y /= height;

            brush.GradientOrigin = ptMouse;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            if (e.Key == Key.Space)
            {
                brush.Center = brush.GradientOrigin = new Point(0.5, 0.5);

                DispatcherTimer tmr = new DispatcherTimer();
                tmr.Interval = TimeSpan.FromMilliseconds(100);
                tmr.Tick += TimeOnTick;
                tmr.Start();
            }
        }

        private void TimeOnTick(object sender, EventArgs e)
        {
            Point pt = new Point(0.5 + 0.05 * Math.Cos(angle),
                0.5 + 0.05 * Math.Sin(angle));
            brush.GradientOrigin = pt;
            angle += Math.PI / 6; // 30 degree
        }
    }
}
