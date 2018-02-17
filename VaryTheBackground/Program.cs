using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.VaryTheBackground
{
	class VaryTheBackground : Window
	{
		SolidColorBrush brush = new SolidColorBrush(Colors.Black);
		int index = 0;
		PropertyInfo[] props;
		
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run(new VaryTheBackground());
		}
		
		public VaryTheBackground()
		{
			Title = "Vary The Background";
			Width = 384;
			Height = 384;
			Background = brush;
			
			props = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
			SetTitleAndBackground();
		}

		void SetTitleAndBackground()
		{
			Title = "Flip throw the brushes - " + props[index].Name;
			Background = (Brush)props[index].GetValue(null, null);
		}
		
		protected override void OnMouseMove(MouseEventArgs args)
		{
			double width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
			double height = ActualHeight 
				- 2 * SystemParameters.ResizeFrameHorizontalBorderHeight
				- SystemParameters.CaptionHeight;
			
			Point ptMouse = args.GetPosition(this);
			Point ptCenter = new Point(width / 2, height / 2);
			Vector vectMouse = ptMouse - ptCenter;
			double angle = Math.Atan2(vectMouse.Y, vectMouse.X);
			Vector vectEllipse = new Vector(width/2 * Math.Cos(angle),
			                                height/2 * Math.Sin(angle));
			Byte byLevel = (byte)(255 * (1-Math.Min(1, vectMouse.Length / vectEllipse.Length)));
			
			Color clr = brush.Color;
			clr.R = clr.G = clr.B = byLevel;
			brush.Color = clr;
			Background = brush;
		}
		
		protected override void OnKeyDown(KeyEventArgs args)
		{
			base.OnKeyDown(args);
			
			if (args.Key == Key.Up || args.Key == Key.Down)
			{
				index += args.Key == Key.Up ? 1 : props.Length - 1;
				index %= props.Length;
				SetTitleAndBackground();
			}

		}
		
		protected override void OnTextInput(TextCompositionEventArgs e)
		{
			base.OnTextInput(e);
			
			if (e.Text == "\b" && Title.Length > 0)
			{
				Title = Title.Substring(0, Title.Length - 1);
			}
			else if (e.Text.Length > 0 && !Char.IsControl(e.Text[0]))
			{
				Title += e.Text;
			}
		}
	}
}