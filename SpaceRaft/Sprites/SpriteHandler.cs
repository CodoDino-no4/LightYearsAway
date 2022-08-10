using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceRaft.Models;

namespace SpaceRaft.Sprites
{
		public class SpriteHandler
		{
				public Texture2D _texture;

				// Centre of sprite
				public Vector2 Origin;

				public Vector2 Position;
				public Vector2 Velocity;
				protected float rotation;
				protected string state;
				public float layer;

				public float RotationVelocity = 10f;
				public float LinearVelocity = 2f;

				public Input Input;
				public bool IsRemoved = false;

				protected KeyboardState _currentKey;
				protected KeyboardState _previousKey;


				public SpriteHandler(Texture2D texture)
				{
						_texture = texture;
						Origin = new Vector2(texture.Width/2, texture.Height/2);
				}

				public Rectangle Rectangle
				{
						get
						{
								return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
						}
				}

				public float Rotation
				{
						get
						{
								return rotation;
						}
						set
						{
								rotation=value;
						}
				}

				public virtual void Update(GameTime gameTime)
				{

				}

				// Static sprites
				public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
				{
						if (_texture!=null)
						{
								spriteBatch.Draw(_texture, position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);

						}
				}

				// Variable position sprites
				public virtual void Draw(SpriteBatch spriteBatch)
				{
						if (_texture!=null)
						{
								spriteBatch.Draw(_texture, Position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);

						}
				}
		}
}
