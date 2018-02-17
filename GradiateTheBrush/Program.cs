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
			Title = "Gradiate The Brush";
			SizeChanged += WindowOnSizeChanged;
			
			brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 0);
			brush.MappingMode = BrushMappingMode.Absolute;
			Background = brush;
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
			brush.StartPoint = ptCenter + vectPerp;
			brush.EndPoint = ptCenter - vectPerp;
			Title = string.Format("start: {0}, end: {1}, width: {2}, height: {3}",
			                      brush.StartPoint.ToString(),
			                      brush.EndPoint.ToString(),
			                     width,
			                    height);
		}
		
	}
}
