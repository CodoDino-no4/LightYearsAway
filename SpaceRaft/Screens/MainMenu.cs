﻿using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Screens
{
		internal class MainMenu : GameScreen
		{
				public MainMenu( Game game ) : base( game )
				{
				}

				private new LYA Game => (LYA) base.Game;

				public override void LoadContent()
				{
						base.LoadContent();
				}

				public override void Draw( GameTime gameTime )
				{

				}

				public override void Update( GameTime gameTime )
				{
						
				}
		}
}
