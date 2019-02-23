using GoRogue;
using GoRogue_SadConsole;
using Microsoft.Xna.Framework;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SadConsole.Input;
using SadConsole;

namespace Test_Game
{
	class Player : Entity
	{
		public Player(Coord position)
			: base('@', Color.White, position, 1, false, true)
		{
			UseKeyboard = true;

			Moved += Player_Moved;
		}

		// Handle viewport sync
		private void Player_Moved(object sender, ItemMovedEventArgs<GoRogue.GameFramework.IGameObject> e)
		{
			if (CurrentMap == TestGame.CurrentMap)
				TestGame.MapConsole.CenterViewPortOnPoint(Position);
		}

		public override bool ProcessKeyboard(Keyboard info)
		{
			Direction dirToMove = Direction.NONE;

			if (info.IsKeyPressed(Keys.NumPad8))
				dirToMove = Direction.UP;
			else if (info.IsKeyPressed(Keys.NumPad9))
				dirToMove = Direction.UP_RIGHT;
			else if (info.IsKeyPressed(Keys.NumPad6))
				dirToMove = Direction.RIGHT;
			else if (info.IsKeyPressed(Keys.NumPad3))
				dirToMove = Direction.DOWN_RIGHT;
			else if (info.IsKeyPressed(Keys.NumPad2))
				dirToMove = Direction.DOWN;
			else if (info.IsKeyPressed(Keys.NumPad1))
				dirToMove = Direction.DOWN_LEFT;
			else if (info.IsKeyPressed(Keys.NumPad4))
				dirToMove = Direction.LEFT;
			else if (info.IsKeyPressed(Keys.NumPad7))
				dirToMove = Direction.UP_LEFT;


			if (dirToMove != Direction.NONE)
			{
				MoveIn(dirToMove);
				return true;
			}

			return false;
		}
	}
}
