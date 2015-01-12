using System;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TheGame
{
	public class Login
	{
		private String _name;
		private int _lives = 3;
		private int _kills = 0;

		private List<ItemObject> _inventory = new List<ItemObject>(); 

		public Login()
		{
			Config();
			Magic.PrintGameName();

			var mainMenu = new Menu("");
			mainMenu.AddSubMenu("Start", PlayerName);
			mainMenu.AddSubMenu("Info", Magic.GameInfo);
			mainMenu.Print((Console.WindowWidth / 2 - 5), 7);
		}

		public void Config()
		{
			Console.CursorVisible = false;
			Console.SetWindowSize(100, 50);
			Console.SetBufferSize(100, 50);
			Console.ResetColor();
			
			WinApi.SetConsoleTitleName("TheGame");
			WinApi.SetConsoleFont("Raster Fonts");
			WinApi.SetConsoleFontSize(8, 9);
			WinApi.SetConsoleWindowPosition(300, 200);
		}

		private void PlayerName(int x)
		{
			while (true)
			{
				Magic.ClearConsole();
				Magic.PrintGameName();

				Magic.Print("input name: ", (Console.WindowWidth/2 - 10), 8, 30);
				Console.ForegroundColor = ConsoleColor.Green;
				_name = Console.ReadLine();
				Console.ResetColor();

				const String pattern = @"^[a-zA-Z][a-zA-Z]{2,10}$";
				if (_name != null && Regex.Match(_name, pattern).Success)
				{
					PlayerClass();
				}
				else
				{
					Magic.Print("wrong name format!", (Console.WindowWidth / 2 - 9), 47, 30);
					Thread.Sleep(1000);
					continue;
				}
				break;
			}
		}

		public void PlayerClass()
		{
			Magic.ClearConsole();
			Magic.PrintGameName();

			var unitClassId = 0;
			var chooseClassMenu = new Menu("choice class: ");
			chooseClassMenu.AddSubMenu("Archer", delegate(int x)
			{
				unitClassId = x;
				chooseClassMenu.Clear();
			});
			
			chooseClassMenu.AddSubMenu("Swordsman", delegate(int x)
			{
				unitClassId = x;
				chooseClassMenu.Clear();
			});
			
			chooseClassMenu.AddSubMenu("Wizard", delegate(int x)
			{
				unitClassId = x;
				chooseClassMenu.Clear();
			});

			chooseClassMenu.AddSubMenu("Thief", delegate(int x)
			{
				unitClassId = x;
				chooseClassMenu.Clear();
			});
			chooseClassMenu.Print((Console.WindowWidth / 2 - 5), 8);

			_inventory.Add(new Key());
			_inventory.Add(new Key());
			TheGame(new Player(UnitClasses.List.Unit[unitClassId], _name, _lives, _kills, _inventory));
		}

		private static void TheGame(Player player)
		{
			if (player != null)
			{
				new Game(player);
			}
			else
			{
				throw new Exception("no player");
			}
		}
	}
}
