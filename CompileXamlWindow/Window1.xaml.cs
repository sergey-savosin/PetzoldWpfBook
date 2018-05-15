/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 15.05.2018
 * Time: 22:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CompileXamlWindow
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
			
			foreach(PropertyInfo prop in typeof(Brushes).GetProperties())
				lstbox.Items.Add(prop.Name);
		}
		
		void ButtonOnClick(object sender, RoutedEventArgs args)
		{
			Button btn = sender as Button;
			MessageBox.Show("The button labled '" + btn.Content + "' has been clicked", Title);
		}
		
		void ListBoxOnSelection(object sender, SelectionChangedEventArgs args)
		{
			//ListBox lstbox = sender as ListBox;
			string strItem = lstbox.SelectedItem as string;
			PropertyInfo prop = typeof(Brushes).GetProperty(strItem);
			elips.Fill = (Brush)prop.GetValue(null, null);
		}
	}
}