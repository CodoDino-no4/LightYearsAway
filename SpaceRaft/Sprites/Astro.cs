using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Helpers;
using System.Linq;

namespace SpaceRaft.Sprites
{
		public class Astro: SpriteHandler
		{
				private enum state
				{
						Idle,
						Walk,
						Float
				};

				private int State
				{
						get; set;
				}

				private Input input;

				public Astro(Texture2D texture) : base(texture)
				{
						State=(int) state.Float;
						input=new Input();
				}

				public override void Update()
				{

						Movement ( );

				}

				public void Movement ( )
				{
						if (input.Up ( ).Held ( ))
								Position.Y-=3f;

						if (input.Down ( ).Held ( ))
								Position.Y+=3f;

						if (input.Left ( ).Held ( ))
								Position.X-=3f;

						if (input.Right ( ).Held ( ))
								Position.X+=3f;
				}

				public void UpdateState ( )
				{

				}
		}
}
