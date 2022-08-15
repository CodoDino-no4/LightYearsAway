using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceRaft.Helpers;

namespace SpaceRaft.Sprites
{
		public class SpriteHandler
		{
				public Texture2D texture;

				// Centre of sprite
				public Vector2 Origin;

				public Vector2 Position;
				public Vector2 Velocity;
				public float rotation;
				protected string state;
				public float layer;

				public float RotationVelocity;
				public float LinearVelocity = 2f;

				public bool IsRemoved = false;

				public SpriteHandler(Texture2D texture)
				{
						this.texture=texture;
						Origin=new Vector2(texture.Width/2, texture.Height/2);
				}

				public Rectangle Rectangle
				{
						get
						{
								return new Rectangle((int) -texture.Width, (int) -texture.Height, texture.Width, texture.Height);
						}
				}

				public float Rotation
				{
						get; set;
				}
				public virtual void Update()
				{

				}

				public virtual void Draw()
				{
						if (this.texture !=null)
								Globals.SpriteBatch.Draw(texture, Position, null, Color.White, Rotation, Origin, 2, SpriteEffects.None, 0);

				}
		}
}
