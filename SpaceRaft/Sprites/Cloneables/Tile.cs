using Microsoft.Xna.Framework.Graphics;
using System;

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

				public Tile( Texture2D texture ) : base( texture )
				{
						Texture=texture;
				}

				public override void Draw()
				{
				}

				public override void Update()
				{

				}
		}
}
