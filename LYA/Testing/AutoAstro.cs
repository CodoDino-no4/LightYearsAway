using LYA.Commands;
using LYA.Helpers;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

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

				private Texture2D tex;
				public Bag<Tile> tiles
				{
						get; set;
				}

				private MoveUp moveUp;
				private MoveDown moveDown;
				private MoveRight moveRight;
				private MoveLeft moveLeft;

				private bool leftDone, downDone, rightDone, upDone, placeDone;

				// Auto movement params
				private Vector2 startPos;
				private int maxPoint;

				//Timer
				private Timer timer;
				private Random rand;

				public AutoAstro( Texture2D texture, int clientId, int maxPoint, Vector2 startPos ) : base( texture, clientId )
				{
						tex=Globals.Content.Load<Texture2D>( "foundation" );

						leftDone=false;
						downDone=false;
						rightDone=false;
						upDone=false;
						placeDone=false;

						this.startPos=startPos;
						this.maxPoint=maxPoint;

						rand=new Random();
						var interval = rand.Next( 2000, 4000 );

						timer=new Timer();
						timer.Interval=interval;
						timer.Enabled=true;

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
						timer.Elapsed+=( object source, ElapsedEventArgs e ) =>
						{
								var place = new PlaceCommand(this, tex, tiles);
								place.Execute();
						};
				}
		}
}
