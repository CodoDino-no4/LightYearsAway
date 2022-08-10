using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceRaft.Sprites
{
		class BGLayer: SpriteHandler
		{
				private float _moveScale;
				private float _defaultSpeed;

				public BGLayer(Texture2D texture, float moveScale, float defaultSpeed = 0.0f) : base(texture)
				{
						_texture=texture;
						_moveScale=moveScale;
						_defaultSpeed=defaultSpeed;
				}

				public override void Update(GameTime gameTime)
				{
						//Position.X-=20f;
						//Position.X+=((movement*_moveScale)+_defaultSpeed)*Globals.ElapsedSeconds;
						//Position.X%=_texture.Width;

						//if (Position.X>=0)
						//{
						//		Position2.X=Position.X-_texture.Width;
						//} else
						//{
						//		Position2.X=Position.X+_texture.Width;
						//}
				}
		}
}
