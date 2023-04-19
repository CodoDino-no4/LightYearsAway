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

				public override void Update()
				{


				}

				public void UpdateState()
				{

				}

		}
}
