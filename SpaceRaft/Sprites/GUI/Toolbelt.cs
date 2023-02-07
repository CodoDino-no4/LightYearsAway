using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LYA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Sprites.GUI
{
		public class Toolbelt : SpriteHandler
		{
				public List<Tool> tools= new List<Tool>();

				public Toolbelt ( Texture2D texture ) : base ( texture )
				{
						this.Texture=texture;
				}

				public override void Update ( )
				{
						Position=new Vector2 ( Globals.ScreenSize.Width/2, Globals.ScreenSize.Height-Texture.Height );
						Rectangle.Inflate ( Rectangle.Width*2, Rectangle.Height*2 );

				}

				public void AddToBelt (Tool tool )
				{
						tools.Add ( tool );
				}
		}
}
