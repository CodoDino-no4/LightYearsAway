using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceRaft.Sprites
{
		public class Astro: SpriteHandler
		{
				public int health
				{
						get; set;
				}
				public bool isDead
				{
						get
						{
								return health<=0;
						}
				}

				public Astro(Texture2D texture) : base(texture)
				{
						state="Idle";
				}

				public override void Update(GameTime gameTime)
				{

				}
		}
}
