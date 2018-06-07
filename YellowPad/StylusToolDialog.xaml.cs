/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 05.06.2018
 * Time: 22:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using Petzold.ListColorsElegantly;

namespace Petzold.YellowPad
{
	/// <summary>
	/// Interaction logic for StylusToolDialog.xaml
	/// </summary>
	public partial class StylusToolDialog : Window
	{
		public StylusToolDialog()
		{
			InitializeComponent();
			txtboxWidth.TextChanged += TextBoxOnTextChanged;
			txtboxHeight.TextChanged += TextBoxOnTextChanged;
			txtboxAngle.TextChanged += TextBoxOnTextChanged;
			txtboxWidth.Focus();
		}
		
		// public property
		public DrawingAttributes DrawingAttributes
		{
			set
			{
				txtboxHeight.Text = (0.75 * value.Height).ToString("F1");
				txtboxWidth.Text = (0.75 * value.Width).ToString("F1");
				txtboxAngle.Text = 
					(180 * Math.Acos(value.StylusTipTransform.M11) /
					 Math.PI).ToString("F1");
				chkboxPressure.IsChecked = value.IgnorePressure;
				chkboxHighlighter.IsChecked = value.IsHighlighter;
				if (value.StylusTip == StylusTip.Ellipse)
					radioEllipse.IsChecked = true;
				else
					radioRect.IsChecked = true;
				
				lstboxColor.SelectedColor = value.Color;
				lstboxColor.ScrollIntoView(lstboxColor.SelectedColor);
			}
			
			get
			{
				DrawingAttributes drawattr = new DrawingAttributes()
				{
					Height = Double.Parse(txtboxHeight.Text) / 0.75,
					Width = Double.Parse(txtboxWidth.Text) / 0.75,
					StylusTipTransform =
						new RotateTransform(Double.Parse(txtboxAngle.Text)).Value,
					IgnorePressure = (bool) chkboxPressure.IsChecked,
					IsHighlighter = (bool) chkboxHighlighter.IsChecked,
					StylusTip = (bool) radioEllipse.IsChecked?
						StylusTip.Ellipse : StylusTip.Rectangle,
					Color = lstboxColor.SelectedColor
				};
				
				return drawattr;
			}
		}

		void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			double width, height, angle;
			btnOk.IsEnabled =
				Double.TryParse(txtboxWidth.Text, out width) &&
				width / 0.75 >= DrawingAttributes.MinWidth &&
				width / 0.75 <= DrawingAttributes.MaxWidth &&
				Double.TryParse(txtboxHeight.Text, out height) &&
				height / 0.75 >= DrawingAttributes.MinHeight &&
				height / 0.75 <= DrawingAttributes.MaxHeight &&
				Double.TryParse(txtboxAngle.Text, out angle);
		}
		
		void OkOnClick(object sender, RoutedEventArgs args)
		{
			DialogResult = true;
		}
	}
}