using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceRaft.Sprites
{
		class Junk: SpriteHandler
		{
				private float speed;
				private Random rand;
				public Junk(Texture2D texture) : base(texture)
				{
						rand=new Random();
						RotationVelocity=rand.Next(2, 7);						speed=rand.Next(0, 5);
				}

				public override void Update()
				{
						JunkMovement();
				}

				public void JunkMovement()
				{
						//Rotation-=MathHelper.ToRadians(RotationVelocity);

						//if (Position.X<=70||Position.Y<=70)
						//{
						//		Position.X+=speed/8;
						//		Position.Y+=speed/8;

						//}
				}
		}
}
