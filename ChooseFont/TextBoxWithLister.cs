/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 05.05.2018
 * Time: 9:53
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
	/// Description of TextBoxWithLister.
	/// </summary>
	class TextBoxWithLister : ContentControl
	{
		TextBox txtbox;
		Lister lister;
		bool isReadOnly;
		
		// public events
		public event EventHandler SelectionChanged;
		public event TextChangedEventHandler TextChanged;
		
		public TextBoxWithLister()
		{
			DockPanel dock = new DockPanel();
			Content = dock;
			
			// TextBox at Up
			txtbox = new TextBox();
			txtbox.TextChanged += TextBoxOnTextChanged;
			dock.Children.Add(txtbox);
			DockPanel.SetDock(txtbox, Dock.Top);
			
			// Lister in the rest place
			lister = new Lister();
			lister.SelectionChanged += ListerOnSelectionChanged;
			dock.Children.Add(lister);
		}
		
		public string Text
		{
			get { return txtbox.Text; }
			set { txtbox.Text = value; }
		}
		
		public bool IsReadOnly
		{
			get { return isReadOnly; }
			set { isReadOnly = value; }
		}
		
		public object SelectedItem
		{
			get
			{
				return lister.SelectedItem;
			}
			set
			{
				lister.SelectedItem = value;
				if (lister.SelectedItem != null)
					txtbox.Text = lister.SelectedItem.ToString();
				else
					txtbox.Text = "";
			}
		}
		
		public int SelectedIndex
		{
			get
			{
				return lister.SelectedIndex;
			}
			set
			{
				lister.SelectedIndex = value;
				if (lister.SelectedIndex == -1)
					txtbox.Text = "";
				else
					txtbox.Text = lister.SelectedItem.ToString();
			}
		}
		
		public void Add(object obj)
		{
			lister.Add(obj);
		}
		
		public void Insert(int index, object obj)
		{
			lister.Insert(index, obj);
		}
		
		public void Clear()
		{
			lister.Clear();
		}
		
		public bool Contains(object obj)
		{
			return lister.Contains(obj);
		}
		
		// set focus on Mouse Click
		protected override void OnMouseDown(MouseButtonEventArgs args)
		{
			base.OnMouseDown(args);
			Focus();
		}
		
		// focus goes to TextBox
		protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
			base.OnGotKeyboardFocus(e);
			if (e.NewFocus == this)
			{
				txtbox.Focus();
				if (SelectedIndex == -1 && lister.Count > 0)
					SelectedIndex = 0;
			}
		}
		
		// press alphanumber keys goes to GoToLetter
		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			base.OnPreviewTextInput(e);
			if (IsReadOnly)
			{
				lister.GoToLetter(e.Text[0]);
				e.Handled = true;
			}
		}
		
		// cursor keys
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			if (SelectedIndex == -1)
				return;
			switch (e.Key)
			{
				case Key.Home:
					if (lister.Count > 0)
						SelectedIndex = 0;
					break;
					
				case Key.End:
					if (lister.Count > 0)
						SelectedIndex = lister.Count - 1;
					break;
					
				case Key.Up:
					if (SelectedIndex > 0)
						SelectedIndex --;
					break;
					
				case Key.Down:
					if (SelectedIndex < lister.Count - 1)
						SelectedIndex ++;
					break;
					
				case Key.PageUp:
					lister.PageUp();
					break;
					
				case Key.PageDown:
					lister.PageDown();
					break;
					
				default:
					return;
			}
			e.Handled = true;
		}

		void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextChanged != null)
				TextChanged(this, e);
		}

		void ListerOnSelectionChanged(object sender, EventArgs e)
		{
			if (SelectedIndex == -1)
				txtbox.Text = "";
			else
				txtbox.Text = lister.SelectedItem.ToString();
			
			OnSelectionChanged(e);
		}

		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (SelectionChanged != null)
				SelectionChanged(this, e);
		}
	}
}
