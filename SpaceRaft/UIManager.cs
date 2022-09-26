using SpaceRaft.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRaft
{
		public class UIManager
		{
				private readonly List<SpriteHandler> UIElements;
				public UIManager()
				{
						UIElements = new List<SpriteHandler>();
				}

				public void AddElement(SpriteHandler element)
				{
						UIElements.Add(element);
				}

				public void Draw()
				{


				}

				public void Update()
				{

				}
		}
}
