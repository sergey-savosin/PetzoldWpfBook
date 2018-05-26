/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 24.05.2018
 * Time: 22:32
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

namespace Petzold.GradientBrushResourceDemo
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			Resources.Add("thicknessMargin", new Thickness(24, 12, 24, 23));
			InitializeComponent();
		}
	}
}