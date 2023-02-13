﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LYA.Sprites.Junk
{
		public class Junk : SpriteHandler
		{
				private Random rand;
				private float speed;
				private float direction;
				public Junk( Texture2D texture ) : base( texture )
				{
						this.Texture=texture;
						rand=new Random();
				}

				public override void Update()
				{
						JunkMovement();
				}

				public void JunkMovement()
				{
						RotationVelocity=rand.Next( 2, 7 );
						rotation-=MathHelper.ToRadians( RotationVelocity );




				}
		}
}