using System;
using System.Collections.Generic;
using System.Linq;

namespace TheGame
{
	public delegate void EventHandler(Player player, Target obj);

	public class Player : Unit, IMapObject
	{
		public event EventHandler OnMove;
		public event EventHandler OnDoor;
		public event EventHandler OnLive;
		public event EventHandler OnEnemy;
		public event EventHandler OnDespawn;
		public event EventHandler OnLiveChange;
		public event EventHandler OnInventoryChange;

		public String Name { set; get; }
		private int _lives;
		public int Lives 
		{
			set
			{
				_lives = value;
				if (OnLiveChange != null)
				{
					OnLiveChange(this, null);
				}
			}
			get
			{
				return _lives;
			}
		}

		public int Kills { set; get; }		
		public List<ItemObject> Inventory = new List<ItemObject>();

		private int _attack;
		private int _block;

		public int Attack
		{
			get
			{
				ChooseAttack();
				return _attack;
			}
		}

		public int Block
		{
			get
			{
				ChooseBlock();
				return _block;
			}
		}

		private Coord _pos;
		private static readonly String _symbol = "@";

		public Player(Unit obj, String name, int lives, int kills, List<ItemObject> inventory)
			: base(obj)
		{
			Name = name;
			Lives = lives;
			Kills = kills;
			Inventory = inventory;

			// default player position
			_pos = new Coord(1, 1);
		}

		public void Despawn()
		{
			Map.Labyrinth[_pos.X, _pos.Y] = new None(_pos);
			if (OnDespawn != null)
			{
				OnDespawn(this, null);
			}
		}

		public void Spawn()
		{
			Map.Labyrinth[_pos.X, _pos.Y] = this;
		}

		public void Initialize()
		{
			Spawn();
			if (OnMove != null)
			{
				OnMove(this, null);
			}
			Move();
		}

		private void Move()
		{
			while (true)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow:
						if (_pos.X > 0 && Map.Labyrinth[_pos.X - 1, _pos.Y].GetType() != typeof(Wall))
						{
							var targetObj = Map.Labyrinth[_pos.X - 1, _pos.Y];

							if (Map.Labyrinth[_pos.X - 1, _pos.Y] is Door)
							{
								if (OnDoor != null)
								{
									OnDoor(this, new Target(targetObj));
								}
							}
							else if (Map.Labyrinth[_pos.X - 1, _pos.Y] is Live)
							{
								if (OnLive != null)
								{
									OnLive(this, new Target(targetObj));
								}
							}
							else if (Map.Labyrinth[_pos.X - 1, _pos.Y] is Enemy)
							{
								if (OnEnemy != null)
								{
									OnEnemy(this, new Target(targetObj));
								}
							}
							else
							{
								Despawn();
								_pos.X--;
								Spawn();
								if (OnMove != null)
								{
									OnMove(this, null);
								}
							}
						}
						break;

					case ConsoleKey.DownArrow:
						if (_pos.X < Map.Labyrinth.GetLength(0) && Map.Labyrinth[_pos.X + 1, _pos.Y].GetType() != typeof(Wall))
						{
							var targetObj = Map.Labyrinth[_pos.X + 1, _pos.Y];

							if (Map.Labyrinth[_pos.X + 1, _pos.Y] is Door)
							{
								if (OnDoor != null)
								{
									OnDoor(this, new Target(targetObj));
								}
							}
							else if (Map.Labyrinth[_pos.X + 1, _pos.Y] is Live)
							{
								if (OnLive != null)
								{
									OnLive(this, new Target(targetObj));
								}
							}
							else if (Map.Labyrinth[_pos.X + 1, _pos.Y] is Enemy)
							{
								if (OnEnemy != null)
								{
									OnEnemy(this, new Target(targetObj));
								}
							}
							else
							{
								Despawn();
								_pos.X++;
								Spawn();
								if (OnMove != null)
								{
									OnMove(this, null);
								}
							}
						}
						break;

					case ConsoleKey.LeftArrow:
						if (_pos.Y > 0 && Map.Labyrinth[_pos.X, _pos.Y - 1].GetType() != typeof(Wall))
						{
							var targetObj = Map.Labyrinth[_pos.X, _pos.Y - 1];

							if (Map.Labyrinth[_pos.X, _pos.Y - 1] is Door)
							{
								if (OnDoor != null)
								{
									OnDoor(this, new Target(targetObj));
								}
							}
							else if (Map.Labyrinth[_pos.X, _pos.Y - 1] is Live)
							{
								if (OnLive != null)
								{
									OnLive(this, new Target(targetObj));
								}
							}
							else if (Map.Labyrinth[_pos.X, _pos.Y - 1] is Enemy)
							{
								if (OnEnemy != null)
								{
									OnEnemy(this, new Target(targetObj));
								}
							}
							else
							{
								Despawn();
								_pos.Y--;
								Spawn();
								if (OnMove != null)
								{
									OnMove(this, null);
								}
							}
						}
						break;

					case ConsoleKey.RightArrow:
						if (_pos.Y < Map.Labyrinth.GetLength(1) - 1 && Map.Labyrinth[_pos.X, _pos.Y + 1].GetType() != typeof(Wall))
						{
							var targetObj = Map.Labyrinth[_pos.X, _pos.Y + 1];

							if (Map.Labyrinth[_pos.X, _pos.Y + 1] is Door)
							{
								if (OnDoor != null)
								{
									OnDoor(this, new Target(targetObj));
								}					
							}
							else if (Map.Labyrinth[_pos.X, _pos.Y + 1] is Live)
							{
								if (OnLive != null)
								{
									OnLive(this, new Target(targetObj));
								}
							}
							else if (Map.Labyrinth[_pos.X, _pos.Y + 1] is Enemy)
							{
								if (OnEnemy != null)
								{
									OnEnemy(this, new Target(targetObj));
								}
							}
							else
							{
								Despawn();
								_pos.Y++;
								Spawn();
								if (OnMove != null)
								{
									OnMove(this, null);
								}
							}
						}
						break;
				}
			}
		}

		public void ChooseAttack()
		{
			var attackMenu = new Menu("attack: ");
			attackMenu.AddSubMenu("Head", delegate(int x)
			{
				_attack = x;
				attackMenu.Clear();
			});
			attackMenu.AddSubMenu("Body", delegate(int x)
			{
				_attack = x;
				attackMenu.Clear();
			});
			attackMenu.AddSubMenu("Legs", delegate(int x)
			{
				_attack = x;
				attackMenu.Clear();
			});
			attackMenu.Print(40, 35);
		}

		public void ChooseBlock()
		{
			var protectMenu = new Menu("block: ");
			protectMenu.AddSubMenu("Head", delegate(int x)
			{
				_block = x;
				protectMenu.Clear();
			});
			protectMenu.AddSubMenu("Body", delegate(int x)
			{
				_block = x;
				protectMenu.Clear();
			});
			protectMenu.AddSubMenu("Legs", delegate(int x)
			{
				_block = x;
				protectMenu.Clear();
			});
			protectMenu.Print(50, 35);
		}

		public void RemoveKey()
		{
			Inventory.Remove(Inventory.OfType<Key>().First());
			if (OnInventoryChange != null)
			{
				OnInventoryChange(this, null);
			}
		}

		public void PrintObjectSymbol()
		{
			Console.Write(_symbol);
		}

		public void PrintPlayerStats()
		{
			var posX = 22;
			var posY = 34;
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("  Player: ");
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
