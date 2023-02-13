using LYA.Helpers;
using LYA.Sprites;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LYA.Managers
{
		public class BGManager
		{
				private readonly List<SpriteHandler> bgLayers;

				public BGManager()
				{
						bgLayers=new List<SpriteHandler>();
				}

				public void AddElement( SpriteHandler element )
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
