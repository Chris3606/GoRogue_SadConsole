using System;
using XnaRect = Microsoft.Xna.Framework.Rectangle;
using GoRogue;
using Microsoft.Xna.Framework.Input;
using GoRogue_SadConsole;

namespace Test_Game
{
	// I want to use an enum for my layers and do some other things, so I do some custom initialization in this subclass
	class MyMap : Map
	{
		public enum Layer { TERRAIN = 0, ITEMS, MONSTERS };

		public MyMap(int width, int height, XnaRect viewport)
			: base(width, height, viewport,
				  numberOfLayers: Enum.GetNames(typeof(Layer)).Length,
				  distanceMeasurement: Distance.CHEBYSHEV,
				  layersBlockingWalkability: LayerMasker.DEFAULT.Mask((int)Layer.TERRAIN, (int)Layer.MONSTERS),
				  layersBlockingTransparency: LayerMasker.DEFAULT.Mask((int)Layer.TERRAIN, (int)Layer.MONSTERS),
				  entityLayersSupportingMultipleItems: LayerMasker.DEFAULT.Mask((int)Layer.ITEMS))
		{
		}
	}
}
