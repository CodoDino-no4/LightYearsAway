using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LYA.Sprites.Cloneables
{
		public class Junk : CloneableSprite
		{
				private Random rand;
				private Vector2 direction;
				private float linearVelcocity;
				private float RotationVelocity;
				public Junk( Texture2D texture ) : base( texture )
				{
						Texture=texture;
						rand=new Random();
				}

				public override void Update()
				{
						JunkMovement();

						//if () junk leaves the (current visible area x no in all directions) remove

				}

				public void JunkMovement()
				{
						RotationVelocity=rand.Next( 2, 7 );
						linearVelcocity=rand.Next( 1, 5 )/8;
						direction.X=rand.Next( -1, 1 );
						direction.Y=rand.Next( -1, 1 );
						rotation-=MathHelper.ToRadians( RotationVelocity );

						Position.X+=direction.X*linearVelcocity;
						Position.Y+=direction.Y*linearVelcocity;

				}
		}
}
