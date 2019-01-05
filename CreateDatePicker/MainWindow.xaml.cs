/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 19.07.2018
 * Time: 21:13
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

namespace Petzold.CreateDatePicker
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		
		void DatePickerOnDateChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
		{
			if (e.NewValue != null)
			{
				DateTime dt = (DateTime)e.NewValue;
				txtblkDate.Text = dt.ToString("d");
			}
			else
			{
				txtblkDate.Text = "";
			}
		}
	}
}