using System;
using System.Xml.Serialization;

namespace TheGame
{
	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = "", IsNullable = false)]
	public class Units
	{
		[XmlElement("Unit")]
		public Unit[] Unit { set; get; }
	}

	[XmlType(AnonymousType = true)]
	public class Unit
	{
		public String Type { set; get; }
		public int Hp { set; get; }
		public int Damage { set; get; }
		public int Defence { set; get; }
		public int Critical	{ set; get; }

		public Unit()
		{
			Type = "none";
			Hp = -1;
			Damage = -1;
			Defence = -1;
			Critical = -1;
		}

		public Unit(Unit obj)
		{
			Type = obj.Type;
			Hp = obj.Hp;
			Damage = obj.Damage;
			Defence = obj.Defence;
			Critical = obj.Critical;
		}

		public void RestoreHp()
		{
			switch (Type)
			{
				case "Archer":
					Hp = UnitClasses.List.Unit[0].Hp;
					break;
				case "Swordsman":
					Hp = UnitClasses.List.Unit[1].Hp;
					break;
				case "Wizard":
					Hp = UnitClasses.List.Unit[2].Hp;
					break;
				case "Thief":
					Hp = UnitClasses.List.Unit[3].Hp;
					break;
			}
		}

		public int Dmg()
		{
			var damage = Damage;
			var critical = Magic.Random.Next(0, Critical + 1);

			if (Magic.Random.Next(0, 3) == 0)
			{
				damage += critical;
				//Console.SetCursorPosition(40, 44);
				//Console.WriteLine("deals +{0} crit dmg!", critical);
			}
			return damage;
		}

		public void ReceiveDmg(int damage)
		{
			if (Magic.Random.Next(0, 3) == 0)
			{
				damage -= Defence;
				//Console.SetCursorPosition(40, 44);
				//Console.WriteLine("blocked {0} damage", Defence);
			}
			Hp -= damage;
			//Console.SetCursorPosition(40, 44);
			//Console.WriteLine("recieved {0} damage", damage);
		}
	}
}
