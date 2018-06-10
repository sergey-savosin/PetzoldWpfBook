/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 06/08/2018
 * Time: 22:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.YellowPad
{
	public partial class YellowPadWindow : Window
	{
		void AboutOnClick(object sender, RoutedEventArgs e)
		{
			YellowPadAboutDialog dlg = new YellowPadAboutDialog()
			{
				Owner = this
			};
			dlg.ShowDialog();
		}
		
		void HelpOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			YellowPadHelp win = new YellowPadHelp()
			{
				Owner = this
			};
			win.Show();
		}
	}
}
