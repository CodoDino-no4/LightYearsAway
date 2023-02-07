using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LYA.Helpers;
using System.Diagnostics;

namespace LYA.Sprites.Background
{
		public class BGLayer: SpriteHandler
		{
				private float parallaxSpeed;

				public BGLayer(Texture2D texture) : base(texture)
				{
						this.Texture=texture;
				}

				public override void Update()
				{
						parallaxSpeed=1f;
				}
		}
}
