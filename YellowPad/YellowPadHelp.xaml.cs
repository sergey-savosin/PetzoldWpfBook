/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 10.06.2018
 * Time: 23:15
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

namespace Petzold.YellowPad
{
	/// <summary>
	/// Interaction logic for YellowPadHelp.xaml
	/// </summary>
	public partial class YellowPadHelp : NavigationWindow
	{
		public YellowPadHelp()
		{
			InitializeComponent();
			
			// set focus to the tree
			(tree.Items[0] as TreeViewItem).IsSelected = true;
			tree.Focus();
		}
		
		void HelpOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			TreeViewItem item = e.NewValue as TreeViewItem;
			if (item.Tag == null)
				return;
			
			frame.Navigate(new Uri(item.Tag as string, UriKind.Relative));
		}
	}
}