using Microsoft.Xna.Framework.Graphics;
using System;

namespace LYA.Sprites.Cloneables
{
		public class CloneableSprite : BaseSprite, ICloneable
		{
				public CloneableSprite Parent;
				public CloneableSprite( Texture2D texture ) : base( texture )
				{
				}

				public object Clone()
				{
						return MemberwiseClone();
				}
		}
}
