using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace LYA.Screens
{
		public class Splash : GameScreen
		{
				public Splash( Game game ) : base( game )
				{
				}

				private new LYA Game => (LYA) base.Game;

				public override void LoadContent()
				{
						base.LoadContent();
				}
				public override void Draw( GameTime gameTime )
				{

				}

				public override void Update( GameTime gameTime )
				{

				}
		}
}
