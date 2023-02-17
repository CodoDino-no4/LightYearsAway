using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Sprites.Cloneables
{
		public class CloneableSprite : BaseSprite, ICloneable
		{
				public CloneableSprite( Texture2D texture ) : base( texture )
				{
				}

				public object Clone()
				{
						return this.MemberwiseClone();
				}
		}
}
