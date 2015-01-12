using System;
using System.Threading;

namespace TheGame
{
	public static class Magic
	{
		public static String GameStatus { set; get; }
		public static readonly Random Random = new Random();

		public static void Print(String message, int posX, int posY, int messagePrintSpeed)
		{
			Console.SetCursorPosition(posX, posY);
			foreach (var letter in message)
			{
				Thread.Sleep(messagePrintSpeed);
				Console.Write(letter);
			}
		}

		public static void ClearConsole()
		{
			Console.Clear();
		}

		public static void ClearPlayerInfo()
		{
			for (var posY = 8; posY < Console.WindowHeight - 1; posY++)
			{
				for (var posX = 2; posX < 20; posX++)
				{
					Console.SetCursorPosition(posX, posY);
					Console.Write(" ");
				}
			}
		}

		public static void ClearMsg(int pause)
		{
			for (var posY = 34; posY < Console.WindowHeight - 1; posY++)
			{
				for (var posX = 22; posX < Console.WindowWidth; posX++)
				{
					Console.SetCursorPosition(posX, posY);
					Thread.Sleep(pause);
					Console.Write(" ");
				}
			}
		}

		public static void GameInfo(int idx)
		{
			const String firstLine = "Coursework of Andrew Shevchuk-Yuganets";
			const String secondLine = "Copyright(c) 2015 Mazillka. All rights reserved.";
			Print(firstLine, (Console.WindowWidth / 2 - firstLine.Length / 2), 46, 30);
			Print(secondLine, (Console.WindowWidth / 2 - secondLine.Length / 2), 47, 30);
		}

		public static void PrintGameName()
		{
			var posY = 0;
			var posX = Console.WindowWidth / 2 - 50 / 2;
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"  _______ _           _____                      ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@" |__   __| |         / ____|                     ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"    | |  | |__   ___| |  __  __ _ _ __ ___   ___ ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"    | |  | '_ \ / _ \ | |_ |/ _` | '_ ` _ \ / _ \");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"    | |  | | | |  __/ |__| | (_| | | | | | |  __/");
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine(@"    |_|  |_| |_|\___|\_____|\__,_|_| |_| |_|\___|");
		}

		public static void PrintGameOver()
		{
			var posX = Console.WindowWidth / 2 - 54 / 2;
			var posY = Console.WindowHeight / 2 - 6;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"   _____                         ____                 ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"  / ____|                       / __ \                ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@" | |  __  __ _ _ __ ___   ___  | |  | |_   _____ _ __ ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@" | | |_ |/ _` | '_ ` _ \ / _ \ | |  | \ \ / / _ \ '__|");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@" | |__| | (_| | | | | | |  __/ | |__| |\ V /  __/ |   ");
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine(@"  \_____|\__,_|_| |_| |_|\___|  \____/  \_/ \___|_|   ");
			Console.ResetColor();

			if (Console.ReadKey().Key == ConsoleKey.Escape)
			{
				WinApi.Exit();
			}
		}

		public static void PrintYouWinGame()
		{
			var posX = Console.WindowWidth / 2 - 48 / 2;
			var posY = Console.WindowHeight / 2 - 6;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@" __     __          __          ___       _ _ _ ");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@" \ \   / /          \ \        / (_)     | | | |");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"  \ \_/ /__  _   _   \ \  /\  / / _ _ __ | | | |");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"   \   / _ \| | | |   \ \/  \/ / | | '_ \| | | |");
			Console.SetCursorPosition(posX, posY++);
			Console.WriteLine(@"    | | (_) | |_| |    \  /\  /  | | | | |_|_|_|");
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine(@"    |_|\___/ \__,_|     \/  \/   |_|_| |_(_|_|_)");
			Console.ResetColor();

			if (Console.ReadKey().Key == ConsoleKey.Escape)
			{
				WinApi.Exit();
			}
		}
	}
}
