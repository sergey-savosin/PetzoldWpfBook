/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 17.05.2018
 * Time: 22:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.XamlCruncher
{
	/// <summary>
	/// Description of XamlCruncherSettings.
	/// </summary>
	public class XamlCruncherSettings : Petzold.NotepadClone.NotepadCloneSettings
	{
		public Dock Orientation = Dock.Left;
		public int TabSpaces = 4;
		public string StartupDocument =
			"<Button xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n" +
			" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n"+
			" Hello, XAML!\r\n" +
			"</Button>\r\n";
		
		public XamlCruncherSettings()
		{
			FontFamily = "Lucida Console";
			FontSize = 10 / 0.75;
		}
	}
}
