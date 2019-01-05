/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 08/07/2018
 * Time: 21:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Petzold.MultiRecordDataEntry;
using Petzold.SingleRecordDataEntry;

namespace Petzold.DataEntryWithListBox
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ListCollectionView collview;
		People people;
		
		public MainWindow()
		{
			InitializeComponent();
			
			// File->New
			ApplicationCommands.New.Execute(null, this);
			
			// Set focus
			pnlPerson.Children[1].Focus();
		}
		
		void AddOnClick(object sender, RoutedEventArgs e)
		{
			Person person = new Person();
			people.Add(person);
			lstbox.SelectedItem = person;
			pnlPerson.Children[1].Focus();
			collview.Refresh();
		}
		
		void DeleteOnClick(object sender, RoutedEventArgs e)
		{
			if (lstbox.SelectedItem != null)
			{
				people.Remove(lstbox.SelectedItem as Person);
				if (lstbox.Items.Count > 0)
				{
					lstbox.SelectedIndex = 0;
				}
				else
				{
					AddOnClick(sender, e);
				}
			}
		}
		
		void NewOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			people = new People();
			people.Add(new Person());
			InitializeNewPeopleObject();
		}
		
		void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			people = People.Load(this);
			if (people != null)
			{
				InitializeNewPeopleObject();
			}
		}
		
		void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			people.Save(this);
		}

		void InitializeNewPeopleObject()
		{
			collview = new ListCollectionView(people);
			
			// proplerty for sorting
			collview.SortDescriptions.Add(
				new SortDescription("LastName", ListSortDirection.Ascending));
			
			// linking ListBox and PersonPanel via ListCollectionView
			lstbox.ItemsSource = collview;
			pnlPerson.DataContext = collview;
			
			// index of ListBox current row
			if (lstbox.Items.Count > 0)
			{
				lstbox.SelectedIndex = 0;
			}
		}
	}
}