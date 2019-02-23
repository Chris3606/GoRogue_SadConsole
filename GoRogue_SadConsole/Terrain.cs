using System;
using System.Collections.Generic;
using SadConsole;
using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;

namespace GoRogue_SadConsole
{
	public class Terrain : Cell, IGameObject
	{
		private IGameObject _backingField;

		// TODO: Other Cell constructor lookalikes
		public Terrain(int glyph, Color foreground, Coord position, bool isWalkable, bool isTransparent)
			: base(foreground, Color.Transparent, glyph)
		{
			_backingField = new GameObject(position, 0, this, true, isWalkable, isTransparent);
		}

		#region IGameObject Implementation
		GoRogue.GameFramework.Map IGameObject.CurrentMap => _backingField.CurrentMap;
		public Map CurrentMap => (Map)_backingField.CurrentMap;

		public bool IsStatic => _backingField.IsStatic;

		public bool IsTransparent { get => _backingField.IsTransparent; set => _backingField.IsTransparent = value; }
		public bool IsWalkable { get => _backingField.IsWalkable; set => _backingField.IsWalkable = value; }
		public Coord Position { get => _backingField.Position; set => _backingField.Position = value; }

		public uint ID => _backingField.ID;

		public int Layer => _backingField.Layer;

		public event EventHandler<ItemMovedEventArgs<IGameObject>> Moved
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
