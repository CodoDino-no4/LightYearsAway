﻿using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites
{
		/// <summary>
		/// Base class for all sprites to extend from
		/// </summary>
		public class BaseSprite
		{

				public Texture2D Texture;

				public Vector2 Origin;

				public Vector2 Position;

				public float Scale;
				protected float rotation;
				public bool InScene;

				private Rectangle rectangle;

				public Rectangle Rectangle
				{
						get
						{
								rectangle=new Rectangle( 0, 0, Texture.Width, Texture.Height );
								return rectangle;
						}

						set
						{
								rectangle=value;
						}
				}

				public BaseSprite( Texture2D texture )
				{
						Texture=texture;
						Origin=new Vector2( Rectangle.Width/2, Rectangle.Height/2 );
						Scale=2f;
						InScene=true;
				}

				public virtual void Draw()
				{

						if (Texture!=null)
								Globals.SpriteBatch.Draw( Texture, Position, Rectangle, Color.White, rotation, Origin, Scale, SpriteEffects.None, 0 );

				}
				public virtual void Update()
				{

				}
		}
}
