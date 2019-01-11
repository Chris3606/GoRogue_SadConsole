using GoRogue;
using GoRogue_SadConsole;
using SadConsole;

namespace Test_Game
{
	class MyEntity : Entity
	{
		public new MyMap.Layer Layer { get => (MyMap.Layer)base.Layer; }
		public new MyMap CurrentMap { get => (MyMap)base.CurrentMap; }

		public MyEntity(AnimatedConsole animation, Coord position, MyMap.Layer layer, bool isWalkable = true, bool isTransparent = true)
			: base(animation, position, (int)layer, isWalkable, isTransparent)
		{
		}
	}
}
