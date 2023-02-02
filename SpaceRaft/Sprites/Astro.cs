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
						Walking,
						Floating
				};

				private string State
				{
						get; set;
				}
				public int Health
				{
						get; set;
				}

				public bool IsDead
				{
						get
						{
								return Health<=0;
						}
				}

				private Input input;

				public Astro(Texture2D texture) : base(texture)
				{
						State="Idle";
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
