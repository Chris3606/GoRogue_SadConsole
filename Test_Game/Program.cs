using Microsoft.Xna.Framework;
using XnaRect = Microsoft.Xna.Framework.Rectangle;
using GoRogue;
using GoRogue.MapViews;
using SadConsole;

namespace Test_Game
{
	class Program
	{
		public const int SCREEN_WIDTH = 80;
		public const int SCREEN_HEIGHT = 25;

		public static MyMap CurrentMap { get; private set; }
		public static Player Player { get; private set; }

		static void Main(string[] args)
		{
			// Setup the engine and create the main window.
			SadConsole.Game.Create(SCREEN_WIDTH, SCREEN_HEIGHT);

			// Hook the start event so we can add consoles to the system.
			SadConsole.Game.OnInitialize = Init;

			// Hook the update event that happens each frame so we can trap keys and respond.
			SadConsole.Game.OnUpdate = Update;

			// Start the game.
			SadConsole.Game.Instance.Run();

			//
			// Code here will not run until the game window closes.
			//

			SadConsole.Game.Instance.Dispose();
		}

		private static void Init()
		{
			// Any custom loading and prep. We will use a sample console for now

			CurrentMap = MapCreation.DungeonMap(100, 100, new XnaRect(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), 20, 4, 15);
			Coord playerSpawnPoint = CurrentMap.WalkabilityView.RandomPosition(true);
			Player = new Player(playerSpawnPoint);
			CurrentMap.AddEntity(Player);
				
			CurrentMap.MapConsole.CenterViewportOn(Player.Position);

			Global.CurrentScreen = CurrentMap.MapConsole;
			Global.FocusedConsoles.Push(Player.SadConsoleEntity);
		}

		private static void Update(GameTime time)
		{
			// Called each logic update.

			// As an example, we'll use the F5 key to make the game full screen
			if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
			{
				SadConsole.Settings.ToggleFullScreen();
			}
		}
	}
}
