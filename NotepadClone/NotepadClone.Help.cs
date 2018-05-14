/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 14.05.2018
 * Time: 22:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.NotepadClone
{
	public partial class NotepadClone
	{
		void AddHelpMenu(Menu menu)
		{
			MenuItem itemHelp = new MenuItem()
			{
				Header = "_Help"
			};
			menu.Items.Add(itemHelp);
			
			MenuItem itemAbout = new MenuItem()
			{
				Header = "_About " + strAppTitle + "..."
			};
			itemAbout.Click += AboutOnClick;
			itemHelp.Items.Add(itemAbout);
		}

		void AboutOnClick(object sender, RoutedEventArgs e)
		{
			AboutDialog dlg = new AboutDialog(this);
			dlg.ShowDialog();
		}
	}
}
