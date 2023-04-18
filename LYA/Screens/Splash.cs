using LYA.Helpers;
using LYA.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace LYA.Screens
{
		public class Splash : GameScreen
		{
				private Texture2D imgTex;
				private BaseSprite img;
				public Splash( Game game ) : base( game )
				{
				}

				private new LYA Game => (LYA) base.Game;

				public override void LoadContent()
				{
						base.LoadContent();

						imgTex=Globals.Content.Load<Texture2D>( "splash_screen" );

						img=new BaseSprite( imgTex );

				}
				public override void Draw( GameTime gameTime )
				{
						GraphicsDevice.Clear( Color.Black );

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap );

						Globals.SpriteBatch.Draw( imgTex, Globals.ScreenSize, Color.White );

						Globals.SpriteBatch.End();
				}

				public override void Update( GameTime gameTime )
				{
						//imgRectangle.X=Globals.ScreenSize.Width/2-imgTex.Width/2;
						//imgRectangle.Y=0;
						//imgRectangle.Width=imgTex.Width;
						//imgRectangle.Height=Globals.ScreenSize.Height;
				}
		}
}
