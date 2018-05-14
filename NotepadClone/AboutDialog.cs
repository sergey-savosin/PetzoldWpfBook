/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 14.05.2018
 * Time: 22:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of AboutDialog.
	/// </summary>
	class AboutDialog : Window
	{
		public AboutDialog(Window owner)
		{
			Assembly asmbly = Assembly.GetExecutingAssembly();
			
			AssemblyTitleAttribute title =
				(AssemblyTitleAttribute)asmbly.GetCustomAttributes(
					typeof(AssemblyTitleAttribute), false)[0];
			string strTitle = title.Title;
			
			AssemblyFileVersionAttribute version =
				(AssemblyFileVersionAttribute)asmbly.GetCustomAttributes(
					typeof(AssemblyFileVersionAttribute), false)[0];
			string strVersion = version.Version.Substring(0, 3);
			
			AssemblyCopyrightAttribute copy =
				(AssemblyCopyrightAttribute)asmbly.GetCustomAttributes(
					typeof(AssemblyCopyrightAttribute), false)[0];
			string strCopyright = copy.Copyright;
			
			Title = "About " + strTitle;
			ShowInTaskbar = false;
			SizeToContent = SizeToContent.WidthAndHeight;
			ResizeMode = ResizeMode.NoResize;
			Left = owner.Left + 96;
			Top = owner.Top + 96;
			
			StackPanel stackMain = new StackPanel();
			Content = stackMain;
			
			TextBlock txtblk = new TextBlock()
			{
				Text = strTitle + " Version " + strVersion,
				FontFamily = new FontFamily("Times New Roman"),
				FontSize = 32, // 24 pt
				FontStyle = FontStyles.Italic,
				Margin = new Thickness(24),
				HorizontalAlignment = HorizontalAlignment.Center
			};
			stackMain.Children.Add(txtblk);
			
			txtblk = new TextBlock()
			{
				Text = strCopyright,
				FontSize = 20, // 15 pt
				HorizontalAlignment = HorizontalAlignment.Center
			};
			stackMain.Children.Add(txtblk);
			
			Run run = new Run("www.charlespetzold.com");
			Hyperlink link = new Hyperlink(run);
			link.Click += LinkOnClick;
			txtblk = new TextBlock(link)
			{
				FontSize = 20,
				HorizontalAlignment = HorizontalAlignment.Center
			};
			stackMain.Children.Add(txtblk);
			
			Button btn = new Button()
			{
				Content = "OK",
				IsDefault = true,
				IsCancel = true,
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(24)
			};
			btn.Click += OkOnClick;
			stackMain.Children.Add(btn);
			
			btn.Focus();
		}

		void LinkOnClick(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.charlespetzold.com");
		}

		void OkOnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
