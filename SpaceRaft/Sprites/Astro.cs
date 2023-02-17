using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

				public int state;

				public Vector2 Direction;

				public Astro( Texture2D texture ) : base( texture )
				{
						state=(int) State.swim;
						Direction=Vector2.Zero;
				}

				public override void Update()
				{


				}

				public void UpdateState()
				{

				}

		}
}
