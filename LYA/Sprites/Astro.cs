using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace LYA.Sprites
{
		public class Astro : BaseSprite
		{
				public enum State
				{
						idle,
						walk,
						swim,
				};

				public State state;

				public Vector2 Direction;

				public int clientId;

				public Astro( Texture2D texture, int clientId ) : base( texture )
				{
						state=State.swim;
						Direction=Vector2.Zero;
						this.clientId=clientId;
				}

				public void Draw( Deque<Astro> sprites )
				{
						foreach (BaseSprite sprite in sprites)
						{
								if (Texture!=null)
										Globals.SpriteBatch.Draw( Texture, Position, Rectangle, Color.White, rotation, Origin, Scale, SpriteEffects.None, 0 );
						}
				}

				public override void Update()
				{


				}

				public void UpdateState()
				{

				}

		}
}
