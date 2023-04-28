using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites.Cloneables
{
		/// <summary>
		/// Tile sprite
		/// </summary>
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
						type=(Type) Enum.Parse( typeof( Type ), Texture.Name );
				}

				public override void Update()
				{

				}
		}
}
