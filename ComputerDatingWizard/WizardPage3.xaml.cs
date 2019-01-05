/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 17.06.2018
 * Time: 23:12
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

namespace Petzold.ComputerDatingWizard
{
	/// <summary>
	/// Interaction logic for WizardPage3.xaml
	/// </summary>
	public partial class WizardPage3 : Page
	{
		Vitals vitals;
		
		public WizardPage3(Vitals vitals)
		{
			InitializeComponent();
			this.vitals = vitals;
		}
		
		void PreviousButtonOnClick(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		
		void FinishButtonOnClick(object sender, RoutedEventArgs e)
		{
			vitals.MomsMaidenName = txtboxMom.Text;
			vitals.Pet
				= Vitals.GetCheckedRadioButton(grpboxPet).Content as string;
			vitals.Income
				= Vitals.GetCheckedRadioButton(grpboxIncome).Content as string;
			WizardPage4 page = new WizardPage4(vitals);
			NavigationService.Navigate(page);
		}
	}
}