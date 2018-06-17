/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 17.06.2018
 * Time: 23:24
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
	/// Interaction logic for WizardPage4.xaml
	/// </summary>
	public partial class WizardPage4 : Page
	{
		public WizardPage4(Vitals vitals)
		{
			InitializeComponent();
			runName.Text = vitals.Name;
			runHome.Text = vitals.Home;
			runGender.Text = vitals.Gender;
			runOS.Text = vitals.FavoriteOS;
			runDirectory.Text = vitals.Directory;
			runMomsMaidenName.Text = vitals.MomsMaidenName;
			runPet.Text = vitals.Pet;
			runIncome.Text = vitals.Income;
		}
		
		void PreviousButtonOnClick(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		
		void SubmitButtonOnClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Thank you!\n\nYou will be contacted by email "
			 + "in four to six months.",
			 Application.Current.MainWindow.Title,
			 MessageBoxButton.OK,
			 MessageBoxImage.Exclamation);
			Application.Current.Shutdown();
		}
	}
}