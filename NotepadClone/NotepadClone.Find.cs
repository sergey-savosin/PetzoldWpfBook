/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.05.2018
 * Time: 8:13
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
	/// Description of NotepadClone_Find.
	/// </summary>
	public partial class NotepadClone
	{
		string strFindWhat = "", strReplaceWith = "";
		StringComparison strcomp = StringComparison.OrdinalIgnoreCase;
		Direction dirFind = Direction.Down;
		
		void AddFindMenuItems(MenuItem itemEdit)
		{
			// Find command
			MenuItem itemFind = new MenuItem()
			{
				Header = "_Find...",
				Command = ApplicationCommands.Find
			};
			itemEdit.Items.Add(itemFind);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Find, FindOnExecute, FindCanExecute));
			
			// FindNext
			InputGestureCollection coll = new InputGestureCollection();
			coll.Add(new KeyGesture(Key.F3));
			RoutedUICommand commFindNext =
				new RoutedUICommand("Find _Next", "FindNext", GetType(), coll);
			
			MenuItem itemNext = new MenuItem()
			{
				Command = commFindNext
			};
			itemEdit.Items.Add(itemNext);
			CommandBindings.Add(
				new CommandBinding(
					commFindNext, FindNextOnExecute, FindNextCanExecute));
			
			// Replace command
			MenuItem itemReplace = new MenuItem()
			{
				Header = "_Replace",
				Command = ApplicationCommands.Replace
			};
			itemEdit.Items.Add(itemReplace);
			CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Replace, ReplaceOnExecute, FindCanExecute));
		}

		void FindOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			FindDialog dlg = new FindDialog(this)
			{
				FindWhat = strFindWhat,
				MatchCase = strcomp == StringComparison.Ordinal,
				Direction = dirFind
			};
			
			dlg.FindNext += FindDialogOnFindNext;
			dlg.Show();
		}

		void FindCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (txtbox.Text.Length > 0 &&
			                OwnedWindows.Count == 0);
		}

		void FindNextOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			if (strFindWhat == null || strFindWhat.Length == 0)
				FindOnExecute(sender, e);
			else
				FindNext();
		}

		void FindNextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (txtbox.Text.Length > 0 &&
			                strFindWhat.Length > 0);
		}

		void ReplaceOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			ReplaceDialog dlg = new ReplaceDialog(this)
			{
				FindWhat = strFindWhat,
				ReplaceWith = strReplaceWith,
				MatchCase = strcomp == StringComparison.Ordinal,
				Direction = dirFind
			};
			
			dlg.FindNext += FindDialogOnFindNext;
			dlg.Replace += ReplaceDialogOnReplace;
			dlg.ReplaceAll += ReplaceDialogOnReplaceAll;
			
			dlg.Show();
		}

		void FindDialogOnFindNext(object sender, EventArgs e)
		{
			FindReplaceDialog dlg = sender as FindReplaceDialog;
			
			strFindWhat = dlg.FindWhat;
			strcomp = dlg.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			dirFind = dlg.Direction;
			
			FindNext();
		}

		// common method FindNext
		void FindNext()
		{
			int indexStart, indexFind;
			
			if (dirFind == Direction.Down)
			{
				indexStart = txtbox.SelectionStart + txtbox.SelectionLength;
				indexFind = txtbox.Text.IndexOf(strFindWhat, indexStart, strcomp);
			}
			else
			{
				indexStart = txtbox.SelectionStart;
				indexFind = txtbox.Text.LastIndexOf(strFindWhat, indexStart, strcomp);
			}
			
			// select founded text
			if (indexFind != -1)
			{
				txtbox.Select(indexFind, strFindWhat.Length);
				txtbox.Focus();
			}
			else
				MessageBox.Show("Cannot find \"" + strFindWhat + "\"",
				                Title,
				                MessageBoxButton.OK,
				                MessageBoxImage.Information);
		}

		void ReplaceDialogOnReplace(object sender, EventArgs e)
		{
			ReplaceDialog dlg = sender as ReplaceDialog;
			
			strFindWhat = dlg.FindWhat;
			strReplaceWith = dlg.ReplaceWith;
			strcomp = dlg.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			if (strFindWhat.Equals(txtbox.SelectedText, strcomp))
				txtbox.SelectedText = strReplaceWith;
			
			FindNext();
		}

		void ReplaceDialogOnReplaceAll(object sender, EventArgs e)
		{
			ReplaceDialog dlg = sender as ReplaceDialog;
			string str = txtbox.Text;
			strFindWhat = dlg.FindWhat;
			strReplaceWith = dlg.ReplaceWith;
			strcomp = dlg.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			int index = 0;
			
			while (index + strFindWhat.Length < str.Length)
			{
				index = str.IndexOf(strFindWhat, index, strcomp);
				
				if (index != -1)
				{
					str = str.Remove(index, strFindWhat.Length);
					str = str.Insert(index, strReplaceWith);
					index += strReplaceWith.Length;
				}
				else
					break;
			}
			
			txtbox.Text = str;
		}
	}
}
