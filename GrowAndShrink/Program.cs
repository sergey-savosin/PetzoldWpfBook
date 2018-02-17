using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.InheritTheApp
{
	class GrowAndShrink : Window
	{
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run(new GrowAndShrink());
		}
		
		public GrowAndShrink()
		{
			Title = "Grow and shrink";
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			Width = 192;
			Height = 192;
		}
		
		protected override void OnKeyDown(KeyEventArgs args)
		{
			base.OnKeyDown(args);
			
			if (args.Key == Key.Up)
			{
				Width *= 1.1;
				Height *= 1.1;
				Left -= 0.05 * Width;
				Top -= 0.05 * Height;
			}
			else if (args.Key == Key.Down)
			{
				Width /= 1.1;
				Height /= 1.1;
				Left += 0.05 * Width;
				Top += 0.05 * Height;
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