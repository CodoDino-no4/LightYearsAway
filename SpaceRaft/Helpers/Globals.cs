using LYA.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace LYA.Helpers
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
				public static ScreenManager ScreenManager
				{
						get; set;
				}
				public static float ElapsedSeconds
				{
						get; set;
				}
				public static Rectangle ScreenSize
				{
						get; set;
				}

				public static int MaxPlayers
				{
						get; set;
				}

				public static int ClientId
				{
						get; set;
				}

				public static int PlayerCount
				{
						get; set;
				}

				public static bool IsMulti
				{
						get; set;
				}

				public static PacketFormer Packet
				{
						get; set;
				}


				public static void Update( GameTime gameTime, GraphicsDeviceManager graphics, ScreenManager screenManager )
				{
						ElapsedSeconds=(float) gameTime.TotalGameTime.TotalSeconds;

						ScreenSize=graphics.GraphicsDevice.Viewport.Bounds;

						ScreenManager=screenManager;

				}
		}
}
