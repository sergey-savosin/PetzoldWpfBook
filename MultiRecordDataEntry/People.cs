/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 07/31/2018
 * Time: 22:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Microsoft.Win32;
using Petzold.SingleRecordDataEntry;

namespace Petzold.MultiRecordDataEntry
{
	/// <summary>
	/// Description of People.
	/// </summary>
	public class People : ObservableCollection<Person>
	{
		const string strFilter = "People XML files (*.PeopleXml)|" +
			"*.PeopleXml|All files (*.*)|*.*";
		
		public static People Load(Window win)
		{
			OpenFileDialog dlg = new OpenFileDialog()
			{
				Filter = strFilter
			};
			People people = null;
			if ((bool)dlg.ShowDialog(win))
			{
				try
				{
					StreamReader reader = new StreamReader(dlg.FileName);
					XmlSerializer xml = new XmlSerializer(typeof(People));
					people = (People)xml.Deserialize(reader);
					reader.Close();
				}
				catch (Exception exc)
				{
					MessageBox.Show("Could not load file: " + exc.Message,
					                win.Title,
					                MessageBoxButton.OK,
					                MessageBoxImage.Exclamation);
					people = null;
				}
			}
			return people;
		}
		
		public bool Save(Window win)
		{
			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = strFilter
			};
			if ((bool)dlg.ShowDialog(win))
			{
				try
				{
					StreamWriter writer = new StreamWriter(dlg.FileName);
					XmlSerializer xml = new XmlSerializer(GetType());
					xml.Serialize(writer, this);
					writer.Close();
				}
				catch (Exception exc)
				{
					MessageBox.Show("Could not save file: " + exc.Message,
					                win.Title,
					                MessageBoxButton.OK,
					                MessageBoxImage.Exclamation);
					return false;
				}
			}
			return true;
		}
		
		public People()
		{
		}
	}
}
