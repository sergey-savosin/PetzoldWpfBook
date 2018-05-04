/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 01.05.2018
 * Time: 7:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Printing;

namespace Petzold.PrintBunchaButton
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class PrintBunchaButton : Window
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application app = new Application();
			app.Run(new PrintBunchaButton());
		}
		
		public PrintBunchaButton()
		{
			Title = "Print a bunch of buttons";
			SizeToContent = SizeToContent.WidthAndHeight;
			ResizeMode = ResizeMode.CanMinimize;
			
			// print button
			Button btn = new Button()
			{
				FontSize = 24,
				Content = "Print...",
				Padding = new Thickness(12),
				Margin = new Thickness(96)
			};
			btn.Click += PrintOnClick;
			Content = btn;
		}
		
		void PrintOnClick(object sender, RoutedEventArgs e)
		{
			PrintDialog dlg = new PrintDialog();
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				Grid grid = new Grid();
				
				// make 5 cols, 5 rows
				for (int i=0; i<5; i++)
				{
					ColumnDefinition coldef = new ColumnDefinition()
					{
						Width = GridLength.Auto
					};
					grid.ColumnDefinitions.Add(coldef);
					
					RowDefinition rowdef = new RowDefinition()
					{
						Height = GridLength.Auto
					};
					grid.RowDefinitions.Add(rowdef);
				}
				
				// gradient fill
				grid.Background = new LinearGradientBrush(
					Colors.Gray,
					Colors.White,
					new Point(0, 0),
					new Point(1, 1));
				
				Random rand = new Random();
				
				// fill the grid with 25 buttons
				for (int i=0; i<25; i++)
				{
					Button btn = new Button()
					{
						FontSize = 12 + rand.Next(8),
						Content = "Button No. " + (i + 1),
						HorizontalAlignment = HorizontalAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center,
						Margin = new Thickness(6)
					};
					grid.Children.Add(btn);
					Grid.SetRow(btn, i % 5);
					Grid.SetColumn(btn, i / 5);
				}
				
				Size sizeGrid = grid.DesiredSize;

				grid.Measure(
					new Size(Double.PositiveInfinity, Double.PositiveInfinity));
				sizeGrid = grid.DesiredSize;
				
				// center the grid on page
				Point ptGrid = new Point(
					(dlg.PrintableAreaWidth - sizeGrid.Width) / 2,
					(dlg.PrintableAreaHeight - sizeGrid.Height) / 2);
				
				grid.Arrange(new Rect(ptGrid, sizeGrid));
				
				dlg.PrintVisual(grid, Title);
			}
		}
	}
}
