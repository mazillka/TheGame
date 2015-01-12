using System;
using System.Linq;

namespace TheGame
{
	public class Game
	{
		public Game(Player player)
		{
			player.OnMove += PrintMap;
			player.OnDoor += Door;
			player.OnLive += Live;
			player.OnEnemy += Enemy;
			player.OnLiveChange += PrintPlayerInfo;
			player.OnInventoryChange += PrintPlayerInfo;

			foreach (var entry in Map.Labyrinth)
			{
				entry.OnDespawn += PrintMap;
			}

			PrintPlayerInfo(player, null);

			player.Initialize();
		}

		private static void Door(Player player, Target obj)
		{
			const int posX = 22;
			var posY = 34;
			Magic.ClearMsg(0);
			Magic.Print("you found locked door.", posX, posY++, 20);
			if (player.Inventory.OfType<Key>().Any())
			{
				Magic.Print("and opened it with key.", posX, posY, 20);
				player.RemoveKey();
				(obj.Value as Door).Despawn();
			}
			else
			{
				var dice = Magic.Random.Next(0, 10);
				Magic.Print("in order to pass you must pick up number from 0 to 9:", posX, posY++, 20);

				for (var i = 0; i < 3; i++)
				{
					int number;
					posY++;
					Magic.Print("attempt " + (i + 1) + ": ", posX, posY++, 20);

					Console.ForegroundColor = ConsoleColor.Green;
					Int32.TryParse(Console.ReadLine(), out number);
					Console.ResetColor();

					if (number != dice)
					{
						continue;
					}
					Magic.Print("door opened, now you can pass", posX, posY + 3, 30);
					(obj.Value as Door).Despawn();
					break;
				}
				Magic.Print("door closed!", posX, posY + 3, 30);
			}
		}

		private static void Live(Player player, Target obj)
		{
			Magic.ClearMsg(0);
			Magic.Print("you found 1 live.", 22, 34, 20);
			player.Lives++;
			(obj.Value as Live).Despawn();
		}

		private static void Enemy(Player player, Target enemy)
		{
			Magic.ClearMsg(0);
			new Fight(player, enemy);
		}

		public static void PrintMap(Player player, Target obj)
		{
			var posX = (Console.WindowWidth/2 - 30);
			var posY = 8;
			for (var i = 0; i < Map.Labyrinth.GetLength(0); ++i, Console.WriteLine())
			{
				Console.SetCursorPosition(posX, posY++);
				for (var j = 0; j < Map.Labyrinth.GetLength(1); ++j)
				{
					if (Map.Labyrinth[i, j] is Wall)
					{
						Console.ForegroundColor = ConsoleColor.DarkGray;
					}
					else if (Map.Labyrinth[i, j] is Live)
					{
						Console.ForegroundColor = ConsoleColor.Red;
					}
					else if (Map.Labyrinth[i, j] is Door)
					{
						Console.ForegroundColor = ConsoleColor.White;
					}
					else if (Map.Labyrinth[i, j] is Player)
					{
						Console.ForegroundColor = ConsoleColor.Green;
					}
					Map.Labyrinth[i, j].PrintObjectSymbol();
					Console.ResetColor();
				}
			}
		}

		public static void PrintPlayerInfo(Player player, Target obj)
		{
			var posX = 2;
			var posY = 8;
			Magic.ClearPlayerInfo();
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("----------------");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(" Player: ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("Nick: {0}", player.Name);
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("Lives: {0}", player.Lives);
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine("Kills: {0}", player.Kills);

			Console.SetCursorPosition(posX, posY += 2);
			Console.WriteLine(" Inventory: ");
			Console.SetCursorPosition(posX, posY++);
			foreach (var entry in player.Inventory)
			{
				Console.SetCursorPosition(posX, posY++);
				entry.Print();
			}
		}
	}
}
