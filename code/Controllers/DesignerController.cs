using minimal.Extensions;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimal.Controllers
{
	[Library]
	public class DesignerController : BasePlayerController
	{
		public float Bounce { get; set; } = 0.25f;
		public float Size { get; set; } = 20.0f;

		public override void Simulate()
		{
			Rotation = Rotation.FromYaw( 180 );
			//var vel = new Vector3( 0, (Input.Left), (Input.Forward) );
			var vel = new Vector3( 0, -Input.MouseDelta.x, -Input.MouseDelta.y );

			vel = vel.Normal * 10000;

			if ( Input.Down( InputButton.Run ) )
				vel *= 2.0f;

			if ( Input.Down( InputButton.Duck ) )
				vel *= 0.5f;

			Velocity += vel * Time.Delta;

			if ( Velocity.LengthSquared > 0.01f )
			{
				Move( Velocity * Time.Delta );
			}

			Velocity = Velocity.Approach( 0, Velocity.Length * Time.Delta * 5.0f );

			if ( Input.Down( InputButton.Jump ) )
				Velocity = Velocity.Approach( 0, Velocity.Length * Time.Delta * 5.0f );
		}

		public void Move( Vector3 delta, int a = 0 )
		{
			if ( a > 1 )
				return;

			var len = delta.Length;
			var targetPos = Position + delta;
			var tr = Trace.Ray( Position, targetPos ).WorldOnly().Size( Size ).Run();

			if ( tr.StartedSolid )
			{
				Position = targetPos;
			}
			else if ( tr.Hit )
			{
				Position = tr.EndPos + tr.Normal;

				// subtract the normal from our velocity
				Velocity = Velocity.SubtractDirection( tr.Normal * (1.0f + Bounce) );

				delta = Velocity.Normal * delta.Length * (1.0f - tr.Fraction);

				Move( delta, ++a );
			}
			else
			{
				Position = tr.EndPos;
			}
		}
	}
}
