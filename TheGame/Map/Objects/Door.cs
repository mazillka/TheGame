using System;

namespace TheGame
{
	class Door : MapObject
	{
		private static readonly String _symbol = "+";
		public Door(Coord position) : base(position, _symbol) { }
	}
}
