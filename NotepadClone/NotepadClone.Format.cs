/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.05.2018
 * Time: 8:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of NotepadClone.
	/// </summary>
	public partial class NotepadClone
	{
		void AddFormatMenu(Menu menu)
		{
			// top level menu Format
			MenuItem itemFormat = new MenuItem()
			{
				Header = "_Format"
			};
			menu.Items.Add(itemFormat);
			
			// command WordWrap
			WordWrapMenuItem itemWrap = new WordWrapMenuItem();
			itemFormat.Items.Add(itemWrap);
			
			// bind to TextBox
			Binding bind = new Binding()
			{
				Path = new PropertyPath(TextBox.TextWrappingProperty),
				Source = txtbox,
				Mode = BindingMode.TwoWay
			};
			itemWrap.SetBinding(WordWrapMenuItem.WordWrapProperty, bind);
			
			// command Font
			MenuItem itemFont = new MenuItem()
			{
				Header = "_Font..."
			};
			itemFont.Click += FontOnClick;
			itemFormat.Items.Add(itemFont);
		}

		void FontOnClick(object sender, RoutedEventArgs e)
		{
			FontDialog dlg = new FontDialog()
			{
				Owner = this,
				Typeface = new Typeface(
					txtbox.FontFamily,
					txtbox.FontStyle,
					txtbox.FontWeight,
					txtbox.FontStretch),
				FaceSize = txtbox.FontSize
			};
			
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				// set new font properties
				txtbox.FontFamily = dlg.Typeface.FontFamily;
				txtbox.FontSize = dlg.FaceSize;
				txtbox.FontStyle = dlg.Typeface.Style;
				txtbox.FontWeight = dlg.Typeface.Weight;
				txtbox.FontStretch = dlg.Typeface.Stretch;
			}
		}
	}
}
