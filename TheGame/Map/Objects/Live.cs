using System;

namespace TheGame
{
	class Live : MapObject
	{
		private static readonly String _symbol = ((char) 3).ToString();
		public Live(Coord position) : base(position, _symbol) { }
	}
}
