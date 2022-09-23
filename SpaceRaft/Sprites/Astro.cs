using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Helpers;

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

				private Input input;

				public Astro(Texture2D texture) : base(texture)
				{
						state="Idle";
						input=new Input();
						Position= new Vector2(Globals.ScreenSize.Width/2, Globals.ScreenSize.Height/2);
				}

				public override void Update()
				{
						if (input.Up().Held())
								Position.Y-=3f;

						if (input.Down().Held())
								Position.Y+=3f;

						if (input.Left().Held())
								Position.X-=3f;

						if (input.Right().Held())
								Position.X+=3f;
				}

				public void DrawAstro()
				{
						if (texture!=null)
								Globals.SpriteBatch.Draw(texture, Position, Rectangle, Color.White, rotation, Origin, 2, SpriteEffects.None, 0);
				}
		}
}
