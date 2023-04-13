using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites.Background
{
		public class BGLayer : BaseSprite
		{
				private float parallaxSpeed;

				public BGLayer( Texture2D texture ) : base( texture )
				{
						this.Texture=texture;
				}

				public override void Update()
				{
						parallaxSpeed=1f;
				}
		}
}
