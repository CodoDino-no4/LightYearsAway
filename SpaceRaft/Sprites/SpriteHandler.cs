using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceRaft.Helpers;

namespace SpaceRaft.Sprites
{
		public class SpriteHandler : SpriteComponent
		{
				public Texture2D texture;

				// Centre of sprite
				private Vector2 Origin;
				public Vector2 Position;
				public Vector2 Velocity;
				protected float Rotation;

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

				public float Rotate
				{
						get; set;
				}
				public override void Update()
				{

				}

				public override void Draw()
				{
						if (texture !=null)
								Globals.SpriteBatch.Draw(texture, Position, null, Color.White, Rotate, Origin, 2, SpriteEffects.None, 0);

				}
		}
}
