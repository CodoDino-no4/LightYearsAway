﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LYA.Helpers;
using System.Collections.Generic;
using System.Diagnostics;

namespace LYA.Sprites.Background
{
		public class BGManager
		{
				private readonly List<BGLayer> bgLayers;

				public BGManager()
				{
						bgLayers=new List<BGLayer>();
				}

				public void AddLayer(BGLayer layer)
				{
						bgLayers.Add(layer);
				}
				public void Update()
				{
						foreach (var layer in bgLayers)
								layer.Update();
				}

				public void DrawBackground()
				{
						foreach (var layer in bgLayers)
								Globals.SpriteBatch.Draw(layer.Texture, Globals.ScreenSize, Color.White);
				}

		}
}
