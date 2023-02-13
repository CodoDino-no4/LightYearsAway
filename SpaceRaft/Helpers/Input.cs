using Apos.Input;
using Microsoft.Xna.Framework.Input;

namespace LYA.Helpers
{
		public static class Input
		{
				private static AnyCondition keys = new AnyCondition();

				public static ICondition Up()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition( Keys.Up ),
								new KeyboardCondition( Keys.W )
						);

						return keys;
				}
				public static ICondition Down()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition( Keys.Down ),
								new KeyboardCondition( Keys.S )
						);

						return keys;
				}
				public static ICondition Left()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition( Keys.Left ),
								new KeyboardCondition( Keys.A )
						);

						return keys;
				}
				public static ICondition Right()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition( Keys.Right ),
								new KeyboardCondition( Keys.D )
						);

						return keys;
				}

				public static ICondition Quit()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition( Keys.Escape )
						);

						return keys;
				}

				public static bool MouseZoom()
				{
						bool zoom = false;

						if (MouseCondition.Scrolled())
						{
								zoom=true;
						}

						return zoom;
				}

				public static ICondition MiddleMouse()
				{
						keys=
						new AnyCondition(
								new MouseCondition( MouseButton.MiddleButton )
								);

						return keys;
				}

				public static ICondition ZoomIn()
				{
						keys=new AnyCondition( new KeyboardCondition( Keys.OemPlus ) );

						return keys;
				}

				public static ICondition ZoomOut()
				{
						keys=new AnyCondition( new KeyboardCondition( Keys.OemMinus ) );

						return keys;
				}

				public static ICondition Place()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition( Keys.E )
						);

						return keys;
				}

		}
}
