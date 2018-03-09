/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 09.03.2018
 * Time: 15:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.DrawCircles
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class DrawCircles : Window
	{
		Canvas canv;
		// fields for drawing
		bool isDrawing;
		Ellipse elips;
		Point ptCenter;
		
		// fields for dragging
		bool isDragging;
		FrameworkElement elDragging;
		Point ptMouseStart, ptElementStart;
	
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run(new DrawCircles());
		}
		
		public DrawCircles()
		{
			Title = "Draw circles";
			Content = canv = new Canvas();
		}
		
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
		{
			base.OnMouseLeftButtonDown(args);
			
			if (isDragging)
				return;
			
			// create new Ellipse
			ptCenter = args.GetPosition(canv);
			elips = new Ellipse()
			{
				Stroke = SystemColors.WindowTextBrush,
				StrokeThickness = 1,
				Width = 0,
				Height = 0
			};
			canv.Children.Add(elips);
			Canvas.SetLeft(elips, ptCenter.X);
			Canvas.SetTop(elips, ptCenter.Y);
			
			// capture mouse
			CaptureMouse();
			isDrawing = true;
		}
		
		protected override void OnMouseRightButtonDown(MouseButtonEventArgs args)
		{
			base.OnMouseRightButtonDown(args);
			if (isDrawing)
				return;
			
			// find source element
			ptMouseStart = args.GetPosition(canv);
			elDragging = canv.InputHitTest(ptMouseStart) as FrameworkElement;
			if (elDragging != null)
			{
				ptElementStart = new Point(Canvas.GetLeft(elDragging), Canvas.GetTop(elDragging));
				isDragging = true;
			}
		}
		
		protected override void OnMouseDown(MouseButtonEventArgs args)
		{
			base.OnMouseDown(args);
			if (args.ChangedButton == MouseButton.Middle)
			{
				Shape shape = canv.InputHitTest(args.GetPosition(canv))
					as Shape;
				if (shape != null)
					shape.Fill = (shape.Fill == Brushes.Red ? Brushes.Transparent : Brushes.Red);
			}
		}
		
		protected override void OnMouseMove(MouseEventArgs args)
		{
			base.OnMouseMove(args);
			Point ptMouse = args.GetPosition(canv);
			
			// resize ellipse
			if (isDrawing)
			{
				double dRadius = Math.Sqrt(
					Math.Pow(ptCenter.X - ptMouse.X, 2) +
					Math.Pow(ptCenter.Y - ptMouse.Y, 2));
				Canvas.SetLeft(elips, ptCenter.X - dRadius);
				Canvas.SetTop(elips, ptCenter.Y - dRadius);
				elips.Width = 2 * dRadius;
				elips.Height = 2 * dRadius;
			}
			
			// move elips
			else if (isDragging)
			{
				Canvas.SetLeft(elDragging, ptElementStart.X + ptMouse.X - ptMouseStart.X);
				Canvas.SetTop(elDragging, ptElementStart.Y + ptMouse.Y - ptMouseStart.Y);
			}
		}
		
		protected override void OnMouseUp(MouseButtonEventArgs args)
		{
			base.OnMouseUp(args);
			
			// finish drawing
			if (isDrawing && args.ChangedButton == MouseButton.Left)
			{
				elips.Stroke = Brushes.Blue;
				elips.StrokeThickness = Math.Min(24,  elips.Width / 4);
				elips.Fill = Brushes.Red;
				isDrawing = false;
				ReleaseMouseCapture();
			}
			// finish dragging
			else if (isDragging && args.ChangedButton == MouseButton.Right)
			{
				isDragging = false;
			}
		}
		
		protected override void OnTextInput(TextCompositionEventArgs args)
		{
			base.OnTextInput(args);
			
			// Escape press cancels drawing and dragging
			if (args.Text.IndexOf('\x1B') != -1)
			{
				if (isDrawing)
				{
					ReleaseMouseCapture();
					//isDrawing = false;
				}
				else if (isDragging)
				{
					Canvas.SetLeft(elDragging, ptElementStart.X);
					Canvas.SetTop(elDragging, ptElementStart.Y);
					isDragging = false;
				}
			}
		}
		
		protected override void OnLostMouseCapture(MouseEventArgs args)
		{
			base.OnLostMouseCapture(args);
			if (isDrawing)
			{
				canv.Children.Remove(elips);
				isDrawing = false;
			}
		}
	}
}
