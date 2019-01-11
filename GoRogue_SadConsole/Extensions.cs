using GoRogue;
using Microsoft.Xna.Framework;

namespace GoRogue_SadConsole
{
	public static class Extensions
	{
		public static Point ToPoint(this Coord position) => new Point(position.X, position.Y);
		public static Coord ToCoord(this Point position) => Coord.Get(position.X, position.Y);

		public static Microsoft.Xna.Framework.Rectangle ToMonogameRectangle(this GoRogue.Rectangle rectangle)
			=> new Microsoft.Xna.Framework.Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

		public static GoRogue.Rectangle ToGoRogueRectangle(this Microsoft.Xna.Framework.Rectangle rectangle)
			=> new GoRogue.Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
	}
}