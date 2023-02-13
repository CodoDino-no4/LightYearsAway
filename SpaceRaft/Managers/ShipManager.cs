
using LYA.Sprites;
using System.Collections.Generic;

namespace LYA.Managers
{
		public class ShipManager
		{
				private readonly List<SpriteHandler> shipFoundations;
				public ShipManager()
				{
						shipFoundations=new List<SpriteHandler>();
				}

				public void AddElement( SpriteHandler element )
				{
						shipFoundations.Add( element );
				}

				public void Draw()
				{
						foreach (var element in shipFoundations)
								element.Draw();

				}
				public void Update()
				{
						foreach (var element in shipFoundations)
								element.Update();
				}
		}
}
