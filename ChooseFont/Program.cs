/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 03.05.2018
 * Time: 9:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ChooseFont
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class ChooseFont : Window
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application app = new Application();
			app.Run(new ChooseFont());
		}
		
		public ChooseFont()
		{
			Title = "Choose font";
			
			Button btn = new Button()
			{
				Content = Title,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			btn.Click += ButtonOnClick;
			Content = btn;
		}
		
		void ButtonOnClick(object sender, RoutedEventArgs e)
		{
			FontDialog dlg = new FontDialog()
			{
				Owner = this,
				Typeface = new Typeface(
					FontFamily,
					FontStyle,
					FontWeight,
					FontStretch),
				FaceSize = FontSize
			};
			
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				// set Window properties
				FontFamily = dlg.FontFamily;
				FontStyle = dlg.FontStyle;
				FontWeight = dlg.FontWeight;
				FontStretch = dlg.FontStretch;
				FontSize = dlg.FaceSize;
			}
		}
	}
}
