using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceRaft;
using SpaceRaft.Helpers;
using System;
using System.Diagnostics;
using MonoGame.Extended.Tweening;

public class Camera
{
		public float Zoom
		{
				get; set;
		}

		public Rectangle Bounds
		{
				get; protected set;
		}
		public Viewport Viewport
		{
				get; protected set;
		}
		public Matrix Transform
		{
				get; protected set;
		}
		public Rectangle VisibleArea
		{
				get; set;
		}

		public Input input;

		float scale = 1f;
		float scrollSpeed = 0.1f;
		float snapDistance = 0.002f;

		float minExp = 0f;
		float maxExp = -2f;
		float targetExp = 0f;

		Vector2 _mouseWorld = Vector2.Zero;
		Vector2 _dragAnchor = Vector2.Zero;
		bool _isDragged = false;

		float _expDistance = 0.002f;
		public Vector2 Position = new Vector2(0f, 0f);

		ICondition CameraDrag = new MouseCondition(MouseButton.MiddleButton);

		public Camera(Viewport viewport)
		{
				Viewport=viewport;
				Bounds=viewport.Bounds;
				Zoom=2f;
				input = new Input();
		}

		public void UpdateCameraInput()
		{
				Bounds=Viewport.Bounds;

				if (MouseCondition.Scrolled())
				{
						int scrollDelta = MouseCondition.ScrollDelta;
						targetExp=MathHelper.Clamp(targetExp-scrollDelta*_expDistance, maxExp, minExp);
				}

				_mouseWorld=Vector2.Transform(InputHelper.NewMouse.Position.ToVector2(), Matrix.Invert(GetView()));
				scale=ExpToScale(Interpolate(ScaleToExp(scale), targetExp, scrollSpeed, snapDistance));

				if (CameraDrag.Pressed())
				{
						_dragAnchor=_mouseWorld;
						_isDragged=true;
				}
				if (_isDragged&&CameraDrag.HeldOnly())
				{
						Position+=_dragAnchor-_mouseWorld;
						_mouseWorld=_dragAnchor;
				}
				if (_isDragged&&CameraDrag.Released())
				{
						_isDragged=false;
				}

				if (input.Up().Held())
						Position.Y-=3f;
				if (input.Down().Held())
						Position.Y+=3f;

				if (input.Left().Held())
						Position.X-=3f;
				if (input.Right().Held())
						Position.X+=3f;


		}

		/// If the result is stored in the value, it will create a nice interpolation over multiple frames.
		/// </summary>
		/// <param name="start"> The value to start from.</param>
		/// <param name="target"> The value to reach.</param>
		/// <param name="speed"> A value between 0f and 1f.</param>
		/// <param name="snapNear"> When the difference between the target and the result is smaller than this value, the target will be returned.
		public float Interpolate(float start, float target, float speed, float snapNear)
		{
				float result = MathHelper.Lerp(start, target, speed);

				if (start<target)
				{
						result=MathHelper.Clamp(result, start, target);
				} else
				{
						result=MathHelper.Clamp(result, target, start);
				}

				if (MathF.Abs(target-result)<snapNear)
				{
						return target;
				} else
				{
						return result;
				}
		}

		//public void GetVisibleRectangle()
		//{
		//		var inverseViewMatrix = Matrix.Invert(Transform);

		//		var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
		//		var tr = Vector2.Transform(new Vector2(Globals.ScreenSize.Width, 0), inverseViewMatrix);
		//		var bl = Vector2.Transform(new Vector2(0, Globals.ScreenSize.Height), inverseViewMatrix);
		//		var br = Vector2.Transform(new Vector2(Globals.ScreenSize.Width, Globals.ScreenSize.Height), inverseViewMatrix);
		//		var min = new Vector2(
		//				MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
		//				MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
		//		var max = new Vector2(
		//				MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
		//				MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));

		//		VisibleArea=new Rectangle((int) min.X, (int) min.Y, (int) (max.X-min.X), (int) (max.Y-min.Y));
		//}

		public Matrix GetView()
		{
				Vector2 origin = new Vector2(Globals.ScreenSize.Width/2f, Globals.ScreenSize.Height/2f);
				Transform=
						Matrix.CreateTranslation(-origin.X, -origin.Y, 0f)*
						Matrix.CreateTranslation(-Position.X, -Position.Y, 0f)*
						Matrix.CreateScale(scale, scale, 1f)*
						Matrix.CreateTranslation(origin.X, origin.Y, 0f);
				return Transform;

				//Origin is used so all translations are done from the center instead of top left
		}
		public Matrix GetUVTransform(Texture2D t, Vector2 offset, float scale)
		{
				return
						Matrix.CreateScale(t.Width, t.Height, 1f)*
						Matrix.CreateScale(scale, scale, 1f)*
						Matrix.CreateTranslation(offset.X, offset.Y, 0f)*
						GetView()*
						Matrix.CreateScale(1f/Viewport.Width, 1f/Viewport.Height, 1f);
		}

		public float ScaleToExp(float scale)
		{
				return -MathF.Log(scale);
		}
		public float ExpToScale(float exp)
		{
				return MathF.Exp(-exp);
		}
}
