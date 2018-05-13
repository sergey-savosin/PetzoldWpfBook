/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 09.05.2018
 * Time: 22:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of NotepadClone_File.
	/// </summary>
	public partial class NotepadClone : Window
	{
		// filter for dialogs Save/Open
		protected string strFilter = "Text Documents(*.txt)|*.txt|All Files(*.*)|*.*";
		
		void AddFileMenu(Menu menu)
		{
			// File
			MenuItem itemFile = new MenuItem()
			{
				Header = "_File"
			};
			menu.Items.Add(itemFile);

			// New
			MenuItem itemNew = new MenuItem()
			{
				Header = "_New",
				Command = ApplicationCommands.New
			};
			itemFile.Items.Add(itemNew);
			CommandBindings.Add(
				new CommandBinding(ApplicationCommands.New, NewOnExecute));
			
			// Open
			MenuItem itemOpen = new MenuItem()
			{
				Header = "_Open...",
				Command = ApplicationCommands.Open
			};
			itemFile.Items.Add(itemOpen);
			CommandBindings.Add(
				new CommandBinding(ApplicationCommands.Open, OpenOnExecute));
			
			// Save
			MenuItem itemSave = new MenuItem()
			{
				Header = "_Save",
				Command = ApplicationCommands.Save
			};
			itemFile.Items.Add(itemSave);
			CommandBindings.Add(
				new CommandBinding(ApplicationCommands.Save, SaveOnExecute));
			
			// SaveAs
			MenuItem itemSaveAs = new MenuItem()
			{
				Header = "Save _As...",
				Command = ApplicationCommands.SaveAs
			};
			itemFile.Items.Add(itemSaveAs);
			CommandBindings.Add(
				new CommandBinding(ApplicationCommands.SaveAs, SaveAsOnExecute));
			
			// delimeters and Print
			itemFile.Items.Add(new Separator());
			AddPrintMenuItems(itemFile);
			itemFile.Items.Add(new Separator());
			
			// Exit
			MenuItem itemExit = new MenuItem()
			{
				Header = "E_xit"
			};
			itemExit.Click += ExitOnClick;
			itemFile.Items.Add(itemExit);
		}
		
		// command File>New
		protected virtual void NewOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			if (!OkToTrash())
				return;
			txtbox.Text = "";
			strLoadedFile = null;
			isFileDirty = false;
			UpdateTitle();
		}

		// command File>Open
		void OpenOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			if (!OkToTrash())
				return;
			OpenFileDialog dlg = new OpenFileDialog()
			{
				Filter = strFilter
			};
			if ((bool)dlg.ShowDialog(this))
			{
				LoadFile(dlg.FileName);
			}
		}

		// command File>Save
		void SaveOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			if (strLoadedFile == null || strLoadedFile.Length == 0)
				DisplaySaveDialog("");
			else
				SaveFile(strLoadedFile);
		}

		// command File>SaveAs
		void SaveAsOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			DisplaySaveDialog(strLoadedFile);
		}

		// command File>Exit
		void ExitOnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		bool DisplaySaveDialog(string strFileName)
		{
			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = strFilter,
				FileName = strFileName
			};
			if ((bool)dlg.ShowDialog(this))
			{
				SaveFile(dlg.FileName);
				return true;
			}
			return false; // for OkToTrash
		}
		
		// returns true if TextBox contents do not need to be saved
		bool OkToTrash()
		{
			if (!isFileDirty)
				return true;
			MessageBoxResult result =
				MessageBox.Show("The text in the file " +
				                strLoadedFile +
				                " has changen\n\n" +
				                "Do you want to save the changes?",
				                strAppTitle,
				                MessageBoxButton.YesNoCancel,
				                MessageBoxImage.Question,
				                MessageBoxResult.Yes);
			if (result == MessageBoxResult.Cancel)
				return false;
			else if (result == MessageBoxResult.No)
				return true;
			else // result == MessageBoxResult.Yes
			{
				if (strLoadedFile != null && strLoadedFile.Length > 0)
					return SaveFile(strLoadedFile);
				return DisplaySaveDialog("");
			}
		}
		
		// LoadFile. Show window in case of error
		void LoadFile(string strFileName)
		{
			try
			{
				txtbox.Text = File.ReadAllText(strFileName);
			}
			catch (Exception exc)
			{
				MessageBox.Show(
					"Error on FileOpen: " + exc.Message,
					strAppTitle,
					MessageBoxButton.OK,
					MessageBoxImage.Asterisk);
				
				return;
			}
			
			strLoadedFile = strFileName;
			UpdateTitle();
			txtbox.SelectionStart = 0;
			txtbox.SelectionLength = 0;
			isFileDirty = false;
		}
		
		// SaveFile. Show window in case of error
		bool SaveFile(string strFileName)
		{
			try
			{
				File.WriteAllText(strFileName, txtbox.Text);
			}
			catch (Exception exc)
			{
				MessageBox.Show(
					"Error on FileSave: " + exc.Message,
					strAppTitle,
					MessageBoxButton.OK,
					MessageBoxImage.Asterisk);
				return false;
			}
			
			strLoadedFile = strFileName;
			UpdateTitle();
			isFileDirty = false;
			return true;
		}
	}
}
