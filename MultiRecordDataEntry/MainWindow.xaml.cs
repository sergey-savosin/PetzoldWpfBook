/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 07/31/2018
 * Time: 22:41
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
using Petzold.SingleRecordDataEntry;

namespace Petzold.MultiRecordDataEntry
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		People people;
		int index;
		
		public MainWindow()
		{
			InitializeComponent();
			
			// Init->New
			ApplicationCommands.New.Execute(null, this);
			
			// Set focus
			pnlPerson.Children[1].Focus();
		}
		
		void FirstOnClick(object sender, RoutedEventArgs e)
		{
			index = 0;
			pnlPerson.DataContext = people[index];
			EnableAndDisableButtons();
		}
		
		void PrevOnClick(object sender, RoutedEventArgs e)
		{
			index -= 1;
			pnlPerson.DataContext = people[index];
			EnableAndDisableButtons();
		}
		
		void NextOnClick(object sender, RoutedEventArgs e)
		{
			index += 1;
			pnlPerson.DataContext = people[index];
			EnableAndDisableButtons();
		}
		
		void LastOnClick(object sender, RoutedEventArgs e)
		{
			index = people.Count - 1;
			pnlPerson.DataContext = people[index];
			EnableAndDisableButtons();
		}
		
		void AddOnClick(object sender, RoutedEventArgs e)
		{
			index = people.Count;
			people.Insert(index, new Person());
		}
		
		void DelOnClick(object sender, RoutedEventArgs e)
		{
			people.RemoveAt(index);
			if (people.Count == 0)
			{
				people.Insert(0, new Person());
			}
			
			if (index > (people.Count - 1))
			{
				index--;
			}
			
			pnlPerson.DataContext = people[index];
			EnableAndDisableButtons();
		}
		
		void NewOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			people = new People();
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
			index = 0;
			if (people.Count == 0)
			{
				people.Insert(0, new Person());
			}
			pnlPerson.DataContext = people[0];
			EnableAndDisableButtons();
		}

		void EnableAndDisableButtons()
		{
			btnPrev.IsEnabled = index != 0;
			btnNext.IsEnabled = index < (people.Count - 1);
			pnlPerson.Children[1].Focus();
		}
	}
}