/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 17.05.2018
 * Time: 22:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Xml;

namespace Petzold.XamlCruncher
{
	/// <summary>
	/// Description of XamlCruncher.
	/// </summary>
	class XamlCruncher : Petzold.NotepadClone.NotepadClone
	{
		Frame frameParent; // for XAML object
		Window win; // for XAML window
		StatusBarItem statusParse; // error messages
		int tabspaces = 4;
		
		XamlCruncherSettings settingsXaml;
		
		XamlOrientationMenuItem itemOrientation;
		bool isSuspendParsing = false;
		
		[STAThread]
		public new static void Main()
		{
			Application app = new Application();
			app.ShutdownMode = ShutdownMode.OnMainWindowClose;
			app.Run(new XamlCruncher());
		}
		
		public bool IsSuspendParsing
		{
			set { isSuspendParsing = value; }
			get { return isSuspendParsing; }
		}
		
		public XamlCruncher()
		{
			strFilter = "XAML files (*.xaml)|*.xaml|All files (*.*)|*.*";
			
			// find dockpanel and delete textbox inside it
			DockPanel dock = txtbox.Parent as DockPanel;
			dock.Children.Remove(txtbox);
			
			// create Grid panel 3x3
			Grid grid = new Grid();
			dock.Children.Add(grid);
			for (int i=0; i<3; i++)
			{
				RowDefinition rowdef = new RowDefinition()
				{
					Height = new GridLength(0)
				};
				grid.RowDefinitions.Add(rowdef);
				
				ColumnDefinition coldef = new ColumnDefinition()
				{
					Width = new GridLength(0)
				};
				grid.ColumnDefinitions.Add(coldef);
			}
			
			grid.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
			grid.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
			
			// two splitters
			GridSplitter split = new GridSplitter()
			{
				HorizontalAlignment = HorizontalAlignment.Stretch,
				VerticalAlignment = VerticalAlignment.Center,
				Height = 6
			};
			grid.Children.Add(split);
			Grid.SetRow(split, 1);
			Grid.SetColumn(split, 0);
			Grid.SetColumnSpan(split, 3);
			
			split = new GridSplitter()
			{
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Stretch,
				Height = 6
			};
			grid.Children.Add(split);
			Grid.SetRow(split, 0);
			Grid.SetColumn(split, 1);
			Grid.SetColumnSpan(split, 3);
			
			// frame for showing XAML
			frameParent = new Frame()
			{
				NavigationUIVisibility = NavigationUIVisibility.Hidden
			};
			grid.Children.Add(frameParent);
			
			// textbox
			txtbox.TextChanged += TextboxOnTextChanged;
			grid.Children.Add(txtbox);
			
			// move settings to XamlCruncherSettings
			settingsXaml = (XamlCruncherSettings)settings;
			
			MenuItem itemXaml = new MenuItem()
			{
				Header = "_Xaml"
			};
			menu.Items.Insert(menu.Items.Count - 1, itemXaml);
			
			// create XamlOrientationMenuItem
			itemOrientation = new XamlOrientationMenuItem(grid, txtbox, frameParent);
			itemOrientation.Orientation = settingsXaml.Orientation;
			itemXaml.Items.Add(itemOrientation);
			
			// menuItem for tabs
			MenuItem itemTabs = new MenuItem()
			{
				Header = "_Tab spaces..."
			};
			itemTabs.Click += TabSpacesOnClick;
			itemXaml.Items.Add(itemTabs);
			
			// menuItem for stopping parsing
			MenuItem itemNoParse = new MenuItem()
			{
				Header = "_Suspend Parsing",
				IsChecked = true
			};
			itemNoParse.SetBinding(MenuItem.IsCheckedProperty, "IsSuspendParsing");
			itemNoParse.DataContext = this;
			itemXaml.Items.Add(itemNoParse);
			
			// command for continue parsing
			InputGestureCollection collGest = new InputGestureCollection();
			collGest.Add(new KeyGesture(Key.F6));
			RoutedUICommand commReparse = new RoutedUICommand(
				"_Reparse",
				"Reparse",
			    GetType(),
			    collGest);
			
			// menuItem for reparse
			MenuItem itemReparse = new MenuItem()
			{
				Command = commReparse
			};
			itemXaml.Items.Add(itemReparse);
			
			CommandBindings.Add(new CommandBinding(commReparse, ReparseOnExecuted));
			
			// command for show window
			collGest = new InputGestureCollection();
			collGest.Add(new KeyGesture(Key.F7));
			RoutedUICommand commShowWin = new RoutedUICommand(
				"Show _Window",
				"ShowWindow",
				GetType(),
				collGest);
			
			// menuItem for showing window
			MenuItem itemShowWin = new MenuItem()
			{
				Command = commShowWin
			};
			itemXaml.Items.Add(itemShowWin);
			CommandBindings.Add(new CommandBinding(
				commShowWin,
				ShowWindowOnExecuted,
				ShowWindowCanExecute));
			
			// menuItem for saving current content to a new document
			MenuItem itemTemplate = new MenuItem()
			{
				Header = "Save as Startup _Document"
			};
			itemTemplate.Click += NewStartupOnClick;
			itemXaml.Items.Add(itemTemplate);
			
			// help menuItem
			MenuItem itemXamlHelp = new MenuItem()
			{
				Header = "_Help..."
			};
			itemXamlHelp.Click += HelpOnClick;
			MenuItem itemHelp = (MenuItem)menu.Items[menu.Items.Count - 1];
			itemHelp.Items.Insert(0, itemXamlHelp);
			
			// new StatusBar
			statusParse = new StatusBarItem();
			status.Items.Insert(0, statusParse);
			status.Visibility = Visibility.Visible;
			
			// handler for exceptions. Comment this line when experimentating!
			Dispatcher.UnhandledException += DispatcherOnUnhandledException;
		}
		
		protected override void NewOnExecute(object sender, ExecutedRoutedEventArgs e)
		{
			base.NewOnExecute(sender, e);
			
			string str = ((XamlCruncherSettings)settings).StartupDocument;
			
			str = str.Replace("\r\n", "\n");
			str = str.Replace("\n", "\r\n");
			txtbox.Text = str;
			isFileDirty = false;
		}
		
		protected override object LoadSettings()
		{
			return XamlCruncherSettings.Load(typeof(XamlCruncherSettings), strAppData);
		}
		
		protected override void OnClosed(EventArgs e)
		{
			settingsXaml.Orientation = itemOrientation.Orientation;
			base.OnClosed(e);
		}
		
		protected override void SaveSettings()
		{
			((XamlCruncherSettings)settings).Save(strAppData);
		}

		void TextboxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (isSuspendParsing)
				txtbox.Foreground = SystemColors.WindowTextBrush;
			else
				Parse();
		}

		void TabSpacesOnClick(object sender, RoutedEventArgs e)
		{
			XamlTabSpacesDialog dlg = new XamlTabSpacesDialog()
			{
				Owner = this,
				TabSpaces = settingsXaml.TabSpaces
			};
			if ((bool)dlg.ShowDialog().GetValueOrDefault())
			{
				settingsXaml.TabSpaces = dlg.TabSpaces;
			}
		}

		void ReparseOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			Parse();
		}

		void ShowWindowOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (win != null)
				win.Show();
		}

		void ShowWindowCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (win != null);
		}

		void NewStartupOnClick(object sender, RoutedEventArgs e)
		{
			((XamlCruncherSettings)settings).StartupDocument = txtbox.Text;
		}

		void HelpOnClick(object sender, RoutedEventArgs e)
		{
			Uri uri = new Uri("pack://application:,,,/XamlCruncherHelp.txt");
			Stream stream = Application.GetResourceStream(uri).Stream;
			Window win = new Window()
			{
				Title = "XAML Cruncher help",
				Content = XamlReader.Load(stream)
			};
			win.Show();
		}
		
		// replaces tab with spaces
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			
			if (e.Source == txtbox && e.Key == Key.Tab)
			{
				string strInsert = new string(' ', tabspaces);
				int iChar = txtbox.SelectionStart;
				int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);
				if (iLine != -1)
				{
					int iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
					strInsert = new string (' ',
						settingsXaml.TabSpaces - iCol % settingsXaml.TabSpaces);
				}
				txtbox.SelectedText = strInsert;
				txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
				e.Handled = true;
			}
		}
		
		// general method Parse
		void Parse()
		{
			StringReader strreader = new StringReader(txtbox.Text);
			XmlTextReader xmlreader = new XmlTextReader(strreader);
			try
			{
				object obj = XamlReader.Load(xmlreader);
				txtbox.Foreground = SystemColors.WindowTextBrush;
				if (obj is Window)
				{
					win = obj as Window;
					statusParse.Content = "Press F7 to display Window";
				}
				else
				{
					win = null;
					frameParent.Content = obj;
					statusParse.Content = "OK";
				}
			}
			catch (Exception exc)
			{
				txtbox.Foreground = Brushes.Red;
				statusParse.Content = exc.Message;
			}
		}

		void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			statusParse.Content = "Unhandled Exception: " + e.Exception.Message;
			e.Handled = true;
		}
	}
}
