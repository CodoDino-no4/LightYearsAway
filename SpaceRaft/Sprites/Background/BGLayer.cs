using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Helpers;
using System.Diagnostics;

namespace SpaceRaft.Sprites.Background
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

				}
		}
}
