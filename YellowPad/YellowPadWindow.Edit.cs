/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 06/07/2018
 * Time: 22:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace Petzold.YellowPad
{
	public partial class YellowPadWindow : Window
	{
		void EditOnOpened(object sender, RoutedEventArgs e)
		{
			itemFormat.IsEnabled = inkcanv.GetSelectedStrokes().Count > 0;
		}
		
		void CutCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = inkcanv.GetSelectedStrokes().Count > 0;
		}
		
		void CutOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			inkcanv.CutSelection();
		}
		
		void CopyOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			inkcanv.CopySelection();
		}
		
		void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = inkcanv.CanPaste();
		}
		
		void PasteOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			inkcanv.Paste();
		}
		
		void DeleteOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			foreach (Stroke strk in inkcanv.GetSelectedStrokes())
			{
				inkcanv.Strokes.Remove(strk);
			}
		}
		
		void SelectAllOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			inkcanv.Select(inkcanv.Strokes);
		}
		
		void FormatOnClick(object sender, RoutedEventArgs e)
		{
			StylusToolDialog dlg = new StylusToolDialog()
			{
				Owner = this,
				Title = "Format selection"
			};
			
			// stroke for the first selected stroke
			StrokeCollection strokes = inkcanv.GetSelectedStrokes();
			
			if (strokes.Count > 0)
				dlg.DrawingAttributes = strokes[0].DrawingAttributes;
			else
				dlg.DrawingAttributes = inkcanv.DefaultDrawingAttributes;
			
			if ((bool)dlg.ShowDialog().GetValueOrDefault())
			{
				foreach (Stroke strk in strokes)
				{
					strk.DrawingAttributes = dlg.DrawingAttributes;
				}
			}
		}
		

	}
}
