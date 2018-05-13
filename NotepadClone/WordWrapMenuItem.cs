/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.05.2018
 * Time: 8:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of WordWrapMenuItem.
	/// </summary>
	public class WordWrapMenuItem : MenuItem
	{
		// register d.p.
		public static DependencyProperty WordWrapProperty =
			DependencyProperty.Register("WordWrap", typeof(TextWrapping), typeof(WordWrapMenuItem));
		
		// public property
		public TextWrapping WordWrap
		{
			set { SetValue(WordWrapProperty, value); }
			get { return (TextWrapping)GetValue(WordWrapProperty); }
		}
		
		public WordWrapMenuItem()
		{
			Header = "_Word Wrap";
			MenuItem item = new MenuItem()
			{
				Header = "_No Wrap",
				Tag = TextWrapping.NoWrap
			};
			item.Click += MenuItemOnClick;
			Items.Add(item);
			
			item = new MenuItem()
			{
				Header = "_Wrap",
				Tag = TextWrapping.Wrap
			};
			item.Click += MenuItemOnClick;
			Items.Add(item);
			
			item = new MenuItem()
			{
				Header = "Wrap with _Overflow",
				Tag = TextWrapping.WrapWithOverflow
			};
			item.Click += MenuItemOnClick;
			Items.Add(item);
		}
		
		// mark command with current state of WordWrap
		protected override void OnSubmenuOpened(RoutedEventArgs e)
		{
			base.OnSubmenuOpened(e);
			
			foreach(MenuItem item in Items)
			{
				item.IsChecked = ((TextWrapping)item.Tag == WordWrap);
			}
		}

		// Set WordWrap value
		void MenuItemOnClick(object sender, RoutedEventArgs e)
		{
			WordWrap = (TextWrapping)(e.Source as MenuItem).Tag;
		}
	}
}
