/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 15.05.2018
 * Time: 22:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace Petzold.LoadXamlFil
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class LoadXamlFile : Window
	{
		Frame frame;
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run(new LoadXamlFile());
		}
		
		public LoadXamlFile()
		{
			Title = "Load Xaml File";
			
			DockPanel dock = new DockPanel();
			Content = dock;
			
			// button
			Button btn = new Button()
			{
				Content = "Open File...",
				Margin = new Thickness(12),
				HorizontalAlignment = HorizontalAlignment.Center
			};
			btn.Click += ButtonOnClick;
			dock.Children.Add(btn);
			DockPanel.SetDock(btn, Dock.Top);
			
			// Frame
			frame = new Frame();
			dock.Children.Add(frame);
		}

		void ButtonOnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog()
			{
				Filter = "XAML files (*.xaml)|*.xaml|All files(*.*)|*.*"
			};
			if ((bool)dlg.ShowDialog())
			{
				try
				{
					// read file
					using (XmlTextReader xmlreader = new XmlTextReader(dlg.FileName))
					{
						object obj = XamlReader.Load(xmlreader);
						
						if (obj is Window)
						{
							Window win = obj as Window;
							win.Owner = this;
							win.Show();
						}
						else
						{
							frame.Content = obj;
						}
						
					}
					
				}
				catch (Exception exc)
				{
					MessageBox.Show(exc.Message, Title);
				}
				finally
				{
					
				}
			}
		}
	}
}
