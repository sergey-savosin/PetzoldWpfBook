/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 26.06.2018
 * Time: 22:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace Petzold.DecimalScrollBar
{
	/// <summary>
	/// Description of DoubleToDecimalConverter.
	/// </summary>
	[ValueConversion(typeof(double), typeof(decimal))]
	public class DoubleToDecimalConverter : IValueConverter
	{
		public object Convert(object value, Type typeTarget,
		               object param, CultureInfo culture)
		{
			decimal num = new Decimal((double)value);
			if (param != null)
			{
				num = Decimal.Round(num, Int32.Parse(param as string));
			}
			return num;
		}
		
		public object ConvertBack(object value, Type typeTarget,
		                          object param, CultureInfo culture)
		{
			return Decimal.ToDouble((decimal)value);
		}
	}
}
