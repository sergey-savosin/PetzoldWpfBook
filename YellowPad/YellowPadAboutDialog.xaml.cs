/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 08.06.2018
 * Time: 21:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Petzold.YellowPad
{
	/// <summary>
	/// Interaction logic for YellowPadAboutDialog.xaml
	/// </summary>
	public partial class YellowPadAboutDialog
	{
		public YellowPadAboutDialog()
		{
			InitializeComponent();
			
			// load Drawing
			Uri uri = new Uri("pack://application:,,,/Images/signature.xaml");
			Stream stream = Application.GetResourceStream(uri).Stream;
			Drawing drawing = (Drawing)XamlReader.Load(stream);
			stream.Close();
			imgSignature.Source = new DrawingImage(drawing);
		}
		
		public void LinkOnRequestNavigate(object Sender, RequestNavigateEventArgs args)
		{
			Process.Start(args.Uri.OriginalString);
			args.Handled = true;
		}
	}
}