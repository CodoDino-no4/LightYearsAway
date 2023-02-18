﻿using Apos.Input;
using LYA._Camera;
using LYA.Commands;
using LYA.Helpers;
using LYA.Managers;
using LYA.Sprites;
using LYA.Sprites.Background;
using LYA.Sprites.Cloneables;
using LYA.Sprites.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LYA
{
		public class LYA : Game
		{
				// Graphics
				private GraphicsDeviceManager graphics;
				private SpriteBatch spriteBatch;
				private bool isFullscreen;

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
				private List<BaseSprite> sprites;
				private List<BaseSprite> uiSprites;

				CommandInput commandInput;

				public LYA()
				{
						// Graphics manager
						graphics=new GraphicsDeviceManager( this );

						// Content directory
						Content.RootDirectory="Content";
						Globals.Content=Content;
				}

				protected override void Initialize()
				{
						// Graphics settings
						isFullscreen=false;
						graphics.SynchronizeWithVerticalRetrace=true;
						graphics.PreferredBackBufferWidth=2000;
						graphics.PreferredBackBufferHeight=1000;
						graphics.IsFullScreen=isFullscreen;

						Window.AllowUserResizing=true;
						Window.Title="Light-Years Away";
						IsMouseVisible=true;

						graphics.ApplyChanges();

						// Set inital viewport 
						Globals.ScreenSize=graphics.GraphicsDevice.Viewport.Bounds;

						// Camera
						camera=new Camera();

						// Managers
						bgManager=new BGManager();

						commandInput=new CommandInput();

						// Create a new SpriteBatch
						spriteBatch=new SpriteBatch( GraphicsDevice );
						Globals.SpriteBatch=spriteBatch;

						// Sprite List
						sprites=new List<BaseSprite>();
						uiSprites=new List<BaseSprite>();

						base.Initialize();
				}

				protected override void LoadContent()
				{
						InputHelper.Setup( this );

						// Background content
						bgInfinateShader=Content.Load<Effect>( "infinite" );

						bg1=Globals.Content.Load<Texture2D>( "BG1-320px" );
						bg2=Globals.Content.Load<Texture2D>( "BG2-320px" );
						bgManager.AddElement( new BGLayer( bg1 ) );
						bgManager.AddElement( new BGLayer( bg2 ) );

						// UI content
						toolBelt=Globals.Content.Load<Texture2D>( "toolbelt-empty" );

						// add sprites to list
						uiSprites.Add( new Toolbelt( toolBelt ) );

						// Player Astro content
						astroIdleTex=Globals.Content.Load<Texture2D>( "Astro-Idle" );
						astro=new Astro( astroIdleTex );

						foundationTex=Globals.Content.Load<Texture2D>( "foundation" );

						// add sprites to list
						sprites.Add(astro);

						//commandInput.PlaceTile(astro, tex, sprites)

				}
				protected override void Draw( GameTime gameTime )
				{
						// Background colour
						//GraphicsDevice.Clear( Color.SlateGray );

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
								sprite.Draw(sprites);

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * UI Layer Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: ui_scale );

						foreach (var sprite in uiSprites)
								sprite.Draw( uiSprites );

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						base.Draw( gameTime );
				}

				protected override void Update( GameTime gameTime )
				{
						InputHelper.UpdateSetup();

						// Update the camera
						camera.UpdateCameraInput( commandInput.PlayerCameraMovement( astro ) );

						//Update BG sprites
						bgManager.Update();

						commandInput.PlaceTile( astro, foundationTex, sprites );

						// Update sprites
						foreach (var sprite in sprites)
								sprite.Update();

						//Update UI Sprites
						foreach (var sprite in uiSprites)
								sprite.Update();

						HasQuit();

						Globals.Update( gameTime, graphics );
						InputHelper.UpdateCleanup();
						base.Update( gameTime );
				}

				protected override void UnloadContent()
				{
						//base.UnloadContent();
				}

				private void HasQuit()
				{
						if (InputBindings.Quit().Pressed())
								Exit();
				}
		}
}
