using GoRogue;
using GFramework = GoRogue.GameFramework;
using SadConsole;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace GoRogue_SadConsole
{
	public class Map : GFramework.Map
	{
		/// <summary>
		/// Exposes the terrain of the map as a SadConsole console. You may change each existing Cell (or use SadConsole functions that do so), however DO NOT assign
		/// a new Cell instance directly to the console -- instead, assign new terrain via the SetTerrain function.
		/// </summary>
		public MapConsole MapConsole => (MapConsole)Terrain;

		public Map(MapConsole console, int numberOfLayers, Distance distanceMeasurement, uint layersBlockingWalkability = uint.MaxValue,
				   uint layersBlockingTransparency = uint.MaxValue, uint entityLayersSupportingMultipleItems = 0)
			: base(console, numberOfLayers - 1, distanceMeasurement, layersBlockingWalkability, layersBlockingTransparency, entityLayersSupportingMultipleItems)
		{
			MapConsole.Map = this; // Has to be here because "this" is not available in the base call
			ObjectAdded += OnObjectAdded;
			ObjectMoved += OnEntityMoved; // Only non-terrain can move (IsStatic is set to false on terrain), so this is safe
			MapConsole.ViewPortChanged += OnViewportChanged;
		}

		public Map(int width, int height, XnaRect viewport, int numberOfLayers, Distance distanceMeasurement, uint layersBlockingWalkability = uint.MaxValue,
				   uint layersBlockingTransparency = uint.MaxValue, uint entityLayersSupportingMultipleItems = 0)
			: this(new MapConsole(width, height, Global.FontDefault, viewport), numberOfLayers, distanceMeasurement, layersBlockingWalkability,
				  layersBlockingTransparency, entityLayersSupportingMultipleItems)
		{ }

		private void OnEntityMoved(object sender, ItemMovedEventArgs<GFramework.GameObject> e)
		{
			SyncEntityVisibility((Entity)e.Item); // Can assume the object is an entity, only non-terrain moves.
		}

		private void OnObjectAdded(object sender, ItemEventArgs<GFramework.GameObject> e)
		{
			if (e.Item.Layer == 0 && !(e.Item is Terrain))
				throw new InvalidOperationException($"Cannot add terrain objects of any type of other than {nameof(Terrain)} to {nameof(GoRogue_SadConsole)}.{nameof(Map)}");

			if (e.Item.Layer != 0 && !(e.Item is Entity))
				throw new InvalidOperationException($"Cannot add non-terrain objects of any type of other than {nameof(Entity)} to {nameof(GoRogue_SadConsole)}.{nameof(Map)}");

			if (e.Item.Layer == 0)
				MapConsole.IsDirty = true;
			else // Is entity, so sync it
				SyncEntityVisibility((Entity)e.Item);

		}

		private void OnViewportChanged(object s, EventArgs e)
		{
			foreach (var entity in Entities.Items.Cast<Entity>()) // These can all move
			{
				entity.SadConsoleEntity.PositionOffset = new Microsoft.Xna.Framework.Point(-MapConsole.ViewPort.Location.X, -MapConsole.ViewPort.Location.Y);
				SyncEntityVisibility(entity);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SyncEntityVisibility(Entity entity)
		{
			entity.SadConsoleEntity.IsVisible = MapConsole.ViewPort.Contains(entity.SadConsoleEntity.Position);
		}
	}
}
