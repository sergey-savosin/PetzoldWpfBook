/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 18.06.2018
 * Time: 22:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Petzold.CustomElementBinding
{
	/// <summary>
	/// Description of SimpleElement.
	/// </summary>
	class SimpleElement : FrameworkElement
	{
		public static DependencyProperty NumberProperty;
		
		static SimpleElement()
		{
			NumberProperty =
				DependencyProperty
				.Register("Number",
					typeof(double),
					typeof(SimpleElement),
					new FrameworkPropertyMetadata(
						0.0,
						FrameworkPropertyMetadataOptions.AffectsRender));
		}
		
		public double Number
		{
			set {SetValue(NumberProperty, value);}
			get {return (double)GetValue(NumberProperty);}
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			return new Size(200, 50);
		}
		
		protected override void OnRender(DrawingContext drawingContext)
		{
			drawingContext.DrawText(
				new FormattedText(
					Number.ToString(),
					CultureInfo.CurrentCulture,
					FlowDirection.LeftToRight,
					new Typeface("Times New Roman"),
					12,
					SystemColors.WindowTextBrush),
				new Point(0, 0));
		}
	}
}
