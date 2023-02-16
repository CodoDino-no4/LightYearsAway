using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites
{
		public class Astro : SpriteHandler
		{
				public enum State
				{
						idle,
						walk,
						swim,
				};

				public int state;

				public enum Direction
				{
						front,
						left,
						right,
						up,
						down,
						back
				}

				public int direction;

				public Astro( Texture2D texture ) : base( texture )
				{
						state=(int) State.swim;
						direction=(int) Direction.front;
				}

				public override void Update()
				{


				}

				public void UpdateState()
				{

				}

		}
}
