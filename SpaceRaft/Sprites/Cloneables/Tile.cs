using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace LYA.Sprites.Cloneables
{
		public class Tile : CloneableSprite
		{
				private enum Type
				{
						foundation,
						wall,
						roof,
						window,
						wallItem,
						floorItem,
						door
				};

				public Vector2 Direction;
				public float linearVelocity;

				public Tile( Texture2D texture ) : base( texture )
				{
						Rectangle=new Rectangle( -Texture.Width, -Texture.Height, Texture.Width, Texture.Width );
						Origin=new Vector2( Rectangle.Width/2, Rectangle.Height/2 );
						Texture=texture;
				}

				public void Draw( Bag<Tile> sprites )
				{
						foreach (BaseSprite sprite in sprites)
						{
								if (Texture!=null)
										Globals.SpriteBatch.Draw( Texture, Position, Rectangle, Color.White, rotation, Origin, Scale, SpriteEffects.None, 0 );
						}
				}

				public override void Update()
				{

				}
		}
}
