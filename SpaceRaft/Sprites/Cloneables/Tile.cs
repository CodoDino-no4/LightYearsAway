using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
						this.Rectangle = new Rectangle( (int) -Texture.Width, (int) -Texture.Height, Texture.Width, Texture.Width);
						Position.Y-=Rectangle.Height/2;
						Texture=texture;
				}

				public override void Update()
				{

				}
		}
}
