using LYA.Helpers;
using LYA.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Commands
{
		public class MoveUp : CommandManager.ICommand
		{
				public Astro astro;
				public MoveUp( Astro astro ) : base()
				{
						this.astro=astro;
				}

				public void Execute()
				{
								astro.Position.Y-=3f;
								astro.Direction.Y=1;
				}
		}
}
