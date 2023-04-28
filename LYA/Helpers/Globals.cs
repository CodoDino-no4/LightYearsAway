using LYA.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace LYA.Helpers
{
		/// <summary>
		/// Global variables
		/// </summary>
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
				public static Rectangle ScreenSize
				{
						get; set;
				}

				public static int ClientId
				{
						get; set;
				}

				public static bool IsMulti
				{
						get; set;
				}

				public static bool testing
				{
						get; set;
				}

				public static PacketFormer Packet
				{
						get; set;
				}


				public static void Update( GraphicsDeviceManager graphics )
				{
						ScreenSize=graphics.GraphicsDevice.Viewport.Bounds;
				}
		}
}
