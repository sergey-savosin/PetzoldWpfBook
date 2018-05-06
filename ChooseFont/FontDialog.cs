/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 05.05.2018
 * Time: 17:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Petzold.ChooseFont;

namespace ChooseFont
{
	/// <summary>
	/// Description of FontDialog.
	/// </summary>
	public class FontDialog : Window
	{
		TextBoxWithLister boxFamily, boxStyle, boxWeight, boxStretch, boxSize;
		Label lblDisplay;
		bool isUpdateSuppressed = true;
		
		// public properties
		public Typeface Typeface
		{
			set
			{
				if (boxFamily.Contains(value.FontFamily))
					boxFamily.SelectedItem = value.FontFamily;
				else
					boxFamily.SelectedIndex = 0;
				
				if (boxStyle.Contains(value.Style))
					boxStyle.SelectedItem = value.Style;
				else
					boxStyle.SelectedIndex = 0;
				
				if (boxWeight.Contains(value.Weight))
					boxWeight.SelectedItem = value.Weight;
				else
					boxWeight.SelectedIndex = 0;
				
				if (boxStretch.Contains(value.Stretch))
					boxStretch.SelectedItem = value.Stretch;
				else
					boxStretch.SelectedIndex = 0;
			}
			
			get
			{
				return new Typeface(
					(FontFamily)boxFamily.SelectedItem,
					(FontStyle)boxStyle.SelectedItem,
					(FontWeight)boxWeight.SelectedItem,
					(FontStretch)boxStretch.SelectedItem);
			}
		}
		
		public double FaceSize
		{
			set
			{
				double size = 0.75 * value;
				boxSize.Text = size.ToString();
				if (!boxSize.Contains(size))
					boxSize.Insert(0, size);
				boxSize.SelectedItem = size;
			}
			
			get
			{
				double size;
				if (!Double.TryParse(boxSize.Text, out size))
					size = 8.25;
				return size / 0.75;
			}
		}
		
		// ctor
		public FontDialog()
		{
			Title = "Font";
			ShowInTaskbar = false;
			WindowStyle = WindowStyle.ToolWindow;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			SizeToContent = SizeToContent.WidthAndHeight;
			ResizeMode = ResizeMode.NoResize;
			
			// Grid with 3 rows
			Grid gridMain = new Grid();
			Content = gridMain;
			
			// row for TextBoxWithLister
			RowDefinition rowdef = new RowDefinition()
			{
				Height = new GridLength(200, GridUnitType.Pixel)
			};
			gridMain.RowDefinitions.Add(rowdef);
			
			// row for text sample
			rowdef = new RowDefinition()
			{
				Height = new GridLength(150, GridUnitType.Pixel)
			};
			gridMain.RowDefinitions.Add(rowdef);
			
			// row for buttons
			rowdef = new RowDefinition()
			{
				Height = GridLength.Auto()
			};
			gridMain.RowDefinitions.Add(rowdef);
			
			// the only column of this Grid
			ColumnDefinition coldef = new ColumnDefinition()
			{
				Width = new GridLength(650, GridUnitType.Pixel)
			};
			gridMain.ColumnDefinitions.Add(coldef);
			
			// Grid 2x5 for TextBoxWithLister
			Grid gridBoxes = new Grid();
			gridMain.Children.Add(gridBoxes);
			
			// row for Labels
			rowdef = new RowDefinition()
			{
				Height = GridLength.Auto()
			};
			gridBoxes.RowDefinitions.Add(rowdef);
			
			// row for TextBoxWithLister
			rowdef = new RowDefinition()
			{
				Height = new GridLength(100, GridUnitType.Star)
			};
			gridBoxes.RowDefinitions.Add(rowdef);
			
			// 1 column (FontFamily)
			coldef = new ColumnDefinition()
			{
				Width = new GridLength(175, GridUnitType.Star)
			};
			gridBoxes.ColumnDefinitions.Add(coldef);
			
			// 2 column (FontStyle)
			coldef = new ColumnDefinition()
			{
				Width = new GridLength(100, GridUnitType.Star)
			};
			gridBoxes.ColumnDefinitions.Add(coldef);
			
			// 3 column (FontWeight)
			coldef = new ColumnDefinition()
			{
				Width = new GridLength(100, GridUnitType.Star)
			};
			gridBoxes.ColumnDefinitions.Add(coldef);
			
			// 4 column (FontStretch)
			coldef = new ColumnDefinition()
			{
				Width = new GridLength(100, GridUnitType.Star)
			};
			gridBoxes.ColumnDefinitions.Add(coldef);
			
			// 5 columns (FaceSize)
			coldef = new ColumnDefinition()
			{
				Width = new GridLength(75, GridUnitType.Star)
			};
			gridBoxes.ColumnDefinitions.Add(coldef);
			
			
			// create Labels and TBWL for FontFamily
		}
	}
}
