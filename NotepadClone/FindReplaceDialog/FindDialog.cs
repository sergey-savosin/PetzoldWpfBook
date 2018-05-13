/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.05.2018
 * Time: 8:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of FindDialog.
	/// </summary>
	class FindDialog : FindReplaceDialog
	{
		public FindDialog(Window owner) : base (owner)
		{
			Title = "Find";
			
			// hide useless buttons
			lblReplace.Visibility = Visibility.Collapsed;
			txtboxReplace.Visibility = Visibility.Collapsed;
			btnReplace.Visibility = Visibility.Collapsed;
			btnAll.Visibility = Visibility.Collapsed;
		}
	}
}
