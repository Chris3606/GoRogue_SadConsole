using GoRogue.MapGeneration;
using GoRogue.MapViews;
using GoRogue_SadConsole;
using Microsoft.Xna.Framework;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace Test_Game
{
	static class MapCreation
	{
		// TODO: This could be replaced in the library with the blueprint system from GoRogue-SadConsole integration
		public static MyMap DungeonMap(int width, int height, XnaRect viewport, int maxRooms, int roomMinSize, int roomMaxSize, int attemptsPerRoom = 10)
		{
			var map = new MyMap(width, height, viewport);

			var grMap = new ArrayMap<bool>(width, height);
			QuickGenerators.GenerateRandomRoomsMap(grMap, maxRooms, roomMinSize, roomMaxSize, attemptsPerRoom);

			foreach (var pos in grMap.Positions())
				if (grMap[pos])
					map.SetTerrain(new Terrain(pos, Color.White, Color.Transparent, '.', true, true));
				else
					map.SetTerrain(new Terrain(pos, Color.White, Color.Transparent, '#', false, false));

			return map;
		}
	}
}
