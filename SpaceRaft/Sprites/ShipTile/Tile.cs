﻿using LYA.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites.ShipTile
{
		public class Tile : SpriteHandler
		{

				private Texture2D foundationTex;

				private enum type
				{
						Foundation,
						Wall,
						Roof,
						Window,
						WallItem,
						FloorItem,
						Door
				};
				public int Type
				{
						get; set;
				}

				public Tile( Texture2D texture ) : base( texture )
				{
						this.Texture=texture;
				}

				public override void Update()
				{

				}

				public void PlaceTile()
				{
						//// Ship Foundation content
						//foundationTex=Globals.Content.Load<Texture2D>( "foundation" );

						//// Ship Foundation sprite
						//foundationTile=new Tile( foundationTex );


						//get the direction astro is facing
						//place tile left or right of astro
						//get astro size
						//get tile size
				}
		}
}
