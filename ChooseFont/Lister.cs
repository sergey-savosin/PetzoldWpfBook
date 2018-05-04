/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 03.05.2018
 * Time: 9:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ChooseFont
{
	/// <summary>
	/// Description of Lister.
	/// </summary>
	class Lister : ContentControl
	{
		ScrollViewer scroll;
		StackPanel stack;
		ArrayList list = new ArrayList();
		int indexSelected = -1;
		
		// public event
		public event EventHandler SelectionChanged;
		
		// ctor
		public Lister()
		{
			Focusable = false;
			
			Border bord = new Border()
			{
				BorderThickness = new Thickness(1),
				BorderBrush = SystemColors.ActiveBorderBrush,
				Background = SystemColors.WindowBrush
			};
			Content = bord;
			
			scroll = new ScrollViewer()
			{
				Focusable = false,
				Padding = new Thickness(2, 0, 0, 0)
			};
			bord.Child = scroll;
			
			stack = new StackPanel();
			scroll.Content = stack;
			
			// left button handler
			AddHandler(
				TextBlock.MouseLeftButtonDownEvent,
				new MouseButtonEventHandler(TextboxOnMouseLeftButtonDown));
			
			Loaded += OnLoaded;
		}

		void TextboxOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			throw new NotImplementedException();
		}

		void OnLoaded(object sender, RoutedEventArgs e)
		{
			ScrollIntoView();
		}
		
		public void Add(object obj)
		{
			
		}

		void ScrollIntoView()
		{
			throw new NotImplementedException();
		}
	}
}
