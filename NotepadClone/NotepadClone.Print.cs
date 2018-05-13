/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 09.05.2018
 * Time: 22:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of NotepadClone_Print.
	/// </summary>
	public partial class NotepadClone : Window
	{
		// fields for printing
		PrintQueue prnqueue;
		PrintTicket prntkt;
		Thickness marginPage = new Thickness(96);
		
		void AddPrintMenuItems(MenuItem itemFile)
		{
			// Page Setup command
			MenuItem itemSetup = new MenuItem()
			{
				Header = "Page Set_up..."
			};
			itemSetup.Click += PageSetupOnClick;
			itemFile.Items.Add(itemSetup);
			
			// Print command
			MenuItem itemPrint = new MenuItem()
			{
				Header = "_Print...",
				Command = ApplicationCommands.Print
			};
			itemFile.Items.Add(itemPrint);
			CommandBindings.Add(
				new CommandBinding(ApplicationCommands.Print, PrintOnExecuted));
			
		}

		void PageSetupOnClick(object sender, RoutedEventArgs e)
		{
			PageMarginsDialog dlg = new PageMarginsDialog()
			{
				Owner = this,
				PageMargins = marginPage
			};
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				// Save page margins
				marginPage = dlg.PageMargins;
			}
		}

		void PrintOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			PrintDialog dlg = new PrintDialog();
			
			if (prnqueue != null)
				dlg.PrintQueue = prnqueue;
			if (prntkt != null)
				dlg.PrintTicket = prntkt;
			
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				prnqueue = dlg.PrintQueue;
				prntkt = dlg.PrintTicket;
				
				// create PlaintTextDocumentPaginator
				PlainTextDocumentPaginator paginator = new PlainTextDocumentPaginator()
				{
					PrintTicket = prntkt,
					Text = txtbox.Text,
					Header = strLoadedFile,
					Typeface = new Typeface(
						txtbox.FontFamily, txtbox.FontStyle, txtbox.FontWeight, txtbox.FontStretch),
					FaceSize = txtbox.FontSize,
					Margins = marginPage,
					PageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight)
				};
				dlg.PrintDocument(paginator, Title);
			}
		}
	}
}
