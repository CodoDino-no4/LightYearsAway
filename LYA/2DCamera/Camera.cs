using Apos.Input;
using LYA.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LYA._Camera
{
		public class Camera
		{
				public Matrix Transform
				{
						get; protected set;
				}

				public float scale = 1f;
				private float uiScale = 1f;
				private float scrollSpeed = 0.1f;
				private float snapDistance = 0.002f;

				private float minExp = 0f;
				private float maxExp = -2f;
				private float targetExp = 0f;

				private Vector2 position;

				public Camera()
				{
						//start at 0
						position=Vector2.Zero;
				}

				public void UpdateCameraInput( Vector2 pos )
				{
						position.X=pos.X-Globals.ScreenSize.Width/2;
						position.Y=pos.Y-Globals.ScreenSize.Height/2;

						// Zoom with scroll wheel
						if (InputBindings.ZoomAny())
						{
								int scrollDelta = MouseCondition.ScrollDelta;
								targetExp=MathHelper.Clamp( targetExp-scrollDelta*snapDistance, maxExp, minExp );
						}

						// Zoom with buttons
						if (InputBindings.ZoomIn().Pressed())
								targetExp=MathHelper.Clamp( targetExp-120*snapDistance, maxExp, minExp );

						if (InputBindings.ZoomOut().Pressed())
								targetExp=MathHelper.Clamp( targetExp+120*snapDistance, maxExp, minExp );

						scale=ExpToScale( Interpolate( ScaleToExp( scale ), targetExp, scrollSpeed, snapDistance ) );

				}

				// Tweening function, interpolation over multiple frames.
				public float Interpolate( float start, float target, float speed, float snapNear )
				{
						float result = MathHelper.Lerp(start, target, speed);

						if (start<target)
						{
								result=MathHelper.Clamp( result, start, target );
						}
						else
						{
								result=MathHelper.Clamp( result, target, start );
						}

						if (MathF.Abs( target-result )<snapNear)
						{
								return target;
						}
						else
						{
								return result;
						}
				}

				// Gets the current visible area Transform
				public Matrix GetView()
				{
						Transform=
								Matrix.CreateTranslation( -Globals.ScreenSize.Width/2, -Globals.ScreenSize.Height/2, 0f )*
								Matrix.CreateTranslation( -position.X, -position.Y, 0f )*
								Matrix.CreateScale( scale, scale, 1f )*
								Matrix.CreateTranslation( Globals.ScreenSize.Width/2, Globals.ScreenSize.Height/2, 0f );

						return Transform;
				}

				// Gets the background Transform
				public Matrix GetBgTransform( Texture2D t )
				{
						return
								Matrix.CreateScale( t.Width, t.Height, 1f )*
								Matrix.CreateScale( 2, 2, 1f )*
								GetView()*
								Matrix.CreateScale( 1f/Globals.ScreenSize.Width, 1f/Globals.ScreenSize.Height, 1f );
				}

				// Gets the UI Scale
				public Matrix GetUIScale()
				{
						return Matrix.CreateScale( uiScale, uiScale, 1f );
				}

				public float ScaleToExp( float scale )
				{
						return -MathF.Log( scale );
				}

				public float ExpToScale( float exp )
				{
						return MathF.Exp( -exp );
				}
		}
}
