
using LYA.Sprites;
using System.Collections.Generic;

namespace LYA.Managers
{
		public class JunkManager
		{
				private readonly List<BaseSprite> junkInScene;
				public JunkManager()
				{
						junkInScene=new List<BaseSprite>();
				}

				public void AddElement( BaseSprite element )
				{
						junkInScene.Add( element );
				}

				public void Update()
				{
						foreach (var junk in junkInScene)
								junk.Update();
				}

				public void Draw()
				{
						foreach (var junk in junkInScene)
								junk.Draw();
				}
		}
}
