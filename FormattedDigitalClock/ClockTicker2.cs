/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 28.06.2018
 * Time: 22:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Petzold.FormattedDigitalClock
{
	/// <summary>
	/// Description of ClockTicker2.
	/// </summary>
	public class ClockTicker2 : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public DateTime DateTime
		{
			get {return DateTime.Now;}
		}
		
		public ClockTicker2()
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
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
				                new PropertyChangedEventArgs("DateTime"));
			}
		}
	}
}
