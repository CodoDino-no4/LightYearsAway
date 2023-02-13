
using LYA.Sprites;
using LYA.Sprites.Junk;
using System.Collections.Generic;

namespace LYA.Managers
{
		public class JunkManager
		{
				private readonly List<SpriteHandler> junkInScene;
				public JunkManager()
				{
						junkInScene=new List<SpriteHandler>();
				}

				public void AddElement( SpriteHandler element)
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
