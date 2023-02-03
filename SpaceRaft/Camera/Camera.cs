using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceRaft.Helpers;
using System;
using System.Diagnostics;
using MonoGame.Extended.Tweening;

public class Camera
{
		public Matrix Transform
		{
				get; protected set;
		}

		public Matrix UITransform
		{
				get; protected set;
		}

		private Input input;

		// Scale
		public float scale = 1f;
		private float uiScale = 1f;
		private float scrollSpeed = 0.1f;
		private float snapDistance = 0.002f;

		private float minExp = 0f;
		private float maxExp = -2f;
		private float targetExp = 0f;

		// Top left corner coordinates
		private Vector2 position;

		public Camera()
		{
				position=Vector2.Zero;
				input = new Input();
		}

		public void UpdateCameraInput(Vector2 position)
		{
				this.position.X=position.X-Globals.ScreenSize.Width/2;
				this.position.Y=position.Y-Globals.ScreenSize.Height/2;

				// Scale FOV with scroll wheel
				if (input.MouseZoom())
				{
						int scrollDelta = MouseCondition.ScrollDelta;
						targetExp=MathHelper.Clamp(targetExp-scrollDelta*snapDistance, maxExp, minExp);
				}

				// Scale FOV with buttons
				if (input.ZoomIn().Pressed())
						targetExp=MathHelper.Clamp(targetExp-120*snapDistance, maxExp, minExp);
				
				if (input.ZoomOut().Pressed())
						targetExp=MathHelper.Clamp(targetExp+120*snapDistance, maxExp, minExp);

				scale=ExpToScale(Interpolate(ScaleToExp(scale), targetExp, scrollSpeed, snapDistance));

		}

		// Tweening function, interpolation over multiple frames.
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

		// Gets the current visible area Transform
		public Matrix GetView()
		{
				Transform=
						Matrix.CreateTranslation(-Globals.ScreenSize.Width/2, -Globals.ScreenSize.Height/2, 0f)*
						Matrix.CreateTranslation(-position.X, -position.Y, 0f)*
						Matrix.CreateScale(scale, scale, 1f)*	
						Matrix.CreateTranslation(Globals.ScreenSize.Width/2, Globals.ScreenSize.Height/2, 0f);
				
				return Transform;
		}

		// Gets the visible camera area Transform
		public Matrix GetUIScale()
		{
				return Matrix.CreateScale ( uiScale, uiScale, 1f );
		}

		// Gets the UV Transform
		public Matrix GetUVTransform(Texture2D t, Vector2 offset, float scale)
		{
				return
						Matrix.CreateScale(t.Width, t.Height, 1f)*
						Matrix.CreateScale(scale, scale, 1f)*
						Matrix.CreateTranslation(offset.X, offset.Y, 0f)*
						GetView()*
						Matrix.CreateScale(1f/Globals.ScreenSize.Width, 1f/Globals.ScreenSize.Height, 1f);
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
