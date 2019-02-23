﻿using GoRogue;
using GoRogue_SadConsole;
using Microsoft.Xna.Framework;

namespace Test_Game
{
	static class TerrainFactory
	{
		public static Terrain Wall(Coord position) => new Terrain('#', Color.White, position, false, false);
		public static Terrain Floor(Coord position) => new Terrain('.', Color.White, position, true, true);
	}
}