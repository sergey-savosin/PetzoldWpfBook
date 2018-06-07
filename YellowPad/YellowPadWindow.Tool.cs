/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 06/07/2018
 * Time: 22:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.YellowPad
{
	public partial class YellowPadWindow : Window
	{
		void StylusToolOnClick(object sender, RoutedEventArgs e)
		{
			StylusToolDialog dlg = new StylusToolDialog()
			{
				Owner = this,
				DrawingAttributes = inkcanv.DefaultDrawingAttributes
			};
			if ((bool)dlg.ShowDialog().GetValueOrDefault())
			{
				inkcanv.DefaultDrawingAttributes = dlg.DrawingAttributes;
			}
		}
		
		void EraserToolOnClick(object sender, RoutedEventArgs e)
		{
			EraserToolDialog dlg = new EraserToolDialog()
			{
				Owner = this,
				EraserShape = inkcanv.EraserShape
			};
			if ((bool)dlg.ShowDialog().GetValueOrDefault())
			{
				inkcanv.EraserShape = dlg.EraserShape;
			}
		}

	}
}
