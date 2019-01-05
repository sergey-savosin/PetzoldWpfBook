/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 07/04/2018
 * Time: 22:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace Petzold.DumpControlTemplate
{
	/// <summary>
	/// Description of ControlMenuItem.
	/// </summary>
	public class ControlMenuItem : MenuItem
	{
		public ControlMenuItem()
		{
			Assembly asbly = Assembly.GetAssembly(typeof(Control));
			Type[] atype = asbly.GetTypes();
			SortedList<string, MenuItem> sortlst = new SortedList<string, MenuItem>();
			Header = "Control";
			Tag = typeof(Control);
			sortlst.Add("Control", this);
			
			// enlist all types
			foreach (Type typ in atype)
			{
				if (typ.IsPublic && (typ.IsSubclassOf(typeof(Control))))
				{
					MenuItem item = new MenuItem()
					{
						Header = typ.Name,
						Tag = typ
					};
					sortlst.Add(typ.Name, item);
				}
			}
			
			// enlist array
			foreach (KeyValuePair<string, MenuItem> kvp in sortlst)
			{
				if (kvp.Key != "Control")
				{
					string strParent = ((Type)kvp.Value.Tag).BaseType.Name;
					MenuItem itemParent = sortlst[strParent];
					itemParent.Items.Add(kvp.Value);
				}
			}
			
			// second enlist of array
			foreach (KeyValuePair<string, MenuItem> kvp in sortlst)
			{
				Type typ = (Type)kvp.Value.Tag;
				
				if (typ.IsAbstract && kvp.Value.Items.Count == 0)
					kvp.Value.IsEnabled = false;
				
				if (!typ.IsAbstract && kvp.Value.Items.Count > 0)
				{
					MenuItem item = new MenuItem()
					{
						Header = kvp.Value.Header as string,
						Tag = typ
					};
					kvp.Value.Items.Insert(0, item);
				}
			}
		}
	}
}
