using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceRaft.Sprites
		{
		class BackgroundTile : Sprite
				{

				public BackgroundTile( Texture2D texture ) : base( texture )
				{
						Origin = new Vector2( texture.Width / 2, texture.Height / 2 );
				}

				public override void Update( GameTime gameTime)
				{

				}

		}
}
