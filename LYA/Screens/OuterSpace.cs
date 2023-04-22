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
		/// <summary>
		/// Outer Space Game Screen
		/// </summary>
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
				private List<KeyValuePair<int, Vector2>> tmpClients;
				private bool playersAdded = false;

				private AutoAstro autoAstro;

				public OuterSpace( Game game, ClientManager clientManager ) : base( game )
				{
						// Camera
						camera=new Camera();

						// Managers
						bgManager=new BGManager();

						// Networking
						this.clientManager=clientManager;
						tmpClients=new List<KeyValuePair<int, Vector2>>();

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

						// Auto Astro
						if (Globals.testing)
						{
								List<Vector2> testPositions = new List<Vector2>()
								{
										new Vector2(10, 10),
										new Vector2(20, 20),
										new Vector2(30, 30),
										new Vector2(40, 40),

								};

								Random rand = new Random();
								var pos = rand.Next( 0, testPositions.Count() );

								autoAstro=new AutoAstro( astroIdleTex, 0, 40, testPositions[pos] );
						}

						astro=new Astro( astroIdleTex, Globals.ClientId );

						// Tiles content
						foundationTex=Globals.Content.Load<Texture2D>( "foundation" );
				}

				public override void Draw( GameTime gameTime )
				{
						// Matrix transformations
						Matrix projection = Matrix.CreateOrthographicOffCenter(Globals.ScreenSize, 0, 1);
						Matrix bg_transform = camera.GetBgTransform(bg1);
						Matrix ui_scale = camera.GetUIScale();

						// Shader setup
						bgInfinateShader.Parameters[ "view_projection" ].SetValue( Matrix.Identity*projection );
						bgInfinateShader.Parameters[ "uv_transform" ].SetValue( Matrix.Invert( bg_transform ) );

						/* Begin Spritebatch
						 * Infinate Background */

						Globals.SpriteBatch.Begin( effect: bgInfinateShader, samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform );

						// Draw Background
						bgManager.Draw();

						Globals.SpriteBatch.End();

						/* Begin Spritebatch
						 * Variable position Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform );

						// Draw Tiles
						foreach (var sprite in tileSprites)
								sprite.Draw();

						// Draw Multiplayer Astros
						foreach (var sprite in astroSprites)
								sprite.Draw();

						// Draw Astro
						if (Globals.testing)
						{
								autoAstro.Draw();
								autoAstro.tiles=tileSprites;
						}
						else
						{
								astro.Draw();
						}

						Globals.SpriteBatch.End();

						/* Begin Spritebatch
						 * UI Layer Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: ui_scale );

						// Draw UI
						foreach (var sprite in uiSprites)
								sprite.Draw();

						Globals.SpriteBatch.End();
				}

				public override void Update( GameTime gameTime )
				{
						// If multiplayer
						if (Globals.IsMulti&&tmpCount!=0)
						{
								// Add exisitng players on the server
								if (!playersAdded)
								{
										foreach (var client in clientManager.clients)
										{
												astroSprites.AddToFront( new Astro( astroIdleTex, client.id ) { Position=client.position } );
										}

										playersAdded=true;
								}

								// Player count has changed
								if (tmpCount!=clientManager.clients.Count())
								{
										// If player count has increased
										if (tmpCount<clientManager.clients.Count())
										{
												var client=clientManager.clients.Find( c => c.isAdded.Equals( false ) );

												astroSprites.AddToFront( new Astro( astroIdleTex, client.id ));

												client.isAdded=true;
										}
										// If player count has decreased
										else
										{
												foreach (var sprite in astroSprites)
												{
														var client=clientManager.clients.Find( c => c.hasLeft.Equals( true ) );

														if (sprite.clientId==client.id)
														{
																astroSprites.Remove( sprite );
																clientManager.clients.Remove( client );
																break;
														}
												}
										}
								}

								// Update astroSprites
								foreach (var sprite in astroSprites)
								{
										var client=clientManager.clients.Find( c => c.id.Equals( sprite.clientId ) );
										sprite.Position=client.position;
										sprite.Update();
								}

								// Add new tile to world
								if (clientManager.tileCoords.Key!=0)
								{
										bool emptyPos =true;

										foreach (var sprite in tileSprites)
										{
												if (clientManager.tileCoords.Value==sprite.Position)
												{
														emptyPos=false;
														break;
												}
										}

										if (emptyPos)
										{
												tileSprites.Add( new Tile( foundationTex ) { Position=clientManager.tileCoords.Value } );
										}
								}
						}

						// Update the camera
						camera.UpdateCameraInput( CommandManager.PlayerCameraMovement( astro ) );

						// Update BG Sprites
						bgManager.Update();

						// Update astro
						astro.Update();

						if (Globals.testing)
						{
								autoAstro.Update();
						}

						CommandManager.Commands( astro, foundationTex, tileSprites );

						// Update UI Sprites
						foreach (var sprite in uiSprites)
						{
								sprite.Update();
						}

						// Update tile Sprites
						foreach (var sprite in tileSprites)
						{
								sprite.Update();
						}

						// Temp player count set
						tmpCount=clientManager.clients.Count();
				}
		}
}
