using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRaft.Sprites
{
		public class UIElement : SpriteHandler
		{
				public UIElement(Texture2D texture, Vector2 position) : base(texture)
				{
						this.texture = texture;
						Position=position;
				}
		}
}
