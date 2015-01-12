using System;
using System.Collections.Generic;

namespace TheGame
{
	public class Enemy : Unit, IMapObject
	{
		public event EventHandler OnDespawn;

		public int Attack
		{
			get
			{
				return Magic.Random.Next(0, 3);
			}
		}

		public int Block
		{
			get
			{
				return Magic.Random.Next(0, 3);
			}
		}

		public List<ItemObject> Inventory = new List<ItemObject>();
		private Coord _pos;
		private static readonly String _symbol = "X";

		public Enemy(Coord pos, Unit obj, List<ItemObject> inventory)
			: base(obj)
		{
			Inventory = inventory;
			_pos = pos;
		}

		public void Despawn()
		{
			Map.Labyrinth[_pos.X, _pos.Y] = new None(_pos);
			if (OnDespawn != null)
			{
				OnDespawn(null, null);
			}
		}

		public void Spawn()
		{
			Map.Labyrinth[_pos.X, _pos.Y] = this;
		}

		public void PrintObjectSymbol()
		{
			Console.Write(_symbol);
		}

		public void PrintEnemyStats()
		{
			var posX = 82;
			var posY = 34;
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("  Enemy: ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(Type);
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("Hp: {0}", Hp);
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("Damage: {0}", Damage);
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("Defence: {0}", Defence);
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine("Critical: {0}", Critical);
		}
	}
}
