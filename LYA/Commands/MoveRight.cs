using LYA.Helpers;
using LYA.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Commands
{
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
