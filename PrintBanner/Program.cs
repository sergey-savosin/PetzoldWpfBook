/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 01.05.2018
 * Time: 7:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Printing;

namespace Petzold.PrintBanner
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	class PrintBanner : Window
	{
		TextBox txtbox;
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application app = new Application();
			app.Run(new PrintBanner());
		}
		
		public PrintBanner()
		{
			Title = "Print a banner";
			SizeToContent = SizeToContent.WidthAndHeight;
			
			StackPanel stack = new StackPanel();
			Content = stack;
			
			txtbox = new TextBox()
			{
				Width = 250,
				Margin = new Thickness(12),
				Text = "Test"
			};
			stack.Children.Add(txtbox);
			
			Button btn = new Button()
			{
				Content = "Print...",
				Margin = new Thickness(12),
				HorizontalAlignment = HorizontalAlignment.Center
			};
			btn.Click += PrintOnClick;
			stack.Children.Add(btn);
			
			txtbox.Focus();
		}
		
		void PrintOnClick(object sender, RoutedEventArgs e)
		{
			PrintDialog dlg = new PrintDialog();
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				// book orientation
				PrintTicket prntkt = dlg.PrintTicket;
				prntkt.PageOrientation = PageOrientation.Portrait;
				dlg.PrintTicket = prntkt;
				
				var printCapabilities = dlg.PrintQueue.GetPrintCapabilities(dlg.PrintTicket);
				var ow = printCapabilities.PageImageableArea.OriginWidth;
				var oh = printCapabilities.PageImageableArea.OriginHeight;
				
				BannerDocumentPaginator paginator = new BannerDocumentPaginator();
				paginator.Text = txtbox.Text;
				paginator.Typeface = new Typeface("Times New Roman");
				paginator.PageSize = new Size(
					dlg.PrintableAreaWidth,
					dlg.PrintableAreaHeight);
				
				dlg.PrintDocument(paginator, "Banner: " + txtbox.Text);
			}
		}
	}
}
