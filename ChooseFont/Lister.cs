﻿/*
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

		void OnLoaded(object sender, RoutedEventArgs e)
		{
			ScrollIntoView();
		}
		
		public void Add(object obj)
		{
			list.Add(obj);
			TextBlock txtblk = new TextBlock()
			{
				Text = obj.ToString()
			};
			stack.Children.Add(txtblk);
		}
		
		public void Insert(int index, object obj)
		{
			list.Insert(index, obj);
			TextBlock txtblk = new TextBlock()
			{
				Text = obj.ToString()
			};
			stack.Children.Insert(index, txtblk);
		}
		
		public void Clear()
		{
			SelectedIndex = -1;
			stack.Children.Clear();
			list.Clear();
		}
		
		public bool Contains(object obj)
		{
			return list.Contains(obj);
		}
		
		public int Count
		{
			get { return list.Count; }
		}
		
		public void GoToLetter(char ch)
		{
			int offset = SelectedIndex + 1;
			for (int i = 0; i < Count; i++)
			{
				int index = (i + offset) % Count;
				if (Char.ToUpper(ch) == Char.ToUpper(list[index].ToString()[0]))
				{
					SelectedIndex = index;
					break;
				}
			}
		}
		
		// show selection line
		public int SelectedIndex
		{
			set
			{
				if (value < -1 || value >= Count)
					throw new ArgumentOutOfRangeException("SelectedIndex");
				if (value == indexSelected)
					return;
				if (indexSelected != -1)
				{
					TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
					txtblk.Background = SystemColors.WindowBrush;
					txtblk.Foreground = SystemColors.WindowTextBrush;
				}
				
				indexSelected = value;
				
				if (indexSelected > -1)
				{
					TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
					txtblk.Background = SystemColors.HighlightBrush;
					txtblk.Foreground = SystemColors.HighlightTextBrush;
				}
				
				ScrollIntoView();
				
				// run event SelectionChanged
				OnSelectionChanged(EventArgs.Empty);
			}
			
			get
			{
				return indexSelected;
			}
		}
		
		public object SelectedItem
		{
			set
			{
				SelectedIndex = list.IndexOf(value);
			}
			get
			{
				if (SelectedIndex > -1)
					return list[SelectedIndex];
				return null;
			}
		}
		
		// page scroll methods
		public void PageUp()
		{
			if (SelectedIndex == -1 || Count == 0)
				return;
			int index = SelectedIndex - (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);
			if (index < 0)
				index = 0;
			SelectedIndex = index;
		}
		
		public void PageDown()
		{
			if (SelectedIndex == -1 || Count == 0)
				return;
			int index = SelectedIndex + (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);
			if (index > Count - 1)
				index = Count - 1;
			SelectedIndex = index;
		}

		// private method for scrolling view to current line
		void ScrollIntoView()
		{
			if (Count == 0 || SelectedIndex == -1 || scroll.ViewportHeight > scroll.ExtentHeight)
				return;
			
			double heightPerItem = scroll.ExtentHeight / Count;
			double offsetItemTop = SelectedIndex * heightPerItem;
			double offsetItemBot = (SelectedIndex + 1) * heightPerItem;
			
			if (offsetItemTop < scroll.VerticalOffset)
				scroll.ScrollToVerticalOffset(offsetItemTop);
			else if (offsetItemBot > scroll.VerticalOffset + scroll.ViewportHeight)
				scroll.ScrollToVerticalOffset(offsetItemBot - scroll.ViewportHeight);
		}
		
		// Event handler
		void TextboxOnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
		{
			if (args.Source is TextBlock)
			{
				SelectedIndex = stack.Children.IndexOf(args.Source as TextBlock);
			}
		}
		
		protected virtual void OnSelectionChanged(EventArgs args)
		{
			if (SelectionChanged != null)
				SelectionChanged(this, args);
		}
	}
}
