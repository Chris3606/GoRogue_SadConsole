using System;
using System.Collections.Generic;
using GoRogue.GameFramework;
using GoRogue.MapViews;
using GoRogue;
using Console = SadConsole.Console;
using SadConsole.Components;
using SadConsole;

namespace GoRogue_SadConsole
{
	public class Map : GoRogue.GameFramework.Map
	{

		private List<Console> _renderers;
		public IReadOnlyList<Console> Renderers => _renderers.AsReadOnly();

		// One of these per layer, so we force the rendering order to be what we want
		// (high layers appearing on top of low layers).  They're added to consoles in order of
		// this array, first to last, which controls the render order.
		private MultipleConsoleEntityDrawingComponent[] _entitySyncersByLayer;

		/// <summary>
		/// Exposed only to allow you to create consoles that use this as their rendering data.  DO NOT set new cells via this array -- use SetTerrain
		/// instead.
		/// </summary>
		public Cell[] RenderingCellData { get; }

		public Map(int width, int height, int numberOfEntityLayers, Distance distanceMeasurement, uint layersBlockingWalkability = uint.MaxValue,
				   uint layersBlockingTransparency = uint.MaxValue, uint entityLayersSupportingMultipleItems = uint.MaxValue)
			: base(CreateTerrain(width, height), numberOfEntityLayers, distanceMeasurement, layersBlockingWalkability,
				  layersBlockingTransparency, entityLayersSupportingMultipleItems)
		{
			// Cast it to what we know it really is and store it so we have the reference for later.
			RenderingCellData = ((ArrayMap<Terrain>)((LambdaSettableTranslationMap<Terrain, IGameObject>)Terrain).BaseMap);

			// Initialize basic components
			_renderers = new List<Console>();
			_entitySyncersByLayer = new MultipleConsoleEntityDrawingComponent[numberOfEntityLayers];
			for (int i = 0; i < _entitySyncersByLayer.Length; i++)
			_entitySyncersByLayer[i] = new MultipleConsoleEntityDrawingComponent();


			// Sync existing renderers when things are added
			ObjectAdded += GRMap_ObjectAdded;
			ObjectRemoved += GRMap_ObjectRemoved;
		}

		

		private void GRMap_ObjectAdded(object sender, ItemEventArgs<IGameObject> e)
		{
			if (e.Item is Entity entity)
				_entitySyncersByLayer[entity.Layer - 1].Entities.Add(entity);
			else if (e.Item.Layer == 0)
			{
				foreach (var renderer in _renderers)
					renderer.IsDirty = true;
			}
		}

		private void GRMap_ObjectRemoved(object sender, ItemEventArgs<IGameObject> e)
		{
			if (e.Item is Entity entity)
				_entitySyncersByLayer[entity.Layer - 1].Entities.Remove(entity);
		}

		/// <summary>
		/// Configures the given console to render the current map and its entities.  THe renderer MUST be using this map's cell data
		/// to back it.
		/// </summary>
		/// <param name="renderer">Render to configure.  Renderer MUST have its cells data be this Map's RenderingCellData.</param>
		public void ConfigureAsRenderer(Console renderer) // TODO: See about SetSurface to deal with parenting
		{
			if (renderer.Cells != RenderingCellData)
				throw new ArgumentException($"Cannot set a console to render a map that doesn't have the map's {nameof(RenderingCellData)} backing it.");

			_renderers.Add(renderer);
			foreach (var syncer in _entitySyncersByLayer)
				renderer.Components.Add(syncer);
			renderer.IsDirty = true; // Make sure we re-render - SadConsole bug doesn't set this when constructed
		}

		// Create new map, and return as something GoRogue understands
		private static ISettableMapView<IGameObject> CreateTerrain(int width, int height)
		{
			var actualTerrain = new ArrayMap<Terrain>(width, height);
			return new LambdaSettableTranslationMap<Terrain, IGameObject>(actualTerrain, t => t, g => (Terrain)g);
		}
	}
}
