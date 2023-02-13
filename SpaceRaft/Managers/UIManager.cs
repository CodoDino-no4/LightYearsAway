using LYA.Sprites;
using System.Collections.Generic;

namespace LYA.Managers
{
		public class UIManager
		{
				private readonly List<SpriteHandler> uiElements;
				public UIManager()
				{
						uiElements=new List<SpriteHandler>();
				}

				public void AddElement( SpriteHandler element )
				{
						uiElements.Add( element );
				}

				public void Draw()
				{
						foreach (var element in uiElements)
								element.Draw();

				}
				public void Update()
				{
						foreach (var element in uiElements)
								element.Update();
				}
		}
}
