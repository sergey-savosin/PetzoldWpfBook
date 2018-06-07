/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 05/29/2018
 * Time: 22:41
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.YellowPad
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class YellowPadWindow : Window
	{
		public static readonly double widthCanvas = 5 * 96;
		public static readonly double heightCanvas = 7 * 96;
		
		public YellowPadWindow()
		{
			InitializeComponent();
			
			double y = 96;
			while (y <= heightCanvas)
			{
				Line line = new Line()
				{
					X1 = 0,
					Y1 = y,
					X2 = widthCanvas,
					Y2 = y,
					Stroke = Brushes.LightBlue
				};
				inkcanv.Children.Add(line);
				y += 24;
				
				// turn off Eraser-Mode menu for nonplanshet
				if (Tablet.TabletDevices.Count == 0)
					menuEraserMode.Visibility = Visibility.Collapsed;
			}
		}
		void EditOnOpened(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		
		void AboutOnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void FormatOnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void CutCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void CutOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void CopyOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void PasteOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void DeleteOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void SelectAllOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void HelpOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}