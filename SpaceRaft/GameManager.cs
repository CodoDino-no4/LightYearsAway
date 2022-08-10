namespace SpaceRaft
{
		class GameManager
		{
				//private readonly BGManager _bgm = new();
				//private readonly InputManager _im = new();

				public GameManager()
				{
						//_bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Layer0"), 0.0f, 0.0f));
						//_bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Layer1"), 0.1f, 0.2f));
						//_bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Layer2"), 0.2f, 0.5f));
						//_bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Layer3"), 0.3f, 1.0f));
						//_bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Layer4"), 0.4f, 0.2f, -100.0f));
				}

				public void Update()
				{
						//_im.Update();
						//_bgm.Update(_im.Movement);
				}

				public void Draw()
				{
						//_bgm.Draw();
				}

				//public static void drawSeamlessBackground(SpriteBatch s, Texture2D t, GraphicsDevice gd, float parallax, Camera camera)
				//{
				//		Vector2 textureSize = new Vector2(t.Width, t.Height);
				//		Rectangle view = gd.Viewport.Bounds;

				//		Matrix m = Matrix.CreateTranslation(new Vector3(-cam.Origin/textureSize, 0.0f))*
				//								Matrix.CreateScale(1f/cam.Zoom)*
				//								Matrix.CreateScale(textureSize.X, textureSize.Y, 1)*
				//								Matrix.CreateRotationZ(-cam.Rotation)*
				//								Matrix.CreateScale(1f/textureSize.X, 1f/textureSize.Y, 1)*
				//								Matrix.CreateTranslation(new Vector3(cam.Origin/textureSize, 0.0f))*
				//								Matrix.CreateTranslation(new Vector3((cam.Position*parallax)/textureSize, 0.0f));

				//		infiniteShader.Parameters["ScrollMatrix"].SetValue(m);
				//		infiniteShader.Parameters["ViewportSize"].SetValue(new Vector2(view.Width, view.Height));

				//		s.Begin(samplerState: SamplerState.LinearWrap, effect: infiniteShader);
				//		s.Draw(t, new Vector2(0, 0), view, Color.White);
				//		s.End();
				//}
		}
}
