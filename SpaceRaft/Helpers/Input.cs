using Apos.Input;
using Microsoft.Xna.Framework.Input;

namespace SpaceRaft.Helpers
{
		public class Input
		{
				AnyCondition keys;

				public Input()
				{
						keys=new AnyCondition();
				}

				public ICondition Up()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition(Keys.Up),
								new KeyboardCondition(Keys.W)
						);

						return keys;
				}
				public ICondition Down()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition(Keys.Down),
								new KeyboardCondition(Keys.S)
						);

						return keys;
				}
				public ICondition Left()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition(Keys.Left),
								new KeyboardCondition(Keys.A)
						);

						return keys;
				}
				public ICondition Right()
				{
						keys=
						new AnyCondition(
								new KeyboardCondition(Keys.Right),
								new KeyboardCondition(Keys.D)
						);

						return keys;
				}

				public bool MouseZoom()
				{
						bool zoom = false;

						if (MouseCondition.Scrolled())
						{
								zoom=true;
						}

						return zoom;
				}

				public ICondition MiddleMouse()
				{
						keys=
						new AnyCondition(
								new MouseCondition(MouseButton.MiddleButton)
								);

						return keys;
				}

				public ICondition ZoomIn()
				{
						keys=new AnyCondition(new KeyboardCondition(Keys.OemPlus));

						return keys;
				}

				public ICondition ZoomOut()
				{
						keys=new AnyCondition(new KeyboardCondition(Keys.OemMinus));

						return keys;
				}

		}
}
