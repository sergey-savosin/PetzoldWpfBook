/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 25.07.2018
 * Time: 22:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Petzold.SingleRecordDataEntry
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		const string strFilter = "Person XML files (*.PersonXml)|" + 
			"*.PersonXml|All files|*.*";
		XmlSerializer xml = new XmlSerializer(typeof(Person));
		
		public MainWindow()
		{
			InitializeComponent();
			
			ApplicationCommands.New.Execute(null, null);
			pnlPerson.Children[1].Focus();
		}
		
		void NewOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			pnlPerson.DataContext = new Person();
		}

		void OpenOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog()
			{
				Filter = strFilter
			};
			Person pers;
			if ((bool)dlg.ShowDialog(this))
			{
				try
				{
					StreamReader reader = new StreamReader(dlg.FileName);
					pers = (Person) xml.Deserialize(reader);
					reader.Close();
				}
				catch (Exception exc)
				{
					MessageBox.Show("Could not load file: " + exc.Message,
					                Title,
					                MessageBoxButton.OK,
					                MessageBoxImage.Exclamation);
					return;
				}
				pnlPerson.DataContext = pers;
			}
		}

		void SaveOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = strFilter
			};
			if ((bool) dlg.ShowDialog(this))
			{
				try
				{
					StreamWriter writer = new StreamWriter(dlg.FileName);
					xml.Serialize(writer, pnlPerson.DataContext);
					writer.Close();
				}
				catch (Exception exc)
				{
					MessageBox.Show("Could not save file: " +exc.Message,
						Title,
						MessageBoxButton.OK,
						MessageBoxImage.Exclamation);
					return;
				}
			}
		}
	}
}