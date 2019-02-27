using GoRogue;
using GoRogue_SadConsole;
using Microsoft.Xna.Framework;

namespace Test_Game
{
	internal static class TerrainFactory
	{
		public static Terrain Floor(Coord position) => new Terrain(Color.White, Color.Black, '.', position, true, true);

		public static Terrain Wall(Coord position) => new Terrain(Color.White, Color.Black, '#', position, false, false);
	}
}