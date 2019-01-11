using GoRogue;
using GoRogue.GameFramework;
using SadConsole;

namespace GoRogue_SadConsole
{
	/// <summary>
	/// Type of GameObject that holds a reference to a SadConsole Entity.
	/// </summary>
	public class Entity : GameObject
	{
		public Components.SCEntity SadConsoleEntity { get; }
		public Entity(AnimatedConsole animation, Coord position, int layer, bool isWalkable = true, bool isTransparent = true)
			: base(position, layer, false, isWalkable, isTransparent)
		{
			SadConsoleEntity = new Components.SCEntity(animation, this);
			SadConsoleEntity.Position = position.ToPoint();
			Moved += OnEntityMoved;
		}

		private void OnEntityMoved(object s, ItemMovedEventArgs<GameObject> e)
		{
			SadConsoleEntity.Position = e.NewPosition.ToPoint(); // Not an infinite loop eiyh SCEntity handler, since this will fire only when the position actually changes
		}
	}
}
