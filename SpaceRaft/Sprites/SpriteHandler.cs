﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LYA.Helpers;

namespace LYA.Sprites
{
		public class SpriteHandler : SpriteComponent
		{
				public Texture2D Texture;

				private Vector2 origin;
				public Vector2 Position;
				public float scale;
				public float RotationVelocity;
				protected float Rotation;
				public bool InScene;

				public SpriteHandler(Texture2D texture)
				{
						this.Texture=texture;
						origin=new Vector2(texture.Width/2, texture.Height/2);
						scale=2f;
						InScene=true;
				} 

				public Rectangle Rectangle
				{
						get
						{
								return new Rectangle((int) -Texture.Width, (int) -Texture.Height, Texture.Width, Texture.Height);
						}

						set
						{
						}
				}

				public float Rotate
				{
						get; set;
				}

				public override void Draw()
				{
						if (Texture !=null)
								Globals.SpriteBatch.Draw(Texture, Position, Rectangle, Color.White, Rotate, origin, scale, SpriteEffects.None, 0);

				}
				public override void Update ( )
				{

				}
		}
}
