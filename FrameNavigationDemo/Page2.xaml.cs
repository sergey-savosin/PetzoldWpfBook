/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 10.06.2018
 * Time: 19:16
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

namespace Petzold.FrameNavigationDemo
{
	/// <summary>
	/// Interaction logic for Page2.xaml
	/// </summary>
	public partial class Page2 : Page
	{
		public Page2()
		{
			InitializeComponent();
		}
		void HyperlinkOnRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			NavigationService.Navigate(e.Uri);
			e.Handled = true;
		}
		
		void ButtonOnClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(
				new Uri("Page1.xaml", UriKind.Relative));
		}

	}
}