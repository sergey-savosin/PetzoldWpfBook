/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 07.05.2018
 * Time: 23:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Petzold.NotepadClone
{
	/// <summary>
	/// Description of NotepadCloneSettings.
	/// </summary>
	public class NotepadCloneSettings
	{
		// default config
		public WindowState WindowState = WindowState.Normal;
		public Rect RestoreBounds = Rect.Empty;
		public TextWrapping TextWrapping = TextWrapping.NoWrap;
		public string FontFamily = "";
		public string FontStyle = new FontStyleConverter().ConvertToString(FontStyles.Normal);
		public string FontWeight = new FontWeightConverter().ConvertToString(FontWeights.Normal);
		public string FontStretch = new FontStretchConverter().ConvertToString(FontStretches.Normal);
		public double FontSize = 11;
		
		// save settings to file
		public virtual bool Save(string strAppData)
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(strAppData));
				StreamWriter write = new StreamWriter(strAppData);
				XmlSerializer xml = new XmlSerializer(GetType());
				xml.Serialize(write, this);
				write.Close();
			}
			catch
			{
				return false;
			}
			
			return true;
		}
		
		// load settings from file
		public static object Load(Type type, string strAppData)
		{
			StreamReader reader;
			object settings;
			XmlSerializer xml = new XmlSerializer(type);
			
			try
			{
				reader = new StreamReader(strAppData);
				settings = xml.Deserialize(reader);
				reader.Close();
			}
			catch
			{
				settings = type.GetConstructor(
					System.Type.EmptyTypes).Invoke(null);
			}
			
			return settings;
		}
	}
}
