using LYA.Helpers;
using LYA.Managers;
using LYA.Sprites.Background;
using LYA.Sprites.GUI;
using LYA.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYA.Networking;
using MonoGame.Extended.Collections;
using Apos.Input;
using LYA.Commands;
using LYA._Camera;

namespace LYA.Screens
{
		public class OuterSpace : GameScreen
		{

				// Camera
				private Camera camera;
				private Effect bgInfinateShader;

				// Managers
				private BGManager bgManager;

				// Textures
				private Texture2D bg1, bg2;
				private Texture2D astroIdleTex;
				private Texture2D toolBelt;
				private Texture2D foundationTex;

				// Sprite Objects
				public Astro astro;
				private Deque<BaseSprite> sprites;
				private Deque<BaseSprite> uiSprites;

				// Networking
				public ClientManager clientManager;

				public OuterSpace( Game game ) : base( game )
				{
						// Camera
						camera=new Camera();

						// Managers
						bgManager=new BGManager();

						// Sprite List
						sprites=new Deque<BaseSprite>();
						uiSprites=new Deque<BaseSprite>();
				}

				private new LYA Game => (LYA) base.Game;

				public override void LoadContent()
				{
						base.LoadContent();

						// Background content
						bgInfinateShader=Content.Load<Effect>( "infinite" );

						bg1=Globals.Content.Load<Texture2D>( "BG1-320px" );
						bg2=Globals.Content.Load<Texture2D>( "BG2-320px" );
						bgManager.AddElement( new BGLayer( bg1 ) );
						bgManager.AddElement( new BGLayer( bg2 ) );

						// UI content
						toolBelt=Globals.Content.Load<Texture2D>( "toolbelt-empty" );

						// add sprites to list
						uiSprites.AddToBack( new Toolbelt( toolBelt ) );

						// Player Astro content
						astroIdleTex=Globals.Content.Load<Texture2D>( "Astro-Idle" );
						astro=new Astro( astroIdleTex );

						Globals.playerCount=2;
						for (var i = 1; Globals.playerCount>i; i++)
						{
								sprites.AddToBack( new Astro(astroIdleTex) );
						}

						// add sprites to list
						sprites.AddToBack( astro );

						foundationTex=Globals.Content.Load<Texture2D>( "foundation" );
				}

				public override void Draw( GameTime gameTime )
				{
						Matrix projection = Matrix.CreateOrthographicOffCenter(Globals.ScreenSize, 0, 1);
						Matrix bg_transform = camera.GetBgTransform(bg1);
						Matrix ui_scale = camera.GetUIScale();

						bgInfinateShader.Parameters[ "view_projection" ].SetValue( Matrix.Identity*projection );
						bgInfinateShader.Parameters[ "uv_transform" ].SetValue( Matrix.Invert( bg_transform ) );

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * Infinate Background */

						Globals.SpriteBatch.Begin( effect: bgInfinateShader, samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform );

						bgManager.Draw();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * Variable position Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform );

						foreach (var sprite in sprites)
								sprite.Draw( sprites );

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * UI Layer Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: ui_scale );

						foreach (var sprite in uiSprites)
								sprite.Draw( uiSprites );

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////
				}

				public override void Update( GameTime gameTime )
				{
						// Update the camera
						camera.UpdateCameraInput( CommandManager.PlayerCameraMovement( astro ) );

						//Update BG sprites
						bgManager.Update();

						CommandManager.Commands( astro, foundationTex, sprites );

						// Update sprites
						foreach (var sprite in sprites)
								sprite.Update();

						//Update UI Sprites
						foreach (var sprite in uiSprites)
								sprite.Update();
				}
		}
}
