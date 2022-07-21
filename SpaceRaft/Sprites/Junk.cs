using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SpaceRaft.Sprites;
using SpaceRaft.Models;

namespace SpaceRaft.Sprites
{
		class Junk : Sprite
		{
				public Junk( Texture2D texture ) : base( texture )
				{
						//Rotation
				}

				public override void Update( GameTime gameTime)
				{
						rotation += rotation * RotationVelocity;
				}
		}
}
