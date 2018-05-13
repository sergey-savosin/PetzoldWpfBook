/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 12.05.2018
 * Time: 23:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of FindReplaceDialog.
	/// </summary>
	abstract class FindReplaceDialog : Window
	{
		// public fields
		public event EventHandler FindNext;
		public event EventHandler Replace;
		public event EventHandler ReplaceAll;
		
		// protected fields
		protected Label lblReplace;
		protected TextBox txtboxFind, txtboxReplace;
		protected CheckBox checkMatch;
		protected GroupBox groupDirection;
		protected RadioButton radioDown, radioUp;
		protected Button btnFind, btnReplace, btnAll;
		
		// public properties
		public string FindWhat
		{
			set { txtboxFind.Text = value;}
			get { return txtboxFind.Text;}
		}
		
		public string ReplaceWith
		{
			set { txtboxReplace.Text = value;}
			get { return txtboxReplace.Text;}
		}
		
		public bool MatchCase
		{
			set {checkMatch.IsChecked = value; }
			get {return (bool)checkMatch.IsChecked; }
		}
		
		public Direction Direction
		{
			set
			{
				if (value == Direction.Down)
					radioDown.IsChecked = true;
				else
					radioUp.IsChecked = true;
			}
			
			get
			{
				return (bool)radioDown.IsChecked ? Direction.Down : Direction.Up;
			}
		}
		
		// protected ctor
		protected FindReplaceDialog(Window owner)
		{
			ShowInTaskbar = false;
			WindowStyle = WindowStyle.ToolWindow;
			SizeToContent = SizeToContent.WidthAndHeight;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			Owner = owner;
			
			// create Grid
			Grid grid = new Grid();
			Content = grid;
			
			for (int i = 0; i<3; i++)
			{
				RowDefinition rowdef = new RowDefinition()
				{
					Height = GridLength.Auto
				};
				grid.RowDefinitions.Add(rowdef);
				ColumnDefinition coldef = new ColumnDefinition()
				{
					Width = GridLength.Auto
				};
				grid.ColumnDefinitions.Add(coldef);
			}
			
			// text to find
			Label lbl = new Label()
			{
				Content = "Fi_nd what: ",
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(12)
			};
			grid.Children.Add(lbl);
			Grid.SetRow(lbl, 0);
			Grid.SetColumn(lbl, 0);
			
			txtboxFind = new TextBox()
			{
				Margin = new Thickness(12)
			};
			txtboxFind.TextChanged += FindTextBoxOnTextChanged;
			grid.Children.Add(txtboxFind);
			Grid.SetRow(txtboxFind, 0);
			Grid.SetColumn(txtboxFind, 1);
			
			// text to replace with
			lblReplace = new Label()
			{
				Content = "Re_place with:",
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(12)
			};
			grid.Children.Add(lblReplace);
			Grid.SetRow(lblReplace, 1);
			Grid.SetColumn(lblReplace, 0);
			
			txtboxReplace = new TextBox()
			{
				Margin = new Thickness(12)
			};
			grid.Children.Add(txtboxReplace);
			Grid.SetRow(txtboxReplace, 1);
			Grid.SetColumn(txtboxReplace, 1);
			
			// MatchCase flag
			checkMatch = new CheckBox()
			{
				Content = "Match _case",
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(12)
			};
			grid.Children.Add(checkMatch);
			Grid.SetRow(checkMatch, 2);
			Grid.SetColumn(checkMatch, 0);
			
			// Search way
			groupDirection = new GroupBox()
			{
				Header = "Direction",
				Margin = new Thickness(12),
				HorizontalAlignment = HorizontalAlignment.Left
			};
			grid.Children.Add(groupDirection);
			Grid.SetRow(groupDirection, 2);
			Grid.SetColumn(groupDirection, 1);
			
			StackPanel stack = new StackPanel()
			{
				Orientation = Orientation.Horizontal
			};
			groupDirection.Content = stack;
			
			radioUp = new RadioButton()
			{
				Content = "_Up",
				Margin = new Thickness(6)
			};
			stack.Children.Add(radioUp);
			
			radioDown = new RadioButton()
			{
				Content = "_Down",
				Margin = new Thickness(6)
			};
			stack.Children.Add(radioDown);
			
			// StackPanel for buttons
			stack = new StackPanel()
			{
				Margin = new Thickness(6)
			};
			grid.Children.Add(stack);
			Grid.SetRow(stack, 0);
			Grid.SetColumn(stack, 2);
			Grid.SetRowSpan(stack, 3);
			
			// four buttons
			btnFind = new Button()
			{
				Content = "_Find Next",
				Margin = new Thickness(6),
				IsDefault = true
			};
			btnFind.Click += FindNextOnClick;
			stack.Children.Add(btnFind);
			
			btnReplace = new Button()
			{
				Content = "_Replace",
				Margin = new Thickness(6)
			};
			btnReplace.Click += ReplaceOnClick;
			stack.Children.Add(btnReplace);
			
			btnAll = new Button()
			{
				Content = "Replace _All",
				Margin = new Thickness(6)
			};
			btnAll.Click += ReplaceAllOnClick;
			stack.Children.Add(btnAll);
			
			Button btn = new Button()
			{
				Content = "Cancel",
				Margin = new Thickness(6),
				IsCancel = true
			};
			btn.Click += CancelOnClick;
			stack.Children.Add(btn);
			
			txtboxFind.Focus();
		}

		void FindTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox txtbox = e.Source as TextBox;
			btnFind.IsEnabled =
				btnReplace.IsEnabled =
				btnAll.IsEnabled = (txtbox.Text.Length > 0);
		}
		
		void FindNextOnClick(object sender, RoutedEventArgs e)
		{
			OnFindNext(new EventArgs());
		}
		
		protected virtual void OnFindNext(EventArgs args)
		{
			if (FindNext != null)
				FindNext(this, args);
		}

		void ReplaceOnClick(object sender, RoutedEventArgs e)
		{
			OnReplace(new EventArgs());
		}
		
		protected virtual void OnReplace(EventArgs args)
		{
			if (Replace != null)
				Replace(this, args);
		}

		void ReplaceAllOnClick(object sender, RoutedEventArgs e)
		{
			OnReplaceAll(new EventArgs());
		}
		
		protected virtual void OnReplaceAll(EventArgs args)
		{
			if (ReplaceAll != null)
				ReplaceAll(this, args);
		}

		void CancelOnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
