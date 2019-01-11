using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using GoRogue_SadConsole.Components;

namespace GoRogue_SadConsole
{
	/// <summary>
	/// Represents a piece of terrain in a Map.  Contains a SadConsole cell that represents that terrain.
	/// </summary>
	public class Terrain : GameObject
	{
		public TerrainCell SadConsoleCell { get; }

		// TODO: Support other cell overloads
		public Terrain(Coord position, Color foreground, Color background, int glyph, bool isWalkable = true, bool isTransparent = true)
			: base(position, 0, true, isWalkable, isTransparent)
		{
			SadConsoleCell = new TerrainCell(foreground, background, glyph, this);
		}
	}
}
