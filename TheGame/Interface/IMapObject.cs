namespace TheGame
{
	public interface IMapObject
	{
		void PrintObjectSymbol();

		void Despawn();

		void Spawn();

		event EventHandler OnDespawn;
	}
}
