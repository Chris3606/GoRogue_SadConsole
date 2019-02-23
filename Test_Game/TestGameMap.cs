using GoRogue;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using GoRogue_SadConsole;
using System;

namespace Test_Game
{
	enum MapLayers { TERRAIN = 0, ITEMS, MONSTERS }

	class TestGameMap : Map
	{
		public TestGameMap(int width, int height)
			: base(width, height,
				  numberOfEntityLayers: Enum.GetNames(typeof(MapLayers)).Length - 1,
				  distanceMeasurement: Distance.CHEBYSHEV,
				  entityLayersSupportingMultipleItems: LayerMasker.DEFAULT.Mask((int)MapLayers.ITEMS))
		{ }

		public static TestGameMap CreateDungeonMap(int width, int height)
		{
			var map = new TestGameMap(width, height);
			var terrain = new ArrayMap<bool>(width, height);
			QuickGenerators.GenerateRectangleMap(terrain);

			foreach (var pos in terrain.Positions())
				if (terrain[pos])
					map.SetTerrain(TerrainFactory.Floor(pos));
				else
					map.SetTerrain(TerrainFactory.Wall(pos));

			return map;
		}
	}
}
