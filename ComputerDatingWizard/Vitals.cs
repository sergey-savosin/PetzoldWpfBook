/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 13.06.2018
 * Time: 22:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ComputerDatingWizard
{
	/// <summary>
	/// Description of Vitals.
	/// </summary>
	public class Vitals
	{
		public string Name;
		public string Home;
		public string Gender;
		public string FavoriteOS;
		public string Directory;
		public string MomsMaidenName;
		public string Pet;
		public string Income;
		public static RadioButton GetCheckedRadioButton(GroupBox grpbox)
		{
			Panel pnl = grpbox.Content as Panel;
			if (pnl != null)
			{
				foreach (UIElement el in pnl.Children)
				{
					RadioButton radio = el as RadioButton;
					if (radio != null && (bool)radio.IsChecked)
						return radio;
				}
			}
			return null;
		}
	}
}
