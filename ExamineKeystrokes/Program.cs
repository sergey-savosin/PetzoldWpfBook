/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 09.03.2018
 * Time: 22:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ExamineKeystrokes
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class ExamineKeystrokes : Window
	{
		StackPanel stack;
		ScrollViewer scroll;
		
		string strHeader = "Event     Key      Sys-Key   Text " +
			"Ctrl-Text Sys-Text Ime KeyStates " +
			"IsDown IsUp IsToggled IsRepeat ";
		string strFormatKey = "{0,-10}{1,-9}{2,-10}" +
			"{3,-10}{4,-15}{5,-8}{6,-7}{7,-10}{8,-10}";
		string strFormatText = "{0,-10} " +
			"{1,-10}{2,-10}{3,-10}";
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run(new ExamineKeystrokes());
		}
		
		public ExamineKeystrokes()
		{
			Title = "Examine Keystrokes";
			FontFamily = new FontFamily("Courier New");
			
			Grid grid = new Grid();
			Content = grid;
			
			// one row = auto, other - for the rest space
			RowDefinition rowdef = new RowDefinition()
			{
				Height = GridLength.Auto
			};
			grid.RowDefinitions.Add(rowdef);
			grid.RowDefinitions.Add(new RowDefinition());
			
			// Title output
			TextBlock textHeader = new TextBlock()
			{
				FontWeight = FontWeights.Bold,
				Text = strHeader
			};
			grid.Children.Add(textHeader);
			
			// Create StackPanel via child ScrollViewer
			// for showing events
			scroll = new ScrollViewer();
			grid.Children.Add(scroll);
			Grid.SetRow(scroll, 1);
			stack = new StackPanel();
			scroll.Content = stack;
		}
		
		protected override void OnKeyDown(KeyEventArgs args)
		{
			base.OnKeyDown(args);
			DisplayKeyInfo(args);
		}
		
		protected override void OnKeyUp(KeyEventArgs args)
		{
			base.OnKeyUp(args);
			DisplayKeyInfo(args);
		}
		
		protected override void OnTextInput(TextCompositionEventArgs args)
		{
			base.OnTextInput(args);
			string str = String.Format(strFormatText, args.RoutedEvent.Name,
			                           args.Text,
			                           args.ControlText,
			                           args.SystemText);
			DisplayInfo(str);
		}
		
		void DisplayKeyInfo(KeyEventArgs args)
		{
			string str =
				String.Format(strFormatKey,
				              args.RoutedEvent.Name,
				              args.Key,
				              args.SystemKey,
				              args.ImeProcessedKey,
				              args.KeyStates,
				              args.IsDown,
				              args.IsUp,
				              args.IsToggled,
				              args.IsRepeat);
			DisplayInfo(str);
		}
		
		void DisplayInfo(string str)
		{
			TextBlock text = new TextBlock()
			{
				Text = str
			};
			stack.Children.Add(text);
			scroll.ScrollToBottom();
		}
		
	}
}
