/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 11.07.2018
 * Time: 22:13
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

namespace Petzold.ContentTemplateDemo
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			
			// one more EmployeeButton
			EmployeeButton btn = new EmployeeButton()
			{
				Content = new Employee("Jim", "jim.png", new DateTime(1975, 6, 15), false)
			};
			stack.Children.Add(btn);
		}
		
		void EmployeeButtonOnClick(object sender, RoutedEventArgs e)
		{
			Button btn = e.Source as Button;
			Employee emp = btn.Content as Employee;
			MessageBox.Show(emp.Name + " button clicked!", Title);
		}
	}
}