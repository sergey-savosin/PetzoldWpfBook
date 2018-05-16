/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 16.05.2018
 * Time: 21:11
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
using Petzold.SelectColorFromGrid;

namespace UseCustomClass
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}
		
		void ColorGridBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
		{
			ColorGridBox clrbox = args.Source as ColorGridBox;
			Background = (Brush) clrbox.SelectedValue;
		}
	}
}