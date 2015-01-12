using System;

namespace TheGame
{
	public class Target : EventArgs
	{
		public Object Value { set; get; }

		public Target(Object obj)
		{
			Value = obj;
		}
	}
}
