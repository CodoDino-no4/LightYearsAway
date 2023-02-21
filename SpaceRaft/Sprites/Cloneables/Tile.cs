using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

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
						Debug.WriteLine( "Rectangle TILE"+Rectangle );
				}

				public override void Update()
				{

				}
		}
}
