/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.06.2018
 * Time: 22:36
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
using System.Windows.Navigation;

namespace Petzold.ComputerDatingWizard
{
	/// <summary>
	/// Interaction logic for WizardPage0.xaml
	/// </summary>
	public partial class WizardPage0 : Page
	{
		public WizardPage0()
		{
			InitializeComponent();
		}
		
		void BeginButtonOnClick(object sender, RoutedEventArgs args)
		{
			if (NavigationService.CanGoForward)
				NavigationService.GoForward();
			else
			{
				Vitals vitals = new Vitals();
				WizardPage1 page = new WizardPage1(vitals);
				NavigationService.Navigate(page);
			}
		}
	}
}