using LYA.Helpers;
using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		/// <summary>
		/// Handles direction of movement based on input
		/// </summary>
		public class MoveCommands : CommandManager.ICommand
		{
				public Astro astro;
				private MoveUp moveUp;
				private MoveDown moveDown;
				private MoveRight moveRight;
				private MoveLeft moveLeft;

				public MoveCommands( Astro astro ) : base()
				{
						this.astro=astro;
						moveUp=new MoveUp( astro );
						moveDown=new MoveDown( astro );
						moveLeft=new MoveLeft( astro );
						moveRight=new MoveRight( astro );
				}

				public void Execute()
				{
						// Previous Position
						Vector2 tmpPosition = astro.Position;

						if (InputBindings.Up().Held())
						{
								moveUp.Execute();
						}

						if (InputBindings.Down().Held())
						{
								moveDown.Execute();
						}

						if (InputBindings.Left().Held())
						{
								moveLeft.Execute();
						}

						if (InputBindings.Right().Held())
						{
								moveRight.Execute();
						}

						// Send Packet
						if (Globals.IsMulti&&tmpPosition!=astro.Position)
						{
								Globals.Packet.ClientSendPacket( "Move", Globals.ClientId, (int) astro.Position.X, (int) astro.Position.Y, "" );
						}
				}
		}
}
