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

namespace Petzold.NotepadClone
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
				Height = GridLength.Auto
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
				Height = GridLength.Auto
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
			
			// FontFamily
			TBWLAddToGrid(ref boxFamily, ref gridBoxes, 0, "Font Family");
			
			// FontStyle
			TBWLAddToGrid(ref boxStyle, ref gridBoxes, 1, "Style");
			
			// FontWeight
			TBWLAddToGrid(ref boxWeight, ref gridBoxes, 2, "Weight");
			
			// FontStretch
			TBWLAddToGrid(ref boxStretch, ref gridBoxes, 3, "Stretch");
			
			// FaceSize
			TBWLAddToGrid(ref boxSize, ref gridBoxes, 4, "Size");
			
			// sample Label
			lblDisplay = new Label()
			{
				Content = "AaBbCc XxYyZz 012345",
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			gridMain.Children.Add(lblDisplay);
			Grid.SetRow(lblDisplay, 1);
			
			// Grid 1x5 for buttons
			Grid gridButtons = new Grid();
			gridMain.Children.Add(gridButtons);
			Grid.SetRow(gridButtons, 2);
			for (int i = 0; i<5; i++)
				gridButtons.ColumnDefinitions.Add(new ColumnDefinition());
			
			// OK button
			Button btn = new Button()
			{
				Content = "OK",
				IsDefault = true,
				HorizontalAlignment = HorizontalAlignment.Center,
				MinWidth = 60,
				Margin = new Thickness(12)
			};
			btn.Click += OkOnClick;
			gridButtons.Children.Add(btn);
			Grid.SetColumn(btn, 1);
			
			// Cancel button
			btn = new Button()
			{
				Content = "Cancel",
				IsCancel = true,
				HorizontalAlignment = HorizontalAlignment.Center,
				MinWidth = 60,
				Margin = new Thickness(12)
			};
			gridButtons.Children.Add(btn);
			Grid.SetColumn(btn, 3);
			
			// init FontFamily with system fonts
			foreach(FontFamily fam in Fonts.SystemFontFamilies)
				boxFamily.Add(fam);
			
			// init FontSize
			double[] ptSizes = new double[] {8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72};
			foreach (double ptSize in ptSizes)
				boxSize.Add(ptSize);
			
			// set event handlers
			boxFamily.SelectionChanged += FamilyOnSelectionChanged;
			boxStyle.SelectionChanged += StyleOnSelectionChanged;
			boxWeight.SelectionChanged += StyleOnSelectionChanged;
			boxStretch.SelectionChanged += StyleOnSelectionChanged;
			boxSize.SelectionChanged += SizeOnSelectionChanged;
			
			// init first selections
			Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
			FaceSize = FontSize;
			
			// take a focus
			boxFamily.Focus();
			
			// allow updates
			isUpdateSuppressed = false;
			UpdateSample();
		}
		
		// helper method for ctor
		void TBWLAddToGrid(ref TextBoxWithLister box, ref Grid grid, int col, string caption)
		{
			if (box != null)
				throw new ArgumentException("box var = null");
			
			// create Label
			Label lbl = new Label()
			{
				Content = caption,
				Margin = new Thickness(12, 12, 12, 0)
			};
			grid.Children.Add(lbl);
			Grid.SetRow(lbl, 0);
			Grid.SetColumn(lbl, col);
			
			// create Caption
			box = new TextBoxWithLister()
			{
				IsReadOnly = true,
				Margin = new Thickness(12, 0, 12, 12)
			};
			grid.Children.Add(box);
			Grid.SetRow(box, 1);
			Grid.SetColumn(box, col);
		}

		void OkOnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		void FamilyOnSelectionChanged(object sender, EventArgs e)
		{
			// get selected font family
			FontFamily fntfam = (FontFamily)boxFamily.SelectedItem;
			
			// save previous values
			FontStyle? fntstyPrevious = (FontStyle?)boxStyle.SelectedItem;
			FontWeight? fntwtPrevious = (FontWeight?)boxWeight.SelectedItem;
			FontStretch? fntstrPrevious = (FontStretch?)boxStretch.SelectedItem;
			
			isUpdateSuppressed = true;
			
			boxStyle.Clear();
			boxWeight.Clear();
			boxStretch.Clear();
			
			// enumerate typefaces
			foreach (FamilyTypeface ftf in fntfam.FamilyTypefaces)
			{
				// boxStyle
				if (!boxStyle.Contains(ftf.Style))
				{
					if (ftf.Style == FontStyles.Normal)
						boxStyle.Insert(0, ftf.Style);
					else
						boxStyle.Add(ftf.Style);
				}
				
				// boxWeight
				if (!boxWeight.Contains(ftf.Weight))
				{
					if (ftf.Weight == FontWeights.Normal)
						boxWeight.Insert(0, ftf.Weight);
					else
						boxWeight.Add(ftf.Weight);
				}
				
				// boxStretch
				if (!boxStretch.Contains(ftf.Stretch))
				{
					if (ftf.Stretch == FontStretches.Normal)
						boxStretch.Insert(0, ftf.Stretch);
					else
						boxStretch.Add(ftf.Stretch);
				}
				
			}
			// select boxStyle
			if (boxStyle.Contains(fntstyPrevious))
				boxStyle.SelectedItem = fntstyPrevious;
			else
				boxStyle.SelectedIndex = 0;
			
			// select boxWeight
			if (boxWeight.Contains(fntwtPrevious))
				boxWeight.SelectedItem = fntwtPrevious;
			else
				boxWeight.SelectedIndex = 0;
			
			// select boxStretch
			if (boxStretch.Contains(fntstrPrevious))
				boxStretch.SelectedItem = fntstrPrevious;
			else
				boxStretch.SelectedIndex = 0;
			
			// Update sample
			isUpdateSuppressed = false;
			UpdateSample();
		}

		// Handler for SelectionChanged of boxStyle/Weight/Stretch
		void StyleOnSelectionChanged(object sender, EventArgs e)
		{
			UpdateSample();
		}

		void SizeOnSelectionChanged(object sender, EventArgs e)
		{
			UpdateSample();
		}
		
		void UpdateSample()
		{
			if (isUpdateSuppressed)
				return;
			lblDisplay.FontFamily = (FontFamily)boxFamily.SelectedItem;
			lblDisplay.FontStyle = (FontStyle)boxStyle.SelectedItem;
			lblDisplay.FontWeight = (FontWeight)boxWeight.SelectedItem;
			lblDisplay.FontStretch = (FontStretch)boxStretch.SelectedItem;
			
			double size;
			if (!Double.TryParse(boxSize.Text, out size))
				size = 8.25;
			lblDisplay.FontSize = size / 0.75;
		}
	}
}
