/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 20.05.2018
 * Time: 23:21
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

namespace Petzold.XamlCruncher
{
	/// <summary>
	/// Interaction logic for XamlTabSpacesDialog.xaml
	/// </summary>
	public partial class XamlTabSpacesDialog : Window
	{
		public XamlTabSpacesDialog()
		{
			InitializeComponent();
			txtbox.Focus();
		}
		
		public int TabSpaces
		{
			set {txtbox.Text = value.ToString(); }
			get {return Int32.Parse(txtbox.Text); }
		}
		
		void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
		{
			int result;
			btnOk.IsEnabled = (Int32.TryParse(txtbox.Text, out result) &&
			                   result > 0 &&
			                   result < 11);
		}
		
		void OkOnClick(object sender, RoutedEventArgs args)
		{
			DialogResult = true;
		}
	}
}