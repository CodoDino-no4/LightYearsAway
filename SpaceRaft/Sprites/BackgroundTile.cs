using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceRaft.Sprites
		{
		class BackgroundTile : Sprite
		{
				public Sprite Astro { get; set; }
				public float FollowDistance { get; set; }

				public BackgroundTile( Texture2D texture ) : base( texture )
				{
				}

				public override void Update( GameTime gameTime)
				{
						
				}
		}
}
