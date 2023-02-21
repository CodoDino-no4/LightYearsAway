using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LYA.Sprites.GUI
{
		public class Toolbelt : BaseSprite
		{
				public List<Tool> tools= new List<Tool>();

				public Toolbelt( Texture2D texture ) : base( texture )
				{
						this.Texture=texture;
						Scale=4f;
				}

				public override void Update()
				{
						Position=new Vector2( Globals.ScreenSize.Width/2, Globals.ScreenSize.Height-Texture.Height*2 );
				}

				public void AddToBelt( Tool tool )
				{
						tools.Add( tool );
				}
		}
}
