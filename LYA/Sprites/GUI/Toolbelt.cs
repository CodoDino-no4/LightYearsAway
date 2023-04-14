using LYA.Helpers;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites.GUI
{
		public class Toolbelt : BaseSprite
		{
				public List<Tool> tools;
				public Tile currentSelection;

				public Toolbelt( Texture2D texture ) : base( texture )
				{
						tools=new List<Tool>();
						this.Texture=texture;
						Scale=4f;
						Position=new Vector2( Globals.ScreenSize.Width/2, Globals.ScreenSize.Height-Texture.Height*2 );
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
