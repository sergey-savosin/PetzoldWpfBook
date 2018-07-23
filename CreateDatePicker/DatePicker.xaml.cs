/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 19.07.2018
 * Time: 21:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CreateDatePicker
{
	/// <summary>
	/// Interaction logic for DatePicker.xaml
	/// </summary>
	public partial class DatePicker
	{
		UniformGrid unigridMonth;
		DateTime datetimeSaved = DateTime.Now.Date;
		
		// define dependency property
		public static readonly DependencyProperty DateProperty =
			DependencyProperty.Register("Date",
			                            typeof(DateTime?),
			                            typeof(DatePicker),
			                            new PropertyMetadata(
			                            	new DateTime(),
			                            	DateChangedCallback));
		
		// define routed event
		public static readonly RoutedEvent DateChangedEvent =
			EventManager.RegisterRoutedEvent(
				"DateChanged",
				RoutingStrategy.Bubble,
				typeof(RoutedPropertyChangedEventHandler<DateTime?>),
				typeof(DatePicker));
		
		public DatePicker()
		{
			InitializeComponent();
			
			//Date = DateTime.Now.Date;
			Loaded += DatePickerOnLoaded;
		}

		void DatePickerOnLoaded(object sender, RoutedEventArgs e)
		{
			unigridMonth = FindUniGrid(lstboxMonth);
			if (Date != null)
			{
				DateTime dt = (DateTime)Date;
				unigridMonth.FirstColumn =
					(int)(new DateTime(dt.Year, dt.Month, 1).DayOfWeek);
			}
			Date = DateTime.Now.Date;
		}
		
		UniformGrid FindUniGrid(DependencyObject vis)
		{
			if (vis is UniformGrid)
				return vis as UniformGrid;
			
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(vis); i++)
			{
				Visual visReturn =
					FindUniGrid(VisualTreeHelper.GetChild(vis, i));
				if (visReturn != null)
					return visReturn as UniformGrid;
			}
			
			return null;
		}
		
		public DateTime? Date
		{
			set { SetValue(DateProperty, value); }
			get { return (DateTime?)GetValue(DateProperty); }
		}
		
		public event RoutedPropertyChangedEventHandler<DateTime?> DateChanged
		{
			add { AddHandler(DateChangedEvent, value); }
			remove { RemoveHandler(DateChangedEvent, value); }
		}
		
		void ButtonBackOnClick(object sender, RoutedEventArgs e)
		{
			FlipPage(true);
		}
		
		void ButtonForwardOnClick(object sender, RoutedEventArgs e)
		{
			FlipPage(false);
		}
		
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			//? base.OnKeyDown(e);
			
			if (e.Key == Key.PageDown)
			{
				FlipPage(true);
				e.Handled = true;
			}
			else if (e.Key == Key.PageUp)
			{
				FlipPage(false);
				e.Handled = true;
			}
		}
		
		void FlipPage(bool IsBack)
		{
			if (Date == null)
				return;
			
			DateTime dt = (DateTime)Date;
			int numPages = IsBack ? -1 : 1;
			
			// Shift => scroll by years
			if (Keyboard.IsKeyDown(Key.LeftShift) ||
			    Keyboard.IsKeyDown(Key.RightShift))
				numPages *= 12;
			
			// Ctrl => scroll by 10 years
			if (Keyboard.IsKeyDown(Key.LeftCtrl) ||
			    Keyboard.IsKeyDown(Key.RightCtrl))
				numPages = Math.Max(-1200, Math.Min(1200, 120 * numPages));
			
			// calc new DateTime object
			int year = dt.Year + numPages / 12;
			int month = dt.Month + numPages % 12;
			while (month < 1)
			{
				month += 12;
				year -= 1;
			}
			
			while (month > 12)
			{
				month -= 12;
				year += 1;
			}
			
			// set Date property (gens DateChangedCallback)
			if (year < DateTime.MinValue.Year)
				Date = DateTime.MinValue.Date;
			else if (year > DateTime.MaxValue.Year)
				Date = DateTime.MaxValue.Date;
			else
				Date = new DateTime(year,
				                    month,
				                    Math.Min(dt.Day,
				                             DateTime.DaysInMonth(year, month)));
		}
		
		void LstBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Date == null)
				return;
			DateTime dt = Date.Value;
			
			if (lstboxMonth.SelectedIndex != -1)
			{
				Date = new DateTime(dt.Year,
				                    dt.Month,
				                    Int32.Parse(lstboxMonth.SelectedItem as string));
			}
		}
		
		void ChkboxNullOnChecked(object sender, RoutedEventArgs e)
		{
			if (Date != null)
			{
				datetimeSaved = Date.Value;
				Date = null;
			}
		}
		
		void ChkboxNullOnUnchecked(object sender, RoutedEventArgs e)
		{
			Date = datetimeSaved;
		}

		static void DateChangedCallback(DependencyObject obj, 
		                                DependencyPropertyChangedEventArgs args)
		{
			(obj as DatePicker).OnDateChanged((DateTime?)args.OldValue,
			                                  (DateTime?)args.NewValue);
		}
		
		protected virtual void OnDateChanged(DateTime? dtOldValue,
		                                     DateTime? dtNewValue)
		{
			chkboxNull.IsChecked = dtNewValue == null;
			if (dtNewValue != null)
			{
				DateTime dtNew = dtNewValue.Value;
				
				txtblkMonthYear.Text = dtNew.ToString(
					DateTimeFormatInfo.CurrentInfo.YearMonthPattern);
				
				// Set first day of month
				if (unigridMonth != null)
				{
					unigridMonth.FirstColumn =
						(int)(new DateTime(dtNew.Year,
						                   dtNew.Month,
						                   1).DayOfWeek);
					int iDaysInMonth = DateTime.DaysInMonth(dtNew.Year,
					                                        dtNew.Month);
					
					// if days count differs, fill the list
					if (iDaysInMonth != lstboxMonth.Items.Count)
					{
						lstboxMonth.BeginInit();
						lstboxMonth.Items.Clear();
						for (int i=0; i < iDaysInMonth; i++)
							lstboxMonth.Items.Add((i + 1).ToString());
						lstboxMonth.EndInit();
					}
					lstboxMonth.SelectedIndex = dtNew.Day - 1;
				}
				
				// init event DateChangeEvent
				RoutedPropertyChangedEventArgs<DateTime?> args =
					new RoutedPropertyChangedEventArgs<DateTime?>(
						dtOldValue,
						dtNewValue,
						DatePicker.DateChangedEvent);
				args.Source = this;
				RaiseEvent(args);
			}
		}
	}
}