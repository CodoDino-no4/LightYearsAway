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
				private Vector2 startPos;
				private int maxPoint;

				private Texture2D tex;

				public AutoAstro( Texture2D texture, int clientId, int maxPoint, Vector2 startPos ) : base( texture, clientId )
				{
						leftDone=false;
						downDone=false;
						rightDone=false;
						upDone=false;
						placeDone=false;
						tex=Globals.Content.Load<Texture2D>( "foundation" );
						this.startPos = startPos;
						this.maxPoint = maxPoint;
				}

				public override void Update()
				{
						Movement();
						//Place();
				}

				public void Movement()
				{
						if (!rightDone)
						{
								if (Position.X<=startPos.X+maxPoint)
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
										if (Position.Y<=startPos.Y+maxPoint)
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
												if (Position.X>=-startPos.X+maxPoint)
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
														if (Position.Y>=-startPos.Y+maxPoint)
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
						Random rand = new Random();

						int randXPlus = rand.Next( (int) startPos.X, maxPoint);
						int randXMinus = rand.Next( (int) startPos.X, -maxPoint);
						int randYPlus = rand.Next( (int) startPos.Y, maxPoint);
						int randYMinus = rand.Next( (int) startPos.Y, -maxPoint);

						if (!placeDone)
						{
								List<Vector2> tilePos = new List<Vector2>()
								{
										new Vector2(randXPlus, randYPlus),
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
