using System;
using System.Collections.Generic;
using System.IO;

namespace TheGame
{
	public static class Map
	{
		private static readonly LabyrinthTxt Temp = new LabyrinthTxt();

		public static IMapObject[,] Labyrinth = Temp.Labyrinth;

		private class LabyrinthTxt
		{
			public IMapObject[,] Labyrinth;

			public LabyrinthTxt()
			{
				try
				{
					var stream = new StreamReader("../../Map.txt");
					var document = stream.ReadToEnd();
					stream.Close();

					var line = document.Split('\n');
					var symb = line[0].Split(' ');
					Labyrinth = new IMapObject[line.Length, symb.Length];

					for (var i = 0; i < line.Length; i++)
					{
						symb = line[i].Split(' ');
						for (var j = 0; j < symb.Length; j++)
						{
							switch (symb[j])
							{
								case "0":
									Labyrinth[i, j] = new None(new Coord(i, j));
									break;
								case "#":
									Labyrinth[i, j] = new Wall(new Coord(i, j));
									break;
								case "2":
									Labyrinth[i, j] = new Door(new Coord(i, j));
									break;
								case "3":
									Labyrinth[i, j] = new Live(new Coord(i, j));
									break;
								case "4":
									// Labyrinth[i, j] = new Enemy(new Coord(i, j));
									var dice = Magic.Random.Next(0, 4);
									var bag = new List<ItemObject> {new Key()};
									Labyrinth[i, j] = new Enemy(new Coord(i, j), UnitClasses.List.Unit[dice], bag);
									break;
								default:
									Labyrinth[i, j] = new None(new Coord(i, j));
									break;
							}
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}