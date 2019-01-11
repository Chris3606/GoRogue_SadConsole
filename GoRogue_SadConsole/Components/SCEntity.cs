using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using SadConsole;

namespace GoRogue_SadConsole.Components
{
	/// <summary>
	/// Customized SadConsole Entity that can be a component of a GameObject.  Used in Entity.
	/// </summary>
	public class SCEntity : SadConsole.Entities.Entity
	{
		public GameObject GameObject { get; }

		// TODO: Support other constructors
		public SCEntity(AnimatedConsole animation, GameObject parentGameObject) : base(animation)
		{
			GameObject = parentGameObject;
		}

		protected override void OnPositionChanged(Point oldLocation)
		{
			if (Position.X != GameObject.Position.X || Position.Y != GameObject.Position.Y)
			{
				GameObject.Position = Position.ToCoord(); // Not an infinite loop with Entity handler, since this one will check if it's changed before setting
				if (Position.X != GameObject.Position.X || Position.Y != GameObject.Position.Y) // Movement failed, move back
					Position = oldLocation;
			}
			
		}
	}
}
