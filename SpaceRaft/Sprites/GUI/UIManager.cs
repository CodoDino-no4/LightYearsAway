﻿using Microsoft.Xna.Framework;
using LYA.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using LYA;

// need this so i can scale the whole ui and change it for screen resoltions etc
namespace LYA.Sprites.GUI
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
						{
								element.Update();
						}
				}
		}
}
