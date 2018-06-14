/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 14.06.2018
 * Time: 21:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Specialized;
using Petzold.RecurceDirectoriesIncrementally;

namespace Petzold.ComputerDatingWizard
{
	/// <summary>
	/// Interaction logic for DirectoryPage.xaml
	/// </summary>
	public partial class DirectoryPage : PageFunction<DirectoryInfo>
	{
		public DirectoryPage()
		{
			InitializeComponent();
		}
	}
}