/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 22.05.2018
 * Time: 22:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;


namespace Petzold.DumpContentPropertyAttributes
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			UIElement dummy1 = new UIElement();
			FrameworkElement dummy2 = new FrameworkElement();
			
			SortedList<string, string> listClass = new SortedList<string, string>();
			string strFormat = "{0, -35}{1}";
			
			// list loaded assemblies
			foreach (AssemblyName asmblyname in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
			{
				// list types
				foreach (Type type in Assembly.Load(asmblyname).GetTypes())
				{
					// list attributes. Use "false" to filter only non-child attributes
					foreach (object obj in type.GetCustomAttributes(
						typeof(ContentPropertyAttribute), true))
					{
						if (type.IsPublic &&
						    obj as ContentPropertyAttribute != null)
						{
							listClass.Add(type.Name, (obj as ContentPropertyAttribute).Name);
						}
					}
				}
			}
			
			// show result
			Console.WriteLine(strFormat, "Class", "Content property");
			Console.WriteLine(strFormat, "-----", "----------------");
			foreach (string strClass in listClass.Keys)
			{
				Console.WriteLine(strFormat, strClass, listClass[strClass]);
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}