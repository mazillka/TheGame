using System;

namespace TheGame
{
	public class ItemObject
	{
		String Name { set; get; }
		public ItemObject(String name)
		{
			Name = name;
		}

		public void Print()
		{
			Console.WriteLine(Name);
		}
	}
}
