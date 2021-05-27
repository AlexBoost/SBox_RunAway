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
		private Vector3 _lastPos;

		public override void Simulate()
		{
			Vector3 vel = new Vector3( 0, -Input.MouseDelta.x, -Input.MouseDelta.y );

			Move( vel );
			//vel = vel.Normal * 20000;

			//Velocity += vel * Time.Delta;

			//if ( Velocity.LengthSquared > 0.01f )
			//{
			//	Move( Velocity * Time.Delta );
			//}

			//Velocity = Velocity.Approach( 0, Velocity.Length * Time.Delta * 5.0f );
		}

		public void Move( Vector3 delta, int a = 0 )
		{
			if ( a > 1 )
				return;

			var targetPos = Position + delta;

			Position = targetPos;
		}

		public override void FrameSimulate()
		{
			base.FrameSimulate();

			Rotation = Rotation.FromYaw( 180 );

			Vector3 vel = new Vector3( 0, -Mouse.Delta.x, -Mouse.Delta.y );

			Move( vel );
		}
	}
}
