using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites.Cloneables
{
		/// <summary>
		/// Cloneable sprites
		/// </summary>
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
