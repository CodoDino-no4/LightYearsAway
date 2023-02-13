using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites
{
		public class Astro : SpriteHandler
		{
				private enum state
				{
						Idle,
						Walk,
						Float
				};

				public int State
				{
						get; set;
				}


				public Astro( Texture2D texture ) : base( texture )
				{
						State=(int) state.Float;
				}

				public override void Update()
				{
						//Globals.astroPosX=position.X;
						//Globals.astroPosY=position.Y;

				}

				public void UpdateState()
				{

				}

		}
}
