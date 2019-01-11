using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using SadConsole;

namespace GoRogue_SadConsole.Components
{
	/// <summary>
	/// Customized SadConsole cell that can be a component of a GameObject.  Used in Terrain.
	/// </summary>
	public class TerrainCell : Cell
	{
		public GameObject GameObject { get; }

		// TODO: Support other constructors
		public TerrainCell(Color foreground, Color background, int glyph, GameObject parentGameObject)
			: base(foreground, background, glyph)
		{
			GameObject = parentGameObject;
		}
	}
}
