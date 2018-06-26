/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 06/26/2018
 * Time: 22:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace Petzold.ColorScroll
{
	/// <summary>
	/// Description of DoubleToByteConverter.
	/// </summary>
	[ValueConversion(typeof(double), typeof(byte))]
	public class DoubleToByteConverter : IValueConverter
	{
		public object Convert(object value, Type typeTarget,
		                     object param, CultureInfo culture)
		{
			return (byte)(double)value;
		}
		
		public object ConvertBack(object value, Type typeTarget,
		                          object param, CultureInfo culture)
		{
			return (double)value;
		}
	}
}
