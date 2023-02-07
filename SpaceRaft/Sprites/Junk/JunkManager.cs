using LYA.Helpers;
using LYA.Sprites.Background;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Sprites.Junk
{
		public class JunkManager
		{
				private readonly List<Junk> junkInScene;
				public JunkManager ( )
				{
						junkInScene=new List<Junk> ( );
				}

				public void AddJunk ( Junk junk )
				{
						junkInScene.Add ( junk );
				}

				public void Update ( )
				{
						foreach (var junk in junkInScene)
								junk.Update ( );
				}

				public void DrawJunk ( )
				{
						foreach (var junk in junkInScene)
								junk.Draw ( );
				}
		}
}
