using System;
using System.Collections.Generic;
using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using SadConsole;

namespace GoRogue_SadConsole
{
	// TODO: Components and AddComponent is confusing, probably a solution somewhere via explicit interfaces.
	public class Entity : SadConsole.Entities.Entity, IGameObject
	{
		private IGameObject _backingField;

		#region Constructors
		public Entity(Color foreground, Color background, int glyph, Coord position, int layer, bool isWalkable, bool isTransparent)
			: base(foreground, background, glyph)
		{
			Initialize(position, layer, isWalkable, isTransparent);
		}

		public Entity(int width, int height, Coord position, int layer, bool isWalkable, bool isTransparent)
			: base(width, height)
		{
			Initialize(position, layer, isWalkable, isTransparent);
		}

		public Entity(int width, int height, Font font, Coord position, int layer, bool isWalkable, bool isTransparent)
			: base(width, height, font)
		{
			Initialize(position, layer, isWalkable, isTransparent);
		}

		public Entity(AnimatedConsole animation, Coord position, int layer, bool isWalkable, bool isTransparent)
			: base(animation)
		{
			Initialize(position, layer, isWalkable, isTransparent);
		}
		#endregion


		private void Initialize(Coord position, int layer, bool isWalkable, bool isTransparent)
		{
			_backingField = new GameObject(position, layer, this, false, isWalkable, isTransparent);
			base.Position = _backingField.Position;

			base.Moved += SadConsole_Moved;
			_backingField.Moved += GoRogue_Moved;
		}

		// Handle the case where GoRogue's position was the one that initiated the move
		private void GoRogue_Moved(object sender, ItemMovedEventArgs<IGameObject> e)
		{
			if ((Point)Position != base.Position) // We need to sync entity
				base.Position = Position;

			// SadConsole's Entity position set can't fail so no need to do other checks here
		}

		// Handle the case where you set the position when its casted to Entity
		private void SadConsole_Moved(object sender, EntityMovedEventArgs e)
		{
			if ((Point)Position != base.Position)
				Position = base.Position;

			if ((Point)Position != base.Position) // GoRogue wouldn't allow the position set
				base.Position = Position; // Set it back.  This shouldn't infinite loop because Position is still equal to the old base.Position
		}


		#region IGameObject Implementation
		GoRogue.GameFramework.Map IGameObject.CurrentMap => _backingField.CurrentMap;
		public Map CurrentMap => (Map)_backingField.CurrentMap;

		public bool IsStatic => _backingField.IsStatic;

		public bool IsTransparent { get => _backingField.IsTransparent; set => _backingField.IsTransparent = value; }
		public bool IsWalkable { get => _backingField.IsWalkable; set => _backingField.IsWalkable = value; }
		public new Coord Position { get => _backingField.Position; set => _backingField.Position = value; }

		public uint ID => _backingField.ID;

		public int Layer => _backingField.Layer;

		public new event EventHandler<ItemMovedEventArgs<IGameObject>> Moved
		{
			add => _backingField.Moved += value;
			remove => _backingField.Moved -= value;
		}

		public void AddComponent(object component) => _backingField.AddComponent(component);

		public T GetComponent<T>() => _backingField.GetComponent<T>();

		public IEnumerable<T> GetComponents<T>() => _backingField.GetComponents<T>();


		public bool HasComponent(Type componentType) => _backingField.HasComponent(componentType);

		public bool HasComponent<T>() => _backingField.HasComponent<T>();


		public bool HasComponents(params Type[] componentTypes) => _backingField.HasComponents(componentTypes);


		public bool MoveIn(Direction direction) => _backingField.MoveIn(direction);

		public void OnMapChanged(GoRogue.GameFramework.Map newMap) => _backingField.OnMapChanged(newMap);

		public void RemoveComponent(object component) => _backingField.RemoveComponent(component);

		public void RemoveComponents(params object[] components) => _backingField.RemoveComponents(components);
		#endregion
	}
}
