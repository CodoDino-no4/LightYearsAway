using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace LYA.Commands
{
		/// <summary>
		/// Command base class
		/// </summary>
		public static class CommandManager
		{
				public interface ICommand
				{
						protected void Execute();
				}

				/// <summary>
				/// Calculates the players position based on input
				/// </summary>
				public static Vector2 PlayerMovement( Astro astro )
				{
						// Execute Command
						var move = new MoveCommands(astro);
						move.Execute();

						return astro.Position;
				}

				/// <summary>
				/// Handles placing a tile
				/// </summary>
				public static void PlaceTile( Astro astro, Texture2D tileTex, Bag<Tile> sprites )
				{
						// Execute command
						if (InputBindings.Place().Pressed())
						{
								var place = new PlaceCommand(astro, tileTex, sprites);
								place.Execute();
						}
				}

				/// <summary>
				/// Executes all commands implemented
				/// </summary>
				public static void Commands( Astro astro, Texture2D tileTex, Bag<Tile> sprites )
				{
						PlaceTile( astro, tileTex, sprites );
				}
		}
}
