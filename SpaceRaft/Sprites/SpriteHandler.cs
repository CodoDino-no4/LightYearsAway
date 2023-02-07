using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceRaft.Helpers;

namespace SpaceRaft.Sprites
{
		public class SpriteHandler : SpriteComponent
		{
				public Texture2D Texture;

				private Vector2 origin;
				public Vector2 Position;
				public Vector2 Velocity;
				protected float Rotation;

				public float RotationVelocity;
				public float LinearVelocity = 2f;

				public bool IsRemoved = false;

				public SpriteHandler(Texture2D texture)
				{
						this.Texture=texture;
						origin=new Vector2(texture.Width/2, texture.Height/2);
				}

				public Rectangle Rectangle
				{
						get
						{
								return new Rectangle((int) -Texture.Width, (int) -Texture.Height, Texture.Width, Texture.Height);
						}

						set
						{
						}
				}

				public float Rotate
				{
						get; set;
				}

				public override void Draw()
				{
						if (Texture !=null)
								Globals.SpriteBatch.Draw(Texture, Position, Rectangle, Color.White, Rotate, origin, 2, SpriteEffects.None, 0);

				}
				public override void Update ( )
				{

				}
		}
}
