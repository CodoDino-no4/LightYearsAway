using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace LYA.Sprites
{
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
								rectangle=new Rectangle( -Texture.Width, -Texture.Height, Texture.Width, Texture.Height );
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

				public virtual void Draw( Deque<BaseSprite> sprites )
				{
						foreach (BaseSprite sprite in sprites)
						{
								if (Texture!=null)
										Globals.SpriteBatch.Draw( Texture, Position, Rectangle, Color.White, rotation, Origin, Scale, SpriteEffects.None, 0 );
						}
				}
				public virtual void Update()
				{

				}
		}
}
