/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 07.05.2018
 * Time: 23:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public partial class NotepadClone : Window
	{
		protected string strAppTitle; // program Title
		protected string strAppData; // full path to the settings
		protected NotepadCloneSettings settings; // settings
		protected bool isFileDirty = false; // flag of file content changed
		
		// main window elements
		protected Menu menu;
		protected TextBox txtbox;
		protected StatusBar status;
		string strLoadedFile; // full name of loaded file
		StatusBarItem statLineCol; // line and column
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application app = new Application();
			app.ShutdownMode = ShutdownMode.OnMainWindowClose;
			app.Run(new NotepadClone());
		}
		
		public NotepadClone()
		{
			//Title = "Notepad Clone";
			
			Assembly asmbly = Assembly.GetExecutingAssembly();
			
			// get AssemblyTitle attribute
			AssemblyTitleAttribute title =
				(AssemblyTitleAttribute)asmbly
				.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
			strAppTitle = title.Title;
			
			// get AssemblyProduct
			AssemblyProductAttribute product =
				(AssemblyProductAttribute)asmbly
				.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
			strAppData = Path.Combine(
				Environment.GetFolderPath(
					Environment.SpecialFolder.LocalApplicationData),
				"Petzold\\" + product.Product + "\\" + product.Product + ".Settings.xml");
			
			DockPanel dock = new DockPanel();
			Content = dock;
			
			// main menu
			menu = new Menu();
			dock.Children.Add(menu);
			DockPanel.SetDock(menu, Dock.Top);
			
			//status bar
			status = new StatusBar();
			dock.Children.Add(status);
			DockPanel.SetDock(status, Dock.Bottom);
			
			statLineCol = new StatusBarItem()
			{
				HorizontalAlignment = HorizontalAlignment.Center
			};
			status.Items.Add(statLineCol);
			DockPanel.SetDock(statLineCol, Dock.Right);
			
			// textbox
			txtbox = new TextBox()
			{
				AcceptsReturn = true,
				AcceptsTab = true,
				VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
				HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
			};
			txtbox.TextChanged += TextBoxOnTextChanged;
			txtbox.SelectionChanged += TextBoxOnSelectionChanged;
			dock.Children.Add(txtbox);
			
			// Create menu commands
			AddFileMenu(menu);
			AddEditMenu(menu);
			AddFormatMenu(menu);
//			AddViewMenu(menu);
//			AddHelpMenu(menu);
			
			// load settings
			settings = (NotepadCloneSettings)LoadSettings();
			
			// apply settins
			WindowState = settings.WindowState;
			
			if (settings.RestoreBounds != Rect.Empty)
			{
				Left = settings.RestoreBounds.Left;
				Top = settings.RestoreBounds.Top;
				Width = settings.RestoreBounds.Width;
				Height = settings.RestoreBounds.Height;
			}
			
			txtbox.TextWrapping = settings.TextWrapping;
			txtbox.FontFamily = new FontFamily(settings.FontFamily);
			txtbox.FontStyle = (FontStyle) new FontStyleConverter().ConvertFromString(settings.FontStyle);
			txtbox.FontWeight = (FontWeight) new FontWeightConverter().ConvertFromString(settings.FontWeight);
			txtbox.FontStretch = (FontStretch) new FontStretchConverter().ConvertFromString(settings.FontStretch);
			txtbox.FontSize = settings.FontSize;
			
			// Load handler
			Loaded += WindowOnLoaded;
			
			// set focus
			txtbox.Focus();
		}
		
		// virtual method for loading settings
		protected virtual object LoadSettings()
		{
			return NotepadCloneSettings.Load(typeof(NotepadCloneSettings), strAppData);
		}
		
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			e.Cancel = !OkToTrash();
			settings.RestoreBounds = RestoreBounds;
		}
		
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			settings.WindowState = WindowState;
			settings.TextWrapping = txtbox.TextWrapping;
			settings.FontFamily = txtbox.FontFamily.ToString();
			settings.FontStyle = new FontStyleConverter().ConvertToString(txtbox.FontStyle);
			settings.FontWeight = new FontWeightConverter().ConvertToString(txtbox.FontWeight);
			settings.FontStretch = new FontStretchConverter().ConvertToString(txtbox.FontStretch);
			SaveSettings();
		}
		
		protected virtual void SaveSettings()
		{
			settings.Save(strAppData);
		}
		
		// shows filename or "Untitled"
		protected void UpdateTitle()
		{
			if (strLoadedFile == null)
				Title = "Untitled - " + strAppTitle;
			else
				Title = Path.GetFileName(strLoadedFile) + " - " + strAppTitle;
		}
		
		void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			isFileDirty = true;
		}

		void TextBoxOnSelectionChanged(object sender, RoutedEventArgs e)
		{
			int iChar = txtbox.SelectionStart;
			int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);
			
			// validate for errors
			if (iLine == -1)
			{
				statLineCol.Content = "";
				return;
			}
			
			int iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
			string str = String.Format("Line {0} Col {1}", iLine + 1, iCol + 1);
			
			if (txtbox.SelectionLength > 0)
			{
				iChar += txtbox.SelectionLength;
				iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);
				iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
				str += String.Format(" - Line {0} Col {1}", iLine + 1, iCol + 1);
			}
			
			statLineCol.Content = str;
		}

		void WindowOnLoaded(object sender, RoutedEventArgs e)
		{
			ApplicationCommands.New.Execute(null, this);
			
			// get commandline params
			string[] strArgs = Environment.GetCommandLineArgs();
			if (strArgs.Length > 1) // first arg = program name!
			{
				if (File.Exists(strArgs[1]))
				{
					LoadFile(strArgs[1]);
				}
				else
				{
					MessageBoxResult result =
						MessageBox.Show("Cannot find the " +
						                Path.GetFileName(strArgs[1]) +
						                " file.\r\n\r\n" +
						                "Do you want to create a new file?",
						                strAppTitle,
						                MessageBoxButton.YesNoCancel,
						                MessageBoxImage.Question);
					
					// Cancel button - close the window
					if (result == MessageBoxResult.Cancel)
						Close();
					
					// Yes button - create the file and close it
					else if (result == MessageBoxResult.Yes)
					{
						try
						{
							File.Create(strLoadedFile = strArgs[1]).Close();
						}
						catch (Exception exc)
						{
							MessageBox.Show("Error on File Creation: "+
							                exc.Message,
							                strAppTitle,
							                MessageBoxButton.OK,
							                MessageBoxImage.Asterisk);
							return;
						}
						UpdateTitle();
					}
					
					// No button - nothing to do
				}
			}
		}
	}
}
