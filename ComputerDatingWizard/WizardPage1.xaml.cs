/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.06.2018
 * Time: 22:44
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
	/// Interaction logic for WizardPage1.xaml
	/// </summary>
	public partial class WizardPage1 : Page
	{
		Vitals vitals;
		
		public WizardPage1(Vitals vitals)
		{
			InitializeComponent();
			this.vitals = vitals;
		}
		
		void PreviousButtonOnClick(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		
		void NextButtonOnClick(object sender, RoutedEventArgs e)
		{
			vitals.Name = txtboxName.Text;
			vitals.Home =
				Vitals.GetCheckedRadioButton(grpboxHome).Content as string;
			vitals.Gender = 
				Vitals.GetCheckedRadioButton(grpboxGender).Content as string;
			
			if (NavigationService.CanGoForward)
				NavigationService.GoForward();
			else
			{
				WizardPage2 page = new WizardPage2(vitals);
				NavigationService.Navigate(page);
			}
			
		}
	}
}