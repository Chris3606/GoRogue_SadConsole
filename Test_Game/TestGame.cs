﻿using GoRogue.MapViews;
using GoRogue_SadConsole;
using Microsoft.Xna.Framework;
using SadConsole;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace Test_Game
{
	internal class TestGame
	{
		public const int SCREEN_HEIGHT = 25;
		public const int SCREEN_WIDTH = 80;
		public static TestGameMap CurrentMap { get; private set; }
		public static ScrollingConsole MapConsole { get; private set; }
		public static Player Player { get; private set; }

		public static void Main(string[] args)
		{
			// Setup the engine and create the main window.
			SadConsole.Game.Create(SCREEN_WIDTH, SCREEN_HEIGHT);

			// Hook the start event so we can add consoles to the system.
			SadConsole.Game.OnInitialize = Init;

			// Start the game.
			SadConsole.Game.Instance.Run();

			// Code here will not run until the game window closes.

			SadConsole.Game.Instance.Dispose();
		}

		private static void Init()
		{
			// Generate map
			CurrentMap = TestGameMap.CreateDungeonMap(100, 100);

			// Entity to test layering
			var testItem = new Entity(Color.White, Color.Transparent, 'i', (2, 2), 1, true, true);
			CurrentMap.AddEntity(testItem);

			Player = new Player(CurrentMap.WalkabilityView.RandomPosition(true));
			CurrentMap.AddEntity(Player);

			MapConsole = new ScrollingConsole(width: CurrentMap.Width, height: CurrentMap.Height,
											  font: SadConsole.Global.FontDefault,
											   viewPort: new XnaRect(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT),
											   initialCells: CurrentMap.RenderingCellData);
			MapConsole.CenterViewPortOnPoint(Player.Position);

			CurrentMap.ConfigureAsRenderer(MapConsole);

			// Set our new console as the main object SadConsole processes, and set up keyboard input
			SadConsole.Global.CurrentScreen = MapConsole;
			SadConsole.Global.FocusedConsoles.Push(Player);
		}
	}
}