using LYA.Commands;
using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LYA.Sprites
{
		public class BaseSprite
		{
				public Texture2D Texture;

				private Vector2 origin;
				public Vector2 Position;
				public float Scale;
				protected float rotation;
				public bool InScene;

				public BaseSprite( Texture2D texture )
				{
						Texture=texture;
						origin=new Vector2( texture.Width/2, texture.Height/2 );
						Scale=2f;
						InScene=true;
				}

				private Rectangle rectangle;

				public Rectangle Rectangle
				{
						get
						{
								rectangle =new Rectangle( (int) -Texture.Width, (int) -Texture.Height, Texture.Width, Texture.Height );
								return rectangle;
						}

						set
						{
								rectangle=value;
						}
				}

				public virtual void Draw( List<BaseSprite> sprites )
				{
						foreach (BaseSprite sprite in sprites)
						{
								if (Texture!=null)
										Globals.SpriteBatch.Draw( Texture, Position, Rectangle, Color.White, rotation, origin, Scale, SpriteEffects.None, 0 );

						}

				}
				public virtual void Update()
				{

				}

				public static implicit operator BaseSprite( PlaceCommand v )
				{
						throw new NotImplementedException();
				}
		}
}
