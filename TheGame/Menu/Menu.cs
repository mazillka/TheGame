using System;
using System.Collections.Generic;
using System.Linq;

namespace TheGame
{
	public delegate void Method(int x);

	public class Menu
	{
		private readonly Dictionary<String, Method> _menuItems = new Dictionary<String, Method>();
		private readonly String _name;
		private int _printPosX;
		private int _printPosY;

		private bool _flag;

		public Menu(String name)
		{
			_name = name;
			_flag = true;
		}

		public void AddSubMenu(String name, Method method)
		{
			_menuItems.Add(name, method);
		}

		public void Print(int printPosX, int printPosY)
		{
			_printPosX = printPosX;
			_printPosY = printPosY;
			Navigation();
		}

		private void Navigation()
		{
			var currentItem = 0;
			var lastItem = _menuItems.Count - 1;
			PrintMenu(currentItem);
			while (_flag)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow:
						if (0 < currentItem)
						{
							currentItem -= 1;
						}
						else
						{
							currentItem = lastItem;
						}
						PrintMenu(currentItem);
						break;

					case ConsoleKey.DownArrow:
						if (currentItem < lastItem)
						{
							currentItem += 1;
						}
						else
						{
							currentItem = 0;
						}
						PrintMenu(currentItem);
						break;

					case ConsoleKey.Enter:
						_menuItems.ElementAt(currentItem).Value(currentItem);
						break;

					case ConsoleKey.Escape:
						WinApi.Exit();
						break;

					default:
						PrintMenu(currentItem);
						break;
				}
			}
		}

		private void PrintMenu(int menuItemId)
		{
			if(_menuItems != null)
			{
				var x = _printPosX;
				var y = _printPosY;
				Console.SetCursorPosition(x, y++);
				Console.WriteLine("{0}", _name);
				foreach (var entry in _menuItems)
				{
					Console.SetCursorPosition(x, y++);
					if (entry.Equals(_menuItems.ElementAt(menuItemId)))
					{
						Console.ForegroundColor = ConsoleColor.Green;
					}
					Console.WriteLine(" {0}", entry.Key);
					Console.ResetColor();
				}
			}
			else
			{
				throw new Exception("no submenu");
			}
		}

		public void Clear()
		{
			_flag = false;
			_menuItems.Clear();
		}
	}
}
