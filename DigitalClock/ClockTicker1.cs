/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 28.06.2018
 * Time: 21:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Threading;

namespace Petzold.DigitalClock
{
	/// <summary>
	/// Description of ColorTicker1.
	/// </summary>
	public class ClockTicker1 : DependencyObject
	{
		public static DependencyProperty DateTimeProperty =
			DependencyProperty.Register("DateTime", typeof(DateTime),
			                            typeof(ClockTicker1));
		public DateTime DateTime
		{
			set {SetValue(DateTimeProperty, value);}
			get { return (DateTime)GetValue(DateTimeProperty);}
		}
		
		public ClockTicker1()
		{
			DispatcherTimer timer = new DispatcherTimer()
			{
				Interval = TimeSpan.FromSeconds(1)
			};
			timer.Tick += TimerOnTick;
			timer.Start();
		}

		void TimerOnTick(object sender, EventArgs e)
		{
			DateTime = DateTime.Now;
		}
		
	}
}
