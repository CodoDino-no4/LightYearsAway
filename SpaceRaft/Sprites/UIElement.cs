using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRaft.Sprites
{
		public class UIElement : SpriteHandler
		{
				public UIElement(Texture2D texture) : base(texture)
				{
						this.texture = texture;
				}

				public void Update(Vector2 position)
				{
						Position.X=position.X;
						Position.Y=position.Y+((Globals.ScreenSize.Height/2)-texture.Height);
				}

		}
}
