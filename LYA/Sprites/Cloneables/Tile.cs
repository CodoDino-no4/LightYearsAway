using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using System.Diagnostics;

namespace LYA.Sprites.Cloneables
{
		public class Tile : CloneableSprite
		{
				public enum Type
				{
						foundation,
						wall,
						roof,
						window,
						wallItem,
						floorItem,
						door
				};

				public Type type;

				public Vector2 Direction;

				public Tile( Texture2D texture ) : base( texture )
				{
						Origin=new Vector2( Rectangle.Width/2, Rectangle.Height/2 );
						Texture=texture;
						type=(Type)Enum.Parse(typeof(Type), Texture.Name);
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
