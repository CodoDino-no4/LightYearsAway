using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceRaft
{
		class BGManager
		{
				private readonly List<BGLayer> BGLayers;
				private Camera camera;

				public BGManager(Camera camera)
				{
						BGLayers=new List<BGLayer>();
						this.camera=camera;
				}

				public void AddLayer(BGLayer layer)
				{
						BGLayers.Add(layer);
				}

				public void Update()
				{
						foreach (var layer in BGLayers)
						{
								layer.Update();
						}
				}

				public void DrawBackground()
				{
						foreach (var layer in BGLayers)
						{
								layer.DrawBGLayer(camera);
						}
				}
		}
}
