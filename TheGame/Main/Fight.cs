using System;
using System.Threading;

namespace TheGame
{
	public class Fight
	{
		public Fight(Player player, Target enemy)
		{
			var enemyTmp = (enemy.Value) as Enemy;

			player.PrintPlayerStats();
			enemyTmp.PrintEnemyStats();

			Initialize(player, enemyTmp);
		}

		private void Initialize(Player player, Enemy enemy)
		{
			if (player.Hp > 0 && enemy.Hp > 0)
			{
				Battle(player, enemy);
			}
			else
			{
				Magic.ClearMsg(0);
				if (player.Hp <= 0)
				{
					if (player.Lives > 0)
					{
						player.RestoreHp();
						player.Lives -= 1;
						Console.SetCursorPosition(40, 45);
						Console.WriteLine("you lost");
						Thread.Sleep(1000);
						Magic.ClearMsg(0);
					}
					else
					{
						Magic.PrintGameOver();
					}
				}

				if (enemy.Hp <= 0)
				{
					enemy.Despawn();
					player.RestoreHp();
					Console.SetCursorPosition(40, 45);
					Console.WriteLine("you win");
					Thread.Sleep(1000);
					Magic.ClearMsg(0);
				}
			}
		}

		private void Battle(Player player, Enemy enemy)
		{
			if (player.Attack != enemy.Block)
			{
				enemy.ReceiveDmg(player.Dmg());
			}
			else
			{
				Thread.Sleep(1000);
				Console.SetCursorPosition(40, 43);
				Console.WriteLine("enemy blocked attack");
			}

			if (enemy.Attack != player.Block)
			{
				player.ReceiveDmg(enemy.Dmg());
			}
			else
			{
				Thread.Sleep(1000);
				Console.SetCursorPosition(40, 43);
				Console.WriteLine("player blocked enemy attack");
			}

			Thread.Sleep(400);
			Magic.ClearMsg(0);
			player.PrintPlayerStats();
			enemy.PrintEnemyStats();

			Initialize(player, enemy);
		}
	}
}
