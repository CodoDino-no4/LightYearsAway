using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

// need this so i can scale the whole ui and change it for screen resoltions etc
namespace SpaceRaft.Sprites.GUI
{
		public class UIManager
		{
				private readonly List<SpriteHandler> uiElements;
				public UIManager()
				{
						uiElements=new List<SpriteHandler>();
				}

				public void AddElement(SpriteHandler element)
				{
						uiElements.Add(element);
				}

				public void DrawElements()
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
