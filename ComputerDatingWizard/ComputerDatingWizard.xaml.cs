/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.06.2018
 * Time: 22:23
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
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class ComputerDatingWizard : Window
	{
		public ComputerDatingWizard()
		{
			InitializeComponent();
			
			frame.Navigate(new WizardPage0());
		}
	}
}