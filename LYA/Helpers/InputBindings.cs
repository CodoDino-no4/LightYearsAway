using Apos.Input;
using Microsoft.Xna.Framework.Input;

namespace LYA.Helpers
{
		public static class InputBindings
		{
				private static AnyCondition keyList = new AnyCondition();

				//private static KeyboardCondition bind = new KeyboardCondition(Keys.Up);

				public static bool Input()
				{
						if (keyList!=null)
						{
								return true;
						}

						return false;
				}

				public static ICondition Up()
				{
						keyList=
						new AnyCondition(
								new KeyboardCondition( Keys.Up ),
								new KeyboardCondition( Keys.W )
						);

						return keyList;
				}
				public static ICondition Down()
				{
						keyList=
						new AnyCondition(
								new KeyboardCondition( Keys.Down ),
								new KeyboardCondition( Keys.S )
						);

						return keyList;
				}
				public static ICondition Left()
				{
						keyList=
						new AnyCondition(
								new KeyboardCondition( Keys.Left ),
								new KeyboardCondition( Keys.A )
						);

						return keyList;
				}
				public static ICondition Right()
				{
						keyList=
						new AnyCondition(
								new KeyboardCondition( Keys.Right ),
								new KeyboardCondition( Keys.D )
						);

						return keyList;
				}

				public static ICondition Quit()
				{
						keyList=
						new AnyCondition(
								new KeyboardCondition( Keys.Escape )
						);

						return keyList;
				}

				public static bool ZoomAny()
				{
						bool zoom = false;

						if (MouseCondition.Scrolled())
						{
								zoom=true;
						}

						return zoom;
				}

				public static ICondition unusedMiddleMouse()
				{
						keyList=
						new AnyCondition(
								new MouseCondition( MouseButton.MiddleButton )
								);

						return keyList;
				}

				public static ICondition ZoomIn()
				{
						keyList=new AnyCondition( new KeyboardCondition( Keys.OemPlus ) );

						return keyList;
				}

				public static ICondition ZoomOut()
				{
						keyList=new AnyCondition( new KeyboardCondition( Keys.OemMinus ) );

						return keyList;
				}

				public static ICondition Place()
				{
						keyList=
						new AnyCondition(
								new KeyboardCondition( Keys.E )
						);

						return keyList;
				}

				public static ICondition Menu()
				{
						keyList=
						new AnyCondition(
								new KeyboardCondition( Keys.Escape )
						);

						return keyList;
				}

		}
}
