/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 14.06.2018
 * Time: 21:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Petzold.RecurceDirectoriesIncrementally;

namespace Petzold.ComputerDatingWizard
{
	/// <summary>
	/// Interaction logic for WizardPage2.xaml
	/// </summary>
	public partial class WizardPage2 : Page
	{
		Vitals vitals;
		
		public WizardPage2(Vitals vitals)
		{
			InitializeComponent();
			this.vitals = vitals;
		}
		
		void BrowseButtonOnClick(object sender, RoutedEventArgs e)
		{
			DirectoryPage page = new DirectoryPage();
			page.Return += DirPageOnReturn;
			NavigationService.Navigate(page);
		}
		
		void PreviousButtonOnClick(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		
		void NextButtonOnClick(object sender, RoutedEventArgs e)
		{
			vitals.FavoriteOS = txtboxFavoriteOS.Text;
			vitals.Directory = txtboxFavoriteDir.Text;
			if (NavigationService.CanGoForward)
				NavigationService.GoForward();
			else
			{
				WizardPage3 page = new WizardPage3(vitals);
				NavigationService.Navigate(page);
			}
				
		}

		void DirPageOnReturn(object sender, ReturnEventArgs<DirectoryInfo> e)
		{
			if (e.Result != null)
			{
				txtboxFavoriteDir.Text = e.Result.FullName;
			}
		}
	}
}