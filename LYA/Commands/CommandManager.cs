using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace LYA.Commands
{
		public static class CommandManager
		{
				public interface ICommand
				{
						protected void Execute();
				}

				public static Vector2 PlayerCameraMovement( Astro astro )
				{
						// Execute Command
						var move = new MoveCommands(astro);
						move.Execute();

						return astro.Position;
				}

				public static void PlaceTile( Astro astro, Texture2D tileTex, Bag<Tile> sprites )
				{
						// Execute command
						if (InputBindings.Place().Pressed())
						{
								var place = new PlaceCommand(astro, tileTex, sprites);
								place.Execute();
						}
				}

				public static void Commands( Astro astro, Texture2D tileTex, Bag<Tile> sprites )
				{
						PlaceTile( astro, tileTex, sprites );
				}
		}
}
