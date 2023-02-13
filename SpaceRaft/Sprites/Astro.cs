﻿using LYA.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites
{
		public class Astro : SpriteHandler
		{
				private enum state
				{
						Idle,
						Walk,
						Float
				};

				public int State
				{
						get; set;
				}


				public Astro( Texture2D texture ) : base( texture )
				{
						State=(int) state.Float;
				}

				public override void Update()
				{

						Movement();
						Globals.astroPos=Position;

				}

				public void Movement()
				{
						if (Input.Up().Held())
								Position.Y-=3f;

						if (Input.Down().Held())
								Position.Y+=3f;

						if (Input.Left().Held())
								Position.X-=3f;

						if (Input.Right().Held())
								Position.X+=3f;
				}

				public void UpdateState()
				{

				}

				public void KeyPresses()
				{
						//if (Input.Place().Pressed())
						//		PlaceTile();

				}
		}
}
