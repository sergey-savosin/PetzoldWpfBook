/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 07/04/2018
 * Time: 22:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace Petzold.DumpControlTemplate
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Control ctrl;
		
		public MainWindow()
		{
			InitializeComponent();
		}
		
		void ControlItemOnClick(object sender, RoutedEventArgs e)
		{
			// delete all objects from 1 row
			for (int i=0; i<grid.Children.Count; i++)
			{
				if (Grid.GetRow(grid.Children[i]) == 0)
				{
					grid.Children.Remove(grid.Children[i]);
					break;
				}
			}
			
			txtbox.Text = "";
			
			// get class
			MenuItem item = e.Source as MenuItem;
			
			Type typ = null;
			// try to create
			try
			{
				typ = (Type)item.Tag;
				ConstructorInfo info = typ.GetConstructor(System.Type.EmptyTypes);
				ctrl = (Control)info.Invoke(null);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message, Title);
				return;
			}
			
			// try to set control inside grid
			try
			{
				grid.Children.Add(ctrl);
			}
			catch 
			{
				if (ctrl is Window)
					(ctrl as Window).Show();
				else
					return;
			}
			
			Title = Title.Remove(Title.IndexOf('-'));
			if (typ != null)
				Title += "- " + typ.Name;
			else
				Title += "- " + "no control";
		}
		
		// clear menu blocking
		void DumpOnOpened(object sender, RoutedEventArgs e)
		{
			itemTemplate.IsEnabled = ctrl != null;
			itemItemsPanel.IsEnabled = ctrl != null && ctrl is ItemsControl;
		}
		
		// output Template object
		void DumpTemplateOnClick(object sender, RoutedEventArgs e)
		{
			if (ctrl != null)
				Dump(ctrl.Template);
		}
		
		// output ItemsPanelTemplate
		void DumpItemsPanelOnClick(object sender, RoutedEventArgs e)
		{
			if (ctrl != null && ctrl is ItemsControl)
				Dump((ctrl as ItemsControl).ItemsPanel);
		}
		
		// template output
		void Dump(FrameworkTemplate template)
		{
			if (template != null)
			{
				// output XAML in TextBox
				XmlWriterSettings settings = new XmlWriterSettings()
				{
					Indent = true,
					IndentChars = new string(' ', 4),
					NewLineOnAttributes = true
				};
				StringBuilder strbuild = new StringBuilder();
				XmlWriter xmlwrite = XmlWriter.Create(strbuild, settings);
				try
				{
					XamlWriter.Save(template, xmlwrite);
					txtbox.Text = strbuild.ToString();
				}
				catch (Exception exc)
				{
					txtbox.Text = exc.Message;
				}
			}
			else
				txtbox.Text = "no template";
		}
	}
}