/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 28.06.2018
 * Time: 22:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Petzold.FormattedDigitalClock
{
	/// <summary>
	/// Description of FormattedTextConverter.
	/// </summary>
	public class FormattedTextConverter : IValueConverter
	{
		public object Convert(object value, Type typeTarget,
		                      object param, CultureInfo culture)
		{
			if (param is string)
			{
				return String.Format(param as string, value);
			}
			return value.ToString();
		}
		
		public object ConvertBack(object value, Type typeTarget,
		                          object param, CultureInfo culture)
		{
			return null;
		}
	}
}
