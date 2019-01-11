using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;

namespace Test_Game
{
	class Player : MyEntity
	{
		public Player(Coord position) : base(GetAnimation(), position, MyMap.Layer.MONSTERS, false, true)
		{
			Moved += OnPlayerMoved;

			SadConsoleEntity.UseKeyboard = true;
			SadConsoleEntity.KeyboardHandler += MapKeyboardHandler;
		}

		private void OnPlayerMoved(object sender, ItemMovedEventArgs<GameObject> e)
		{
			if (CurrentMap != null)
				CurrentMap.MapConsole.CenterViewportOn(Position);
		}

		private bool MapKeyboardHandler(Console con, SadConsole.Input.Keyboard keyboard)
		{
			Direction dirToMove = Direction.NONE;

			if (keyboard.IsKeyPressed(Keys.NumPad8))
				dirToMove = Direction.UP;
			else if (keyboard.IsKeyPressed(Keys.NumPad9))
				dirToMove = Direction.UP_RIGHT;
			else if (keyboard.IsKeyPressed(Keys.NumPad6))
				dirToMove = Direction.RIGHT;
			else if (keyboard.IsKeyPressed(Keys.NumPad3))
				dirToMove = Direction.DOWN_RIGHT;
			else if (keyboard.IsKeyPressed(Keys.NumPad2))
				dirToMove = Direction.DOWN;
			else if (keyboard.IsKeyPressed(Keys.NumPad1))
				dirToMove = Direction.DOWN_LEFT;
			else if (keyboard.IsKeyPressed(Keys.NumPad4))
				dirToMove = Direction.LEFT;
			else if (keyboard.IsKeyPressed(Keys.NumPad9))
				dirToMove = Direction.UP_LEFT;

			if (dirToMove != Direction.NONE)
			{
				Program.Player.MoveIn(dirToMove);
				return true;
			}

			return false;
		}

		private static AnimatedConsole GetAnimation()
		{
			var anim = new AnimatedConsole("player", 1, 1);
			anim.CreateFrame();
			anim.CurrentFrame.SetGlyph(0, 0, '@');
			anim.CurrentFrame.SetForeground(0, 0, Color.Red);

			return anim;
		}
	}
}
