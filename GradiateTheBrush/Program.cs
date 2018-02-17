/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 16.02.2018
 * Time: 8:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.GradiateTheBrush
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class GradiateTheBrush : Window
	{
		LinearGradientBrush brush;
		
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run(new GradiateTheBrush());
		}
		
		public GradiateTheBrush()
		{
			Title = "Gradiate The Brush. Press Space or Enter key.";
			//SizeChanged += WindowOnSizeChanged;
			
			brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 0);
			brush.MappingMode = BrushMappingMode.Absolute;
			Background = brush;
		}
		
		void MakeTheCircleRainbow()
		{
			RadialGradientBrush brush2 = new RadialGradientBrush();
			Background = brush2;
			
			brush2.GradientStops.Add(new GradientStop(Colors.Red, 0));
			brush2.GradientStops.Add(new GradientStop(Colors.Orange, 0.17));
			brush2.GradientStops.Add(new GradientStop(Colors.Yellow, 0.33));
			brush2.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
			brush2.GradientStops.Add(new GradientStop(Colors.Blue, 0.67));
			brush2.GradientStops.Add(new GradientStop(Colors.Indigo, 0.84));
			brush2.GradientStops.Add(new GradientStop(Colors.Violet, 1));
		}
		
		void MakeTheLinearRainbow()
		{
			LinearGradientBrush brush2 = new LinearGradientBrush();
			brush2.StartPoint = new Point(0, 0);
			brush2.EndPoint = new Point(1, 0);
			Background = brush2;
			
			brush2.GradientStops.Add(new GradientStop(Colors.Red, 0));
			brush2.GradientStops.Add(new GradientStop(Colors.Orange, 0.17));
			brush2.GradientStops.Add(new GradientStop(Colors.Yellow, 0.33));
			brush2.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
			brush2.GradientStops.Add(new GradientStop(Colors.Blue, 0.67));
			brush2.GradientStops.Add(new GradientStop(Colors.Indigo, 0.84));
			brush2.GradientStops.Add(new GradientStop(Colors.Violet, 1));
		}

		protected override void OnKeyDown(KeyEventArgs args)
		{
			if (args.Key == Key.Space)
			{
				MakeTheLinearRainbow();
			}
			else if (args.Key == Key.Enter)
			{
				MakeTheCircleRainbow();
			}
		}
		
		void WindowOnSizeChanged(object sender, SizeChangedEventArgs args)
		{
			double width = ActualWidth
				- 2*SystemParameters.ResizeFrameVerticalBorderWidth;
			double height = ActualHeight
				- 2*SystemParameters.ResizeFrameHorizontalBorderHeight
				- SystemParameters.CaptionHeight;
			
			Point ptCenter = new Point(width /2, height /2);
			Vector vectDiag = new Vector(width, -height);
			Vector vectPerp = new Vector(vectDiag.Y, -vectDiag.X);
			
			vectPerp.Normalize();
			vectPerp *= width * height / vectDiag.Length;
			Background = brush;
			brush.StartPoint = ptCenter + vectPerp;
			brush.EndPoint = ptCenter - vectPerp;
		}
		
	}
}
