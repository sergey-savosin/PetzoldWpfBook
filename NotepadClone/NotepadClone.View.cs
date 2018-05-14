/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 14.05.2018
 * Time: 22:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of NotepadClone_View.
	/// </summary>
	public partial class NotepadClone
	{
		MenuItem itemStatus;
		
		void AddViewMenu(Menu menu)
		{
			MenuItem itemView = new MenuItem()
			{
				Header = "_View"
			};
			itemView.SubmenuOpened += ViewOnOpen;
			menu.Items.Add(itemView);
			
			// command StatusBar
			itemStatus = new MenuItem()
			{
				Header = "_Status Bar",
				IsCheckable = true
			};
			itemStatus.Checked += StatusOnCheck;
			itemStatus.Unchecked += StatusOnCheck;
			
			itemView.Items.Add(itemStatus);
		}

		void ViewOnOpen(object sender, RoutedEventArgs e)
		{
			itemStatus.IsChecked = (status.Visibility == Visibility.Visible);
		}

		void StatusOnCheck(object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			status.Visibility = item.IsChecked ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}
