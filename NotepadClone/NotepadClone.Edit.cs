/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 12.05.2018
 * Time: 22:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of NotepadClone
	/// </summary>
	public partial class NotepadClone
	{
		void AddEditMenu(Menu menu)
		{
			// top level Edit menu
			MenuItem itemEdit = new MenuItem()
			{
				Header = "_Edit"
			};
			menu.Items.Add(itemEdit);
			
			// Undo command
			MenuItem itemUndo = new MenuItem()
			{
				Header = "_Undo",
				Command = ApplicationCommands.Undo
			};
			itemEdit.Items.Add(itemUndo);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Undo, UndoOnExecute, UndoCanExecute));
			
			// Redo command
			MenuItem itemRedo = new MenuItem()
			{
				Header = "_Redo",
				Command = ApplicationCommands.Redo
			};
			itemEdit.Items.Add(itemRedo);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Redo, RedoOnExecute, RedoCanExecute));
			
			itemEdit.Items.Add(new Separator());
			
			// commands Cut, Copy, Paste, Delete
			MenuItem itemCut = new MenuItem()
			{
				Header = "Cu_t",
				Command = ApplicationCommands.Cut
			};
			itemEdit.Items.Add(itemCut);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Cut, CutOnExecute, CutCanExecute));
			
			MenuItem itemCopy = new MenuItem()
			{
				Header = "_Copy",
				Command = ApplicationCommands.Copy
			};
			itemEdit.Items.Add(itemCopy);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Copy, CopyOnExecute, CutCanExecute));
			
			MenuItem itemPaste = new MenuItem()
			{
				Header = "_Paste",
				Command = ApplicationCommands.Paste
			};
			itemEdit.Items.Add(itemPaste);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));
			
			MenuItem itemDel = new MenuItem()
			{
				Header = "De_lete",
				Command = ApplicationCommands.Delete
			};
			itemEdit.Items.Add(itemDel);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Delete, DeleteOnExecute, CutCanExecute));
			
			itemEdit.Items.Add(new Separator());
			
			// commands Find, FindNext, Replace
			AddFindMenuItems(itemEdit);
			itemEdit.Items.Add(new Separator());
			
			// command Select All
			MenuItem itemAll = new MenuItem()
			{
				Header = "Select _All",
				Command = ApplicationCommands.SelectAll
			};
			itemEdit.Items.Add(itemAll);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.SelectAll, SelectAllOnExecute));
			
			// commands Time/Date
			InputGestureCollection col = new InputGestureCollection();
			col.Add(new KeyGesture(Key.F5));
			RoutedUICommand commTimeDate = new RoutedUICommand("Time/_Date", "TimeDate", GetType(), col);
			MenuItem itemDate = new MenuItem()
			{
				Command = commTimeDate
			};
			itemEdit.Items.Add(itemDate);
			CommandBindings.Add(
				new CommandBinding(commTimeDate, TimeDateOnExecute));
		}

		void UndoOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.Undo();
		}

		void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = txtbox.CanUndo;
		}

		void RedoOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.Redo();
		}

		void RedoCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = txtbox.CanRedo;
		}

		void CutOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.Cut();
		}

		void CutCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = txtbox.SelectedText.Length > 0;
		}

		void CopyOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.Copy();
		}

		void PasteOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.Paste();
		}

		void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = Clipboard.ContainsText();
		}

		void DeleteOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.SelectedText = "";
		}

		void SelectAllOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.SelectAll();
		}

		void TimeDateOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			txtbox.SelectedText = DateTime.Now.ToString();
		}
	}
}
