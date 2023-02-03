using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRaft.Sprites.GUI
{
		public class UIElement: SpriteHandler
		{
				public UIElement(Texture2D texture) : base(texture)
				{
						this.texture=texture;
				}

				public void Update(Vector2 position)
				{

				}

		}
}
