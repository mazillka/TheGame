using System;
using System.IO;
using System.Xml.Serialization;

namespace TheGame
{
	public static class UnitClasses
	{
		private static readonly UnitXml Temp = new UnitXml();
		public static Units List = Temp.UnitsList;

		private class UnitXml
		{
			public readonly Units UnitsList;

			public UnitXml()
			{
				var xml = new XmlSerializer(typeof(Units));
				try
				{
					using (var stream = new FileStream("../../Units.xml", FileMode.Open))
					{
						UnitsList = (Units)xml.Deserialize(stream);
						stream.Flush();
						stream.Close();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
