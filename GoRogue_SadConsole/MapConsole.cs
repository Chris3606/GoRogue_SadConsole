using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapViews;
using Microsoft.Xna.Framework;
using XnaRect = Microsoft.Xna.Framework.Rectangle;
using SadConsole;
using GoRogue_SadConsole.Components;
using System;
using System.Linq;

namespace GoRogue_SadConsole
{
	/// <summary>
	/// SadConsole ScrollingConsole used to represent a Map, and controls rendering of that map.
	/// Feel free to use its functions to modify elements of the cells array, but DO NOT
	/// replace some element with a new Cell -- this is controlled by the corresponding Map's SetTerrain function.
	/// </summary>
	public class MapConsole : ScrollingConsole, ISettableMapView<GameObject>
	{
		public Map Map { get; internal set; }

		int IMapView<GameObject>.Width => base.Width;
		int IMapView<GameObject>.Height => base.Height;

		GameObject IMapView<GameObject>.this[Coord pos] => ((TerrainCell)base[pos.X, pos.Y]).GameObject;

		GameObject IMapView<GameObject>.this[int x, int y] => ((TerrainCell)base[x, y]).GameObject;

		GameObject ISettableMapView<GameObject>.this[Coord pos]
		{
			get => base[pos.X, pos.Y] == null ? null : ((TerrainCell)base[pos.X, pos.Y]).GameObject;
			set => base[pos.X, pos.Y] = ((Terrain)value).SadConsoleCell;
		}

		GameObject ISettableMapView<GameObject>.this[int x, int y]
		{
			get => base[x, y] == null ? null : ((TerrainCell)base[x, y]).GameObject;
			set => base[x, y] = ((Terrain)value).SadConsoleCell;
		}

		public event EventHandler ViewPortChanged;

		// TODO: Support other constructors
		public MapConsole(int width, int height, Font font, XnaRect viewPort)
			: base(width, height, font, viewPort, new TerrainCell[width * height])
		{
		}

		public override void Draw(TimeSpan timeElapsed)
		{
			base.Draw(timeElapsed);

			foreach (var entity in Map.Entities.Items.Cast<Entity>())
				entity.SadConsoleEntity.Draw(timeElapsed);
		}

		// TODO: FOV Support

		public void CenterViewportOn(Coord pos)
		{
			RectangleExtensions.CenterViewPortOnPoint(this, pos.ToPoint());
		}

		public void CenterViewportOn(Point pos)
		{
			RectangleExtensions.CenterViewPortOnPoint(this, pos);
		}


		protected override void OnViewPortChanged()
		{
			base.OnViewPortChanged();

			ViewPortChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
