/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 09.03.2018
 * Time: 21:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.ShadowTheStylus
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class ShadowTheStylus : Window
	{
		static readonly SolidColorBrush brushStylus = Brushes.Blue;
		static readonly SolidColorBrush brushShadow = Brushes.LightBlue;
		static readonly double widthStroke = 96 / 2.54; //1 cm
		static readonly Vector vectShadow = new Vector(widthStroke / 4, widthStroke / 4);
		
		// additional fields for move operations
		Canvas canv;
		Polyline polyStylus, polyShadow;
		bool isDrawing;
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run(new ShadowTheStylus());
		}
		
		public ShadowTheStylus()
		{
			Title = "Shadow the stylus";
			
			// Create panel Canvas
			canv = new Canvas();
			Content = canv;
		}
		
		protected override void OnStylusDown(StylusDownEventArgs args)
		{
			base.OnStylusDown(args);
			Point ptStylus = args.GetPosition(canv);
			
			// create base object
			polyStylus = new Polyline()
			{
				Stroke = brushStylus,
				StrokeThickness = widthStroke,
				StrokeStartLineCap = PenLineCap.Round,
				StrokeEndLineCap = PenLineCap.Round,
				StrokeLineJoin = PenLineJoin.Round,
				Points = new PointCollection()
			};
			polyStylus.Points.Add(ptStylus);
			
			// create shadow object
			polyShadow = new Polyline()
			{
				Stroke = brushShadow,
				StrokeThickness = widthStroke,
				StrokeStartLineCap = PenLineCap.Round,
				StrokeEndLineCap = PenLineCap.Round,
				StrokeLineJoin = PenLineJoin.Round,
				Points = new PointCollection()
			};
			polyShadow.Points.Add(ptStylus + vectShadow);
			
			// insert shadow before the front polyline
			canv.Children.Insert(canv.Children.Count / 2, polyShadow);
			
			// front polyline
			canv.Children.Add(polyStylus);
			
			CaptureStylus();
			isDrawing = true;
			args.Handled = true;
		}
		
		protected override void OnStylusMove(StylusEventArgs args)
		{
			base.OnStylusMove(args);
			if (isDrawing)
			{
				Point ptStylus = args.GetPosition(canv);
				polyStylus.Points.Add(ptStylus);
				polyShadow.Points.Add(ptStylus + vectShadow);
				args.Handled = true;
			}
		}
		
		protected override void OnStylusUp(StylusEventArgs args)
		{
			base.OnStylusUp(args);
			if (isDrawing)
			{
				isDrawing = false;
				ReleaseStylusCapture();
				args.Handled = true;
			}
		}
		
		protected override void OnTextInput(TextCompositionEventArgs args)
		{
			base.OnTextInput(args);
			
			// Escape finishes drawing
			if (isDrawing && args.Text.IndexOf('\x1B') != -1)
			{
				ReleaseStylusCapture();
				args.Handled = true;
			}
		}
		
		protected override void OnLostStylusCapture(StylusEventArgs args)
		{
			base.OnLostStylusCapture(args);
			if (isDrawing)
			{
				canv.Children.Remove(polyStylus);
				canv.Children.Remove(polyShadow);
				isDrawing = false;
			}
		}
		
	}
}
