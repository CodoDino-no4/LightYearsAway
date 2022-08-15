﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace SpaceRaft.Sprites
{
		class BGLayer: SpriteHandler
		{
				private float moveScale;
				private float defaultSpeed;

				public BGLayer(Texture2D texture, float moveScale, float defaultSpeed = 0.0f) : base(texture)
				{
						this.texture=texture;
						this.moveScale=moveScale;
						this.defaultSpeed=defaultSpeed;
				}

				public override void Update()
				{

						//Position.X-=20f;
						//Position.X+=((movement*_moveScale)+_defaultSpeed)*Globals.ElapsedSeconds;
						//Position.X%=_texture.Width;

						//if (Position.X>=0)
						//{
						//		Position2.X=Position.X-_texture.Width;
						//} else
						//{
						//		Position2.X=Position.X+_texture.Width;
						//}
				}

				public virtual void DrawBGLayer(Camera camera)
				{
						if (texture!=null)
								Globals.SpriteBatch.Draw(texture, Globals.ScreenSize, Color.White);
				}
		}
}
