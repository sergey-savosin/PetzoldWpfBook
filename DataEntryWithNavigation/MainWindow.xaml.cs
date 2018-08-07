/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 08/07/2018
 * Time: 20:41
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
using Petzold.MultiRecordDataEntry;
using Petzold.SingleRecordDataEntry;

namespace Petzold.DataEntryWithNavigation
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		People people;
		
		public MainWindow()
		{
			InitializeComponent();
			
			// File->New
			ApplicationCommands.New.Execute(null, this);
			
			// Set focus
			pnlPerson.Children[1].Focus();
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
			InitializeNewPeopleObject();
		}
		
		void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			people.Save(this);
		}

		void InitializeNewPeopleObject()
		{
			navbar.Collection = people;
			navbar.ItemType = typeof(Person);
			pnlPerson.DataContext = people;
		}
	}
}