/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 08/06/2018
 * Time: 21:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.NavigationBar
{
	/// <summary>
	/// Interaction logic for NavigationBar.xaml
	/// </summary>
	public partial class NavigationBar : ToolBar
	{
		IList coll;
		ICollectionView collview;
		Type typeItem;
		
		string strOriginal;
		
		public NavigationBar()
		{
			InitializeComponent();
		}
		
		public IList Collection
		{
			set
			{
				coll = value;
				
				collview = CollectionViewSource.GetDefaultView(coll);
				collview.CurrentChanged += CollectionViewOnCurrentChanged;
				collview.CollectionChanged += CollectionViewOnCollectionChanged;
				
				// call initializer
				CollectionViewOnCurrentChanged(null, null);
				// init textbox
				txtblkTotal.Text = coll.Count.ToString();
			}
			get
			{
				return coll;
			}
		}
		
		// type of elements
		public Type ItemType
		{
			set { typeItem = value; }
			get { return typeItem; }
		}

		void CollectionViewOnCurrentChanged(object sender, EventArgs e)
		{
			txtboxCurrent.Text = ( 1 + collview.CurrentPosition).ToString();
			btnPrev.IsEnabled = collview.CurrentPosition > 0;
			btnNext.IsEnabled = collview.CurrentPosition < (coll.Count - 1);
			btnDel.IsEnabled = coll.Count > 1;
		}

		void CollectionViewOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			txtblkTotal.Text = coll.Count.ToString();
		}
		
		void FirstOnClick(object sender, RoutedEventArgs e)
		{
			collview.MoveCurrentToFirst();
		}
		
		void PreviousOnClick(object sender, RoutedEventArgs e)
		{
			collview.MoveCurrentToPrevious();
		}
		
		void TextBoxOnGotFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			strOriginal = txtboxCurrent.Text;
		}
		
		
		void TextBoxOnLostFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			int current;
			if (Int32.TryParse(txtboxCurrent.Text, out current))
			{
				if (current > 0 && current <= coll.Count)
				{
					collview.MoveCurrentToPosition(current - 1);
				}
			}
			else
			{
				txtboxCurrent.Text = strOriginal;
			}
		}
		
		void TextBoxOnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				txtboxCurrent.Text = strOriginal;
				e.Handled = true;
			}
			else if (e.Key == Key.Enter)
			{
				e.Handled = true;
			}
			else
			{
				return;
			}
			
			MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
		}
		
		void NextOnClick(object sender, RoutedEventArgs e)
		{
			collview.MoveCurrentToNext();
		}
		
		void LastOnClick(object sender, RoutedEventArgs e)
		{
			collview.MoveCurrentToLast();
		}
		
		void AddOnClick(object sender, RoutedEventArgs e)
		{
			ConstructorInfo info = typeItem.GetConstructor(System.Type.EmptyTypes);
			coll.Add(info.Invoke(null));
			collview.MoveCurrentToLast();
		}
		
		void DeleteOnClick(object sender, RoutedEventArgs e)
		{
			coll.RemoveAt(collview.CurrentPosition);
		}

	}
}