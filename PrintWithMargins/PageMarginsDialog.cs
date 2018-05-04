/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 30.04.2018
 * Time: 22:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.PrintWithMargins
{
	/// <summary>
	/// Description of PageMarginsDialog.
	/// </summary>
	public class PageMarginsDialog : Window
	{
		// Page sides
		enum Side
		{
			Left, Right, Top, Bottom
		}
		
		// 4 controls
		TextBox[] txtbox = new TextBox[4];
		Button btnOk, btnCancel;
		
		// public property Thickness
		public Thickness PageMargins
		{
			set
			{
				txtbox[(int)Side.Left].Text = (value.Left / 96).ToString("F3");
				txtbox[(int)Side.Right].Text = (value.Right / 96).ToString("F3");
				txtbox[(int)Side.Top].Text = (value.Top / 96).ToString("F3");
				txtbox[(int)Side.Bottom].Text = (value.Bottom / 96).ToString("F3");
			}
			get
			{
				return new Thickness(
					Double.Parse(txtbox[(int)Side.Left].Text) * 96,
					Double.Parse(txtbox[(int)Side.Top].Text) * 96,
					Double.Parse(txtbox[(int)Side.Right].Text) * 96,
					Double.Parse(txtbox[(int)Side.Bottom].Text) * 96
				);
			}
		}
		
		// ctor
		public PageMarginsDialog()
		{
			Title = "Page setup";
			ShowInTaskbar = false;
			WindowStyle = WindowStyle.ToolWindow;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			SizeToContent = SizeToContent.WidthAndHeight;
			ResizeMode = ResizeMode.NoResize;
			
			StackPanel stack = new StackPanel();
			Content = stack;
			
			GroupBox grpbox = new GroupBox()
			{
				Header = "Margins (inches)",
				Margin = new Thickness(12)
			};
			stack.Children.Add(grpbox);
			
			Grid grid = new Grid()
			{
				Margin = new Thickness(6)
			};
			grpbox.Content = grid;
			
			// 2 rows, 4 cols
			for (int i = 0; i<2; i++)
			{
				RowDefinition rowdef = new RowDefinition()
				{
					Height = GridLength.Auto
				};
				grid.RowDefinitions.Add(rowdef);
			}
			
			for (int i = 0; i<4; i++)
			{
				ColumnDefinition coldef = new ColumnDefinition()
				{
					Width = GridLength.Auto
				};
				grid.ColumnDefinitions.Add(coldef);
			}
			
			// Labels and TextBox
			for (int i = 0; i<4; i++)
			{
				Label lbl = new Label()
				{
					Content = "_" + Enum.GetName(typeof(Side), i) + ":",
					Margin = new Thickness(6),
					VerticalAlignment = VerticalAlignment.Center
				};
				grid.Children.Add(lbl);
				Grid.SetRow(lbl, i / 2);
				Grid.SetColumn(lbl, 2 * (i%2));
				
				txtbox[i] = new TextBox()
				{
					MinWidth = 48,
					Margin = new Thickness(6)
				};
				txtbox[i].TextChanged += TextBoxOnTextChanged;
				grid.Children.Add(txtbox[i]);
				Grid.SetRow(txtbox[i], i / 2);
				Grid.SetColumn(txtbox[i], 2 * (i % 2) + 1);
			}
			
			// create a grid for OK, Cancel buttons
			UniformGrid unigrid = new UniformGrid()
			{
				Rows = 1,
				Columns = 2
			};
			stack.Children.Add(unigrid);
			
			btnOk = new Button()
			{
				Content = "OK",
				IsDefault = true,
				IsEnabled = false,
				MinWidth = 60,
				Margin = new Thickness(12),
				HorizontalAlignment = HorizontalAlignment.Center
			};
			btnOk.Click += OkButtonOnClick;
			unigrid.Children.Add(btnOk);
			
			btnCancel = new Button()
			{
				Content = "Cancel",
				IsCancel = true,
				MinWidth = 60,
				Margin = new Thickness(12),
				HorizontalAlignment = HorizontalAlignment.Center
			};
			unigrid.Children.Add(btnCancel);
		}

		void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			double result;
			btnOk.IsEnabled =
				Double.TryParse(txtbox[(int)Side.Left].Text, out result) &&
				Double.TryParse(txtbox[(int)Side.Right].Text, out result) &&
				Double.TryParse(txtbox[(int)Side.Top].Text, out result) &&
				Double.TryParse(txtbox[(int)Side.Bottom].Text, out result);
		}

		void OkButtonOnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
