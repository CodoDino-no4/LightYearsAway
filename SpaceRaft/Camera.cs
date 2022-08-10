using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceRaft;
using SpaceRaft.Models;

public class Camera
{
		public float Zoom
		{
				get; set;
		}

		public Vector2 Position;
		public Rectangle Bounds
		{
				get; protected set;
		}
		public Rectangle VisibleArea
		{
				get; protected set;
		}
		public Matrix Transform
		{
				get; protected set;
		}

		public Input input;

		protected KeyboardState _currentKey;
		protected KeyboardState _previousKey;

		private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

		public Camera(Viewport viewport)
		{
				Bounds=viewport.Bounds;
				Zoom=2f;
				Position=Vector2.Zero;
		}


		private void UpdateVisibleArea()
		{
				var inverseViewMatrix = Matrix.Invert(Transform);

				var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
				var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
				var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
				var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

				var min = new Vector2(
						MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
						MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
				var max = new Vector2(
						MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
						MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));

				VisibleArea=new Rectangle((int) min.X, (int) min.Y, (int) (max.X-min.X), (int) (max.Y-min.Y));

		}

		public void FollowPosition(Vector2 target)
		{
				var position = Matrix.CreateTranslation(
					-target.X,
					-target.Y,
					0);

				var offset = Matrix.CreateTranslation(Bounds.Width/2, Bounds.Height/2, 0);

				Transform=position*Matrix.CreateScale(Zoom)*offset;
				UpdateVisibleArea();
		}

		public Vector2 MoveCamera(Vector2 movePosition)
		{
				if (Keyboard.GetState().IsKeyDown(Keys.W))
						movePosition.Y-=3f;
				if (Keyboard.GetState().IsKeyDown(Keys.S))
						movePosition.Y+=3f;

				if (Keyboard.GetState().IsKeyDown(Keys.A))
						movePosition.X-=3f;
				if (Keyboard.GetState().IsKeyDown(Keys.D))
						movePosition.X+=3f;

				return movePosition;
		}

		public void AdjustZoom(float zoomAmount)
		{
				Zoom+=zoomAmount;
				if (Zoom<1f)
				{
						Zoom=1f;
				}
				if (Zoom>6f)
				{
						Zoom=6f;
				}
		}

		public void UpdateCamera(Viewport bounds, Vector2 target)
		{
				Bounds=bounds.Bounds;

				FollowPosition(target);

				previousMouseWheelValue=currentMouseWheelValue;
				currentMouseWheelValue=Mouse.GetState().ScrollWheelValue;

				if (currentMouseWheelValue>previousMouseWheelValue)
				{
						AdjustZoom(.2f);
				}

				if (currentMouseWheelValue<previousMouseWheelValue)
				{
						AdjustZoom(-.2f);
				}


				previousZoom=zoom;
				zoom=Zoom;
				if (previousZoom!=zoom)
				{
						//change ui stuff
						//Debug.WriteLine( "Zoomy");

				}
		}
}
