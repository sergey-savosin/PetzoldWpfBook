/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 05.06.2018
 * Time: 22:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32;

namespace Petzold.YellowPad
{
	public partial class YellowPadWindow: Window
	{
		void StylusModeOnOpened(object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			foreach (MenuItem child in item.Items)
			{
				child.IsChecked = inkcanv.EditingMode == (InkCanvasEditingMode)child.Tag;
			}
		}
		
		void StylusModeOnClick(object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			inkcanv.EditingMode = (InkCanvasEditingMode)item.Tag;
		}
		
		void EraserModeOnOpened(object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			foreach (MenuItem child in item.Items)
			{
				child.IsChecked = inkcanv.EditingModeInverted == (InkCanvasEditingMode)child.Tag;
			}
		}
		
		void EraserModeOnClick(object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			inkcanv.EditingModeInverted = (InkCanvasEditingMode)item.Tag;
		}


	}
}
