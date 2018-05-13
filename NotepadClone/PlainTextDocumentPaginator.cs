/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 10.05.2018
 * Time: 22:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of PlainTextDocumentPaginator.
	/// </summary>
	public class PlainTextDocumentPaginator : DocumentPaginator
	{
		char[] charsBreak = new char[]{' ', '-'};
		string txt = "";
		string txtHeader = null;
		Typeface face = new Typeface("");
		double em = 11;
		Size sizePage = new Size(8.5 * 96, 11 * 96);
		Size sizeMax = new Size(0, 0);
		Thickness margins = new Thickness(96);
		PrintTicket prntkt = new PrintTicket();
		TextWrapping txtwrap = TextWrapping.Wrap;
		
		// pages
		List<DocumentPage> listPages;
		
		// public properties
		public string Text
		{
			set {txt = value;}
			get {return txt;}
		}
		
		public TextWrapping TextWrapping
		{
			set {txtwrap = value;}
			get {return txtwrap;}
		}
		
		public Thickness Margins
		{
			set {margins = value;}
			get {return margins;}
		}
		
		public Typeface Typeface
		{
			set {face = value;}
			get {return face;}
		}
		
		public double FaceSize
		{
			set {em = value;}
			get {return em;}
		}
		
		public PrintTicket PrintTicket
		{
			set {prntkt = value;}
			get {return prntkt;}
		}
		
		public string Header
		{
			set {txtHeader = value;}
			get {return txtHeader;}
		}
		
		// overridings
		public override bool IsPageCountValid 
		{

			get
			{
				if (listPages == null)
					Format();
				return true;
			}
		}
		
		public override int PageCount 
		{
			get
			{
				if (listPages == null)
					return 0;
				return listPages.Count;
			}
		}
		
		public override Size PageSize
		{
			get
			{
				return sizePage;
			}
			set
			{
				sizePage = value;
			}
		}
		
		public override DocumentPage GetPage(int pageNumber)
		{
			return listPages[pageNumber];
		}
		
		public override IDocumentPaginatorSource Source
		{
			get
			{
				return null;
			}
		}
		
		class PrintLine
		{
			public string String;
			public bool Flag;
			public PrintLine(string str, bool flag)
			{
				String = str;
				Flag = flag;
			}
		}
		
		void Format()
		{
			List<PrintLine> listLines = new List<PrintLine>();
			FormattedText formtxtSample = GetFormattedText("W");
			
			// printed line width
			double width = PageSize.Width - Margins.Left - Margins.Right;
			
			// validation 1
			if (width < formtxtSample.Width)
				return;
			
			string strLine;
			Pen pn = new Pen(Brushes.Black, 2);
			StringReader reader = new StringReader(txt);
			
			// call ProcessLine
			while (null != (strLine = reader.ReadLine()))
			{
				ProcessLine(strLine, width, listLines);
			}
			reader.Close();
			
			// prepare pages
			double heightLine = formtxtSample.LineHeight + formtxtSample.Height;
			double height = PageSize.Height - Margins.Top - Margins.Bottom;
			int linesPerPage = (int)(height / heightLine);
			
			// validation 2
			if (linesPerPage < 1)
				return;
			
			int numPages = (listLines.Count + linesPerPage - 1) / linesPerPage;
			double xStart = Margins.Left;
			double yStart = Margins.Top;
			
			listPages = new List<DocumentPage>();
			
			for (int iPage = 0, iLine = 0; iPage < numPages; iPage ++)
			{
				// create DravingVisual
				DrawingVisual vis = new DrawingVisual();
				DrawingContext dc = vis.RenderOpen();
				
				Pen pn_border = new Pen(Brushes.Black, 1);
				Rect rect_border = new Rect(
					0, 0,
					PageSize.Width,
					PageSize.Height
				);
				dc.DrawRectangle(null, pn_border, rect_border);

				// show Header
				if (Header != null && Header.Length > 0)
				{
					FormattedText formtxt = GetFormattedText(Header);
					formtxt.SetFontWeight(FontWeights.Bold);
					Point ptText = new Point(
						xStart,
						yStart - 2 * formtxt.Height);
					dc.DrawText(formtxt, ptText);
				}
				
				// show finalizer at the end of page
				if (numPages > 1)
				{
					FormattedText formtxt =
						GetFormattedText("Page " + (iPage + 1) + " of " + numPages);
					formtxt.SetFontWeight(FontWeights.Bold);
					Point ptText = new Point(
						(PageSize.Width + Margins.Left - Margins.Right - formtxt.Width) / 2,
						PageSize.Height - Margins.Bottom + formtxt.Height);
					dc.DrawText(formtxt, ptText);
				}
				
				// iterate through lines at page
				for (int i = 0; i<linesPerPage; i++, iLine++)
				{
					if (iLine == listLines.Count)
						break;
					// prepare information for a line
					string str = listLines[iLine].String;
					FormattedText formtxt = GetFormattedText(str);
					Point ptText = new Point(
						xStart,
						yStart + i * heightLine);
					dc.DrawText(formtxt, ptText);
					
					// show arrow if needed
					if (listLines[iLine].Flag)
					{
						double x = xStart + width + 6;
						double y = yStart + i*heightLine + formtxt.Baseline;
						double len = face.CapsHeight * em;
						dc.DrawLine(pn, new Point(x, y), new Point(x + len, y - len));
						dc.DrawLine(pn, new Point(x, y), new Point(x, y - len/2));
						dc.DrawLine(pn, new Point(x, y), new Point(x + len/2, y));
					}
				}
				dc.Close();
				
				// create DocumentPage
				DocumentPage page = new DocumentPage(vis);
				listPages.Add(page);
			}
			reader.Close();
		}
		
		// convert text line into several print lines
		void ProcessLine(string str, double width, List<PrintLine> list)
		{
			str = str.TrimEnd(' ');
			
			// TextWrapping == TextWrapping.NoWrap
			if (TextWrapping == TextWrapping.NoWrap)
			{
				do
				{
					int length = str.Length;
					while (GetFormattedText(str.Substring(0, length)).Width > width)
						length--;
					list.Add(new PrintLine(str.Substring(0, length), length < str.Length));
					str = str.Substring(length);
				}
				while (str.Length > 0);
			}
			// TextWrapping = Wrap or WrapWithOverflow
			else
			{
				do
				{
					int length = str.Length;
					bool flag = false;
					while (GetFormattedText(str.Substring(0, length)).Width > width)
					{
						int index = str.LastIndexOfAny(charsBreak, length - 2);
						if (index != -1)
							length = index + 1; // include space or minus
						else
						{
							// test for existing of "-"
							index = str.IndexOfAny(charsBreak);
							if (index != -1)
								length = index + 1;
							
							if (TextWrapping == TextWrapping.Wrap)
							{
								while (GetFormattedText(
									str.Substring(0, length)).Width > width)
									length --;
								flag = true;
							}
							break;
						}
					}
					list.Add(new PrintLine(str.Substring(0, length), flag));
					str = str.Substring(length);
				}
				while (str.Length > 0);
			}
		}
		
		// create FormattedText
		FormattedText GetFormattedText(string str)
		{
			return new FormattedText(
				str,
				CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight,
				face,
				em,
				Brushes.Black);
		}
	}
}
