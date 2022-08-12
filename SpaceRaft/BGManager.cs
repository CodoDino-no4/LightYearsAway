using Microsoft.Xna.Framework;
using SpaceRaft.Sprites;
using System.Collections.Generic;

namespace SpaceRaft
{
		class BGManager
		{
				private readonly List<BGLayer> _BGLayers;

				public BGManager()
				{
						_BGLayers=new List<BGLayer>();
				}

				public void AddLayer(BGLayer layer)
				{
						_BGLayers.Add(layer);
				}

				public void Update(GameTime gameTime)
				{
						foreach (var layer in _BGLayers)
						{
								layer.Update(gameTime);
						}
				}

				public void DrawInCentre()
				{
						foreach (var layer in _BGLayers)
						{
								layer.DrawInCentre();
						}
				}
		}
}
