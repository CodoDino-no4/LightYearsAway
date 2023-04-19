using LYA._Camera;
using LYA.Commands;
using LYA.Helpers;
using LYA.Managers;
using LYA.Networking;
using LYA.Sprites;
using LYA.Sprites.Background;
using LYA.Sprites.Cloneables;
using LYA.Sprites.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Screens;

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
				private Deque<Astro> astroSprites;
				private Deque<BaseSprite> uiSprites;
				private Bag<Tile> tileSprites;

				// Networking
				public ClientManager clientManager;
				private int tmpCount;
				private bool playersAdded = false;

				public OuterSpace( Game game, ClientManager clientManager ) : base( game )
				{
						// Camera
						camera=new Camera();

						// Managers
						bgManager=new BGManager();

						// Networking
						this.clientManager=clientManager;

						// Sprite List
						astroSprites=new Deque<Astro>();
						uiSprites=new Deque<BaseSprite>();
						tileSprites=new Bag<Tile>();
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

						// add astroSprites to list
						uiSprites.AddToBack( new Toolbelt( toolBelt ) );

						// Player Astro content
						astroIdleTex=Globals.Content.Load<Texture2D>( "Astro-Idle" );
						astro=new Astro( astroIdleTex, 1 )
						{
								clientId=Globals.ClientId
						};

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

						foreach (var sprite in tileSprites)
								sprite.Draw();

						foreach (var sprite in astroSprites)
								sprite.Draw();

						astro.Draw();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * UI Layer Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: ui_scale );

						foreach (var sprite in uiSprites)
								sprite.Draw();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////
				}

				public override void Update( GameTime gameTime )
				{
						if (Globals.IsMulti && tmpCount !=0)
						{
								// Add exisitng players on the server
								if (!playersAdded)
								{
										foreach (var client in clientManager.clients)
										{
												astroSprites.AddToFront( new Astro( astroIdleTex, client.Key ) { Position = client.Value } );
										}

										playersAdded = true;
								}

								// Player count has changed
								if (tmpCount!=Globals.PlayerCount)
								{
										// If player count has increased
										if (tmpCount<Globals.PlayerCount)
										{
												astroSprites.AddToFront( new Astro( astroIdleTex, clientManager.astroCoords.Key ) 
												{
														Position = new Vector2(clientManager.astroCoords.Value.X, clientManager.astroCoords.Value.X )

												} );
										}
										// If player count has decreased
										else { //if it exisits
												astroSprites.RemoveAt( clientManager.astroCoords.Key-1 );
										}
								}

								// Update astroSprites
								foreach (var sprite in astroSprites)
								{
										if (sprite.clientId==clientManager.astroCoords.Key)
										{
												sprite.Position=clientManager.astroCoords.Value;
										}
										sprite.Update();
								}
						}

						// Update the camera
						camera.UpdateCameraInput( CommandManager.PlayerCameraMovement( astro ) );

						//Update BG astroSprites
						bgManager.Update();

						//Update BG astroSprites
						astro.Update();

						CommandManager.Commands( astro, foundationTex, tileSprites );

						//Update UI Sprites
						foreach (var sprite in uiSprites)
						{
								sprite.Update();
						}

						//Update tile Sprites
						foreach (var sprite in tileSprites)
						{
								sprite.Update();
						}

						// Temp player count set
						tmpCount=Globals.PlayerCount;
				}
		}
}
