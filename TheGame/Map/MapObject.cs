using System;

namespace TheGame
{
	public class MapObject : IMapObject
	{
		public Coord Pos { set; get; }
		public String Symbol { set; get; }

		public event EventHandler OnDespawn;

		public MapObject(Coord position, String symbol)
		{
			Pos = position;
			Symbol = symbol;
		}

		public void PrintObjectSymbol()
		{
			Console.Write(Symbol);
		}

		public void Despawn()
		{
			Map.Labyrinth[Pos.X, Pos.Y] = new None(Pos);
			if (OnDespawn != null)
			{
				OnDespawn(null, null);
			}
		}

		public void Spawn()
		{
			Map.Labyrinth[Pos.X, Pos.Y] = this;
		}
	}

}
