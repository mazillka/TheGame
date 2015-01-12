using System;

namespace TheGame
{
	class Wall : MapObject
	{
		private static readonly String _symbol = "#";
		public Wall(Coord position) : base(position, _symbol) { }
	}
}
