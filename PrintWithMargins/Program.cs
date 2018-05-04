/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 30.04.2018
 * Time: 22:02
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

namespace Petzold.PrintWithMargins
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class PrintWithMargins : Window
	{
		PrintQueue prnqueue;
		PrintTicket prntkt;
		Thickness marginPage = new Thickness(96);
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application app = new Application();
			app.Run(new PrintWithMargins());
		}
		
		public PrintWithMargins()
		{
			Title = "Print with margins";
			
			StackPanel stack = new StackPanel();
			Content = stack;
			
			// btn Page Setup
			Button btn = new Button()
			{
				Content = "Page set_up...",
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(24)
			};
			btn.Click += SetupOnClick;
			stack.Children.Add(btn);
			
			// btn Print
			btn = new Button()
			{
				Content = "Print...",
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(24)
			};
			btn.Click += PrintOnClick;
			stack.Children.Add(btn);
		}

		// call PrintDialog
		void PrintOnClick(object sender, RoutedEventArgs e)
		{
			PrintDialog dlg = new PrintDialog();
			
			// init PrintQueue and PrintTicket
			if (prnqueue != null)
				dlg.PrintQueue = prnqueue;
			if (prntkt != null)
				dlg.PrintTicket = prntkt;
			
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				// save PrintQueue and PrintTicket
				prnqueue = dlg.PrintQueue;
				prntkt = dlg.PrintTicket;
				
				// create DrawingVisual
				DrawingVisual vis = new DrawingVisual();
				DrawingContext dc = vis.RenderOpen();
				Pen pn = new Pen(Brushes.Black, 1);
				
				// Rect for page minus margins
				Rect rectPage = new Rect(
					marginPage.Left,
					marginPage.Top,
					dlg.PrintableAreaWidth - (marginPage.Left + marginPage.Right),
					dlg.PrintableAreaHeight - (marginPage.Top + marginPage.Bottom));
				
				// draw border rect
				dc.DrawRectangle(null, pn, rectPage);
				
				// create formatted text
				FormattedText formtxt = new FormattedText(
					String.Format("Hello, Printer! {0:F3} x {1:F3}",
					              dlg.PrintableAreaWidth / 96,
					              dlg.PrintableAreaHeight / 96),
					CultureInfo.CurrentCulture,
					FlowDirection.LeftToRight,
					new Typeface(
						new FontFamily("Times New Roman"),
						FontStyles.Italic,
						FontWeights.Normal,
						FontStretches.Normal),
					48,
					Brushes.Black);
				
				// get physical size of this string
				Size sizeText = new Size(formtxt.Width, formtxt.Height);
				
				// center point
				Point ptText = new Point(
					rectPage.Left + (rectPage.Width - formtxt.Width) / 2,
					rectPage.Top + (rectPage.Height - formtxt.Height) / 2);
				
				// draw text and border
				dc.DrawText(formtxt, ptText);
				dc.DrawRectangle(null, pn, new Rect(ptText, sizeText));
				
				// close drawing context
				dc.Close();
				
				// print a page
				dlg.PrintVisual(vis, Title);
			}
		}
		
		void SetupOnClick(object sender, RoutedEventArgs e)
		{
			PageMarginsDialog dlg = new PageMarginsDialog()
			{
				Owner = this,
				PageMargins = marginPage
			};
			
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				// save data from dialog
				marginPage = dlg.PageMargins;
			}
		}
	}
}
