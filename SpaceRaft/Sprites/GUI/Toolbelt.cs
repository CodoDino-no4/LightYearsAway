using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRaft.Sprites.GUI
{
		public class Toolbelt : SpriteHandler
		{
				public Toolbelt ( Texture2D texture ) : base ( texture )
				{
						this.Texture=texture;
				}

				public override void Update ( )
				{
						Position=new Vector2 ( Globals.ScreenSize.Width/2, Globals.ScreenSize.Height-Texture.Height );

				}
		}
}
