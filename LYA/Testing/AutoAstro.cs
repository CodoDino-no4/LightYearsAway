using LYA.Commands;
using LYA.Helpers;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using System.Diagnostics;

namespace LYA.Sprites
{
		public class AutoAstro : Astro
		{
				public enum State
				{
						idle,
						walk,
						swim,
				};

				public State state;

				public Vector2 Direction;

				public int clientId;

				public Bag<Tile> tiles
				{
						get; set;
				}

				private MoveUp moveUp;
				private MoveDown moveDown;
				private MoveRight moveRight;
				private MoveLeft moveLeft;
				private bool leftDone, downDone, rightDone, upDone, placeDone;

				private Texture2D tex;

				public AutoAstro( Texture2D texture, int clientId ) : base( texture, clientId )
				{
						leftDone=false;
						downDone=false;
						rightDone=false;
						upDone=false;
						placeDone=false;
						tex=Globals.Content.Load<Texture2D>( "foundation" );
				}

				public override void Update()
				{
						Movement();
						Place();
				}

				public void Movement()
				{
						if (!rightDone)
						{
								if (Position.X<=-100)
								{
										moveRight=new MoveRight( this );
										moveRight.Execute();
								}
								else
								{
										rightDone=true;
										downDone=false;
								}
						}
						else
						{

								if (!downDone)
								{
										if (Position.Y<=-100)
										{
												moveDown=new MoveDown( this );
												moveDown.Execute();
										}
										else
										{
												downDone=true;
												leftDone=false;
										}
								}
								else
								{

										if (!leftDone)
										{
												if (Position.X>=-900)
												{
														moveLeft=new MoveLeft( this );
														moveLeft.Execute();
												}
												else
												{
														leftDone=true;
														upDone=false;
												}
										}
										else
										{

												if (!upDone)
												{
														if (Position.Y>=-400)
														{
																moveUp=new MoveUp( this );
																moveUp.Execute();
														}
														else
														{
																upDone=true;
																rightDone=false;
														}
												}

										}

								}

						}
						Debug.WriteLine( Position );
				}
				public void Place()
				{
						if (!placeDone)
						{
								List<Vector2> tilePos = new List<Vector2>()
								{
										new Vector2(-513, -400),
										new Vector2(-99, -280),
										new Vector2(-600, -97),

								};

								foreach (var pos in tilePos)
								{
										if (pos==Position)
										{
												var place = new PlaceCommand(this, tex, tiles);
												place.Execute();
										}
								}

								if (tiles.Count().Equals( 3 ))
								{
										placeDone=true;
								}
						}

				}

		}
}
