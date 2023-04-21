﻿using LYA.Sprites;

namespace LYA.Commands
{
		public class MoveLeft : CommandManager.ICommand
		{
				public Astro astro;
				public MoveLeft( Astro astro ) : base()
				{
						this.astro=astro;
				}

				public void Execute()
				{
						astro.Position.X-=3f;
						astro.Direction.X=-1;
				}
		}
}
