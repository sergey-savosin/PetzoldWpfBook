/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 01.05.2018
 * Time: 8:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Printing;

namespace Petzold.PrintBanner
{
	/// <summary>
	/// Description of BannerDocumentPaginator.
	/// </summary>
	public class BannerDocumentPaginator : DocumentPaginator
	{
		string txt = "";
		Typeface face = new Typeface("");
		Size sizePage;
		Size sizeMax = new Size(0, 0);
		
		// public properties
		public string Text
		{
			set {txt = value;}
			get {return txt;}
		}
		
		public Typeface Typeface
		{
			set {face = value;}
			get {return face;}
		}
		
		// create formatted text
		FormattedText GetFormattedText(char ch, Typeface face1, double em)
		{
			return new FormattedText(
				ch.ToString(),
				CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight,
				face1,
				em,
				Brushes.Black);
		}
		
		public override bool IsPageCountValid
		{
			get
			{
				// get max symbols's size
				foreach(char ch in txt)
				{
					FormattedText formtxt = GetFormattedText(ch, face, 100); //100
					sizeMax.Width = Math.Max(sizeMax.Width, formtxt.Width);
					sizeMax.Height = Math.Max(sizeMax.Height, formtxt.Height);
				}
				return true;
			}
		}
		
		public override int PageCount
		{
			get
			{
				return txt == null ? 0 : txt.Length;
			}
		}
		
		public override Size PageSize {
			get {
				return sizePage;
			}
			set {
				sizePage = value;
			}
		}
		
		public override DocumentPage GetPage(int pageNumber)
		{
			DrawingVisual vis = new DrawingVisual();
			DrawingContext dc = vis.RenderOpen();
			
			// margins 0.5 inch
			const double margin = 96.0;
			
			// Rect for page minus margins
			Rect rectPage = new Rect(
				margin,
				margin,
				PageSize.Width - 2.0 * margin,
				PageSize.Height - 2.0 * margin);
			
			Pen pn = new Pen(Brushes.Black, 1);
						
			// draw border rect
			dc.DrawRectangle(null, pn, rectPage);

				
			double factor = Math.Min(
				(PageSize.Width - margin) / sizeMax.Width,
				(PageSize.Height - margin) / sizeMax.Height);
			FormattedText formtxt = GetFormattedText(txt[pageNumber], face, factor * 100);
			
			// point of page center
			Point ptText = new Point(
				(PageSize.Width - formtxt.Width) / 2,
				(PageSize.Height - formtxt.Height) / 2);
			ptText = new Point(10,-110);
			dc.DrawText(formtxt, ptText);
			dc.Close();
			
			return new DocumentPage(vis);
		}
		
		public override IDocumentPaginatorSource Source {
			get {
				return null;
			}
		}
		
	}
}
