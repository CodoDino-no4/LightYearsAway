using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceRaft.Sprites
{
		public class Astro: Sprite
		{
				public bool hasDied = false;
				public int health
				{
						get; set;
				}
				public bool isDead
				{
						get
						{
								return health<=0;
						}
				}

				public Astro(Texture2D texture) : base(texture)
				{

						state="Idle";
						Rotation=1f;
				}

				public override void Update(GameTime gameTime)
				{

						MoveWithCamera();
				}

				public Vector2 MoveWithCamera()
				{
						_previousKey=_currentKey;
						_currentKey=Keyboard.GetState();

						var direction = new Vector2((float) Math.Cos(rotation), (float) Math.Sin(rotation));

						if (_currentKey.IsKeyDown(Keys.Up))
								Position.Y-=direction.Y*LinearVelocity;

						if (_currentKey.IsKeyDown(Keys.Down))
								Position.Y+=direction.Y*LinearVelocity;

						if (_currentKey.IsKeyDown(Keys.Left))
								Position.X-=direction.X*LinearVelocity;

						if (_currentKey.IsKeyDown(Keys.Right))
								Position.X+=direction.X*LinearVelocity;

						return Position;
				}

				//track elapsed time since last frame, add since the game started
				// records the time that has elapsed since the last call to the update and other tracks the total time that the game has been running.
				//ElapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;
				//TotalSeconds += ElapsedSeconds;


				//if (kstate.IsKeyDown(Keys.Escape))
				//    Exit();

				//if (Input.IsKeyDown(Keys.Right))
				//{
				//    state = PlayerState.Walking;

				//    if (currentVelocity.X > maxVelocity.X)
				//        currentVelocity = maxVelocity;
				//    else
				//        currentVelocity += accelSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
				//}

				//if (state == PlayerState.Idle)
				//{
				//    if (currentVelocity.X > 0)
				//    {
				//        currentVelocity -= speedDecayRate * (float)gameTime.ElapsedGameTime.TotalSeconds;

				//        if (currentVelocity.X < 0)
				//            currentVelocity = Vector2.Zero;

				//    }

				//    if (currentVelocity.X < Vector2.Zero.X)
				//    {
				//        currentVelocity += speedDecayRate * (float)gameTime.ElapsedGameTime.TotalSeconds;

				//        if (currentVelocity.X > 0)
				//            currentVelocity = Vector2.Zero;
				//    }

				//}

		}
}
