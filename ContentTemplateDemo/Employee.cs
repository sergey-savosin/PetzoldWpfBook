/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 11.07.2018
 * Time: 22:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Petzold.ContentTemplateDemo
{
	/// <summary>
	/// Description of Employee.
	/// </summary>
	public class Employee
	{
		public string Name {get; set; }
		public string Face {get; set; }
		public DateTime Birthdate {get; set; }
		public bool Lefthanded {get; set; }
		
		public Employee()
		{
		}
		
		public Employee(string name, string face, DateTime birthdate, bool lefthanded)
		{
			Name = name;
			Face = face;
			Birthdate = birthdate;
			Lefthanded = lefthanded;
		}
	}
}
