using LYA.Helpers;
using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Managers
{
		public class BGManager
		{
				private readonly List<BaseSprite> bgLayers;

				public BGManager()
				{
						bgLayers=new List<BaseSprite>();
				}

				public void AddElement( BaseSprite element )
				{
						bgLayers.Add( element );
				}
				public void Update()
				{
						foreach (var layer in bgLayers)
								layer.Update();
				}

				public void Draw()
				{
						foreach (var layer in bgLayers)
								Globals.SpriteBatch.Draw( layer.Texture, Globals.ScreenSize, Color.White );
				}

		}
}
