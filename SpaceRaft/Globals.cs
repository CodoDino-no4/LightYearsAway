﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceRaft
{
		public static class Globals
		{
				public static ContentManager Content
				{
						get; set;
				}
				public static SpriteBatch SpriteBatch
				{
						get; set;
				}
				public static float ElapsedSeconds
				{
						get; set;
				}
				public static Vector2 AstroPosition
				{
						get; set;
				}

				public static Vector2 ScreenSize
				{
						get; set;
				}

				public static void Update(GameTime gameTime)
				{
						ElapsedSeconds=(float) gameTime.ElapsedGameTime.TotalSeconds;
				}
		}
}
