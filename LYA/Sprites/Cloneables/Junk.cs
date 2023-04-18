using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Sprites.Cloneables
{
		public class Junk : CloneableSprite
		{
				private Random rand;
				private Vector2 direction;
				private float linearVelcocity;
				private float RotationVelocity;
				public Junk( Texture2D texture ) : base( texture )
				{
						Texture=texture;
						rand=new Random();
				}

				public override void Update()
				{
						JunkMovement();
						removeJunk();

				}
				//// Junk content
				//junk1=Globals.Content.Load<Texture2D>( "junk-1" );
				//		junk2=Globals.Content.Load<Texture2D>( "junk-2" );
				//		junk3=Globals.Content.Load<Texture2D>( "junk-3" );
				//		junk4=Globals.Content.Load<Texture2D>( "junk-4" );
				//		junk5=Globals.Content.Load<Texture2D>( "junk-5" );

				public void JunkMovement()
				{
						RotationVelocity=rand.Next( 2, 7 );
						linearVelcocity=rand.Next( 1, 5 )/8;
						direction.X=rand.Next( -1, 1 );
						direction.Y=rand.Next( -1, 1 );
						rotation-=MathHelper.ToRadians( RotationVelocity );

						Position.X+=direction.X*linearVelcocity;
						Position.Y+=direction.Y*linearVelcocity;

				}

				public void removeJunk()
				{
						//if (Position >= )

						if (!InScene)
						{
								//delete this instance???

						}
				}
		}
}
