using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Helpers;
using System.Diagnostics;

namespace SpaceRaft.Sprites.Background
{
		class BGLayer: SpriteHandler
		{
				private float speed;

				public BGLayer(Texture2D texture) : base(texture)
				{
						this.Texture=texture;
				}

				public override void Update()
				{
						speed=1f;
				}
		}
}
