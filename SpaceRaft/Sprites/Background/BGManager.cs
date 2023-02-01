using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceRaft.Sprites.Background
{
		class BGManager
		{
				private readonly List<BGLayer> BGLayers;

				public BGManager()
				{
						BGLayers=new List<BGLayer>();
				}

				public void AddLayer(BGLayer layer)
				{
						BGLayers.Add(layer);
				}
				public void Update()
				{
						foreach (var layer in BGLayers)
								layer.Update();
				}

				public void DrawBackground()
				{
						foreach (var layer in BGLayers)
								layer.DrawLayer();
				}

		}
}
