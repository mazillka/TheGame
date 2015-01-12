namespace TheGame
{
	public struct Coord
	{
		int _x;
		int _y;

		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}

		public int Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y = value;
			}
		}

		public Coord(int posX, int posY)
		{
			_x = posX;
			_y = posY;
		}
	}
}
