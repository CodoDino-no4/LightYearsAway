using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites.ShipTile
{
		public class Tile : SpriteHandler
		{

				private Texture2D foundationTex;

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
				public int _type
				{
						get; set;
				}

				public Tile( Texture2D texture ) : base( texture )
				{
						this.Texture=texture;
				}

				public override void Draw()
				{
				}

				public override void Update()
				{

				}
		}
}
