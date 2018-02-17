/*
 * Created by SharpDevelop.
 * User: Шелли
 * Date: 02/12/2018
 * Time: 08:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.HandleAnEvent
{
	class HandleAnEvent
	{
		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			
			Window win = new Window();
			win.Title = "Handle An Event";
			win.MouseDown += WindowOnMouseDown;
			
			app.Run(win);
			
			Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			//Console.ReadKey(true);
		}
		
		static void WindowOnMouseDown(object sender, MouseButtonEventArgs args)
		{
			Window win = sender as Window;
			string strMessage = String.Format("Window clicked with {0} button at point {1}",
			                                 args.ChangedButton, args.GetPosition(win));
			MessageBox.Show(strMessage, win.Title);
		}
	}
}