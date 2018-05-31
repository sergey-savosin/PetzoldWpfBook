/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 31.05.2018
 * Time: 21:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32;

namespace Petzold.YellowPad
{
	/// <summary>
	/// Description of YellowPadWindow_File.
	/// </summary>
	public partial class YellowPadWindow : Window
	{
		void NewOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			inkcanv.Strokes.Clear();
		}
		
		void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog()
			{
				CheckFileExists = true,
				Filter = "Ink serialized format (*.isf)|*.isf|" +
					"All files (*.*)|*.*"
			};
			
			if ((bool)dlg.ShowDialog(this))
			{
				try
				{
					FileStream file = new FileStream(dlg.FileName,
					                                 FileMode.Open,
					                                 FileAccess.Read);
					inkcanv.Strokes = new StrokeCollection(file);
					file.Close();
				}
				catch (Exception exc)
				{
					MessageBox.Show(exc.Message, Title);
				}
			}
		}

		void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = "Ink serialized format (*.isf)|*.isf|"+
					"XAML drawing file (*.xaml)|*.xaml|"+
					"All files (*.*)|*.*"
			};
			
			if ((bool)dlg.ShowDialog(this))
			{
				try
				{
					FileStream file = new FileStream(
						dlg.FileName,
						FileMode.Create,
						FileAccess.Write);
					
					if (dlg.FilterIndex == 1 || dlg.FilterIndex == 3)
					{
						inkcanv.Strokes.Save(file);
					}
					else
					{
						// save strokes in object DrawingGroup
						DrawingGroup drawgrp = new DrawingGroup();
						foreach (Stroke strk in inkcanv.Strokes)
						{
							Color clr = strk.DrawingAttributes.Color;
							if (strk.DrawingAttributes.IsHighlighter)
								clr = Color.FromArgb(128, clr.R, clr.G, clr.B);
							drawgrp.Children.Add(
								new GeometryDrawing(
									new SolidColorBrush(clr),
									null,
									strk.GetGeometry()));
						}
						XamlWriter.Save(drawgrp, file);
					}
					file.Close();
				}
				catch (Exception exc)
				{
					MessageBox.Show(exc.Message, Title);
				}
			}
		}
				
		void CloseOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			// close the window
			Close();
		}

	}
}
