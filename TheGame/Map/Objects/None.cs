using System;

namespace TheGame
{
	class None : MapObject
	{
		private static readonly String _symbol = " ";
		public None(Coord position) : base(position, _symbol) { }
	}
}
