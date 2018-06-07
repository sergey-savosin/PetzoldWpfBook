/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 06/07/2018
 * Time: 22:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;

namespace Petzold.YellowPad
{
	/// <summary>
	/// Description of EraserToolDialog.
	/// </summary>
	public class EraserToolDialog : StylusToolDialog
	{
		public EraserToolDialog()
		{
			Title = "Eraser Tool";
			chkboxPressure.Visibility = Visibility.Collapsed;
			chkboxHighlighter.Visibility = Visibility.Collapsed;
			lstboxColor.Visibility = Visibility.Collapsed;
		}
		
		// public property for initialization
		public StylusShape EraserShape
		{
			set
			{
				txtboxHeight.Text = (0.75 * value.Height).ToString("F1");
				txtboxWidth.Text = (0.75 * value.Width).ToString("F1");
				txtboxAngle.Text = value.Rotation.ToString();
				if (value is EllipseStylusShape)
					radioEllipse.IsChecked = true;
				else
					radioRect.IsChecked = true;
			}
			
			get
			{
				StylusShape eraser;
				double width = Double.Parse(txtboxWidth.Text) / 0.75;
				double height = Double.Parse(txtboxHeight.Text) / 0.75;
				double angle = Double.Parse(txtboxAngle.Text);
				if ((bool)radioEllipse.IsChecked)
					eraser = new EllipseStylusShape(width, height, angle);
				else
					eraser = new RectangleStylusShape(width, height, angle);
				
				return eraser;
			}
		}
	}
}
