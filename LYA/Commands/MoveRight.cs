﻿using LYA.Sprites;

namespace LYA.Commands
{
		/// <summary>
		/// Handles move right
		/// </summary>
		public class MoveRight : CommandManager.ICommand
		{
				public Astro astro;
				public MoveRight( Astro astro ) : base()
				{
						this.astro=astro;
				}

				public void Execute()
				{
						astro.Position.X+=3f;
						astro.Direction.X=1;
				}
		}
}
