/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.05.2018
 * Time: 8:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of ReplaceDialog.
	/// </summary>
	class ReplaceDialog : FindReplaceDialog
	{
		public ReplaceDialog(Window owner) : base(owner)
		{
			Title = "Replace";
		}
	}
}
