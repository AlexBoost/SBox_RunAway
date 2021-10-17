using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAway.Animators
{
	public class MyCustomPlayerAnimator : PawnAnimator
	{
		public override void Simulate()
		{
			var idealRotation = Rotation.LookAt( Input.Rotation.Forward.WithZ( 0 ), Vector3.Up );

			DoWalk( idealRotation );

			//
			// Let the animation graph know some shit
			//
			bool noclip = HasTag( "noclip" );

			SetParam( "b_grounded", GroundEntity != null || noclip );
			SetParam( "b_noclip", noclip );
			SetParam( "b_swim", Pawn.WaterLevel.Fraction > 0.5f );

			SetParam( "b_ducked", HasTag( "ducked" ) );

			if ( Pawn.ActiveChild is BaseCarriable carry )
			{
				carry.SimulateAnimator( this );
			}
			else
			{
				SetParam( "holdtype", 0 );
				SetParam( "aimat_weight", 0.5f );
			}

		}

		void DoWalk( Rotation idealRotation )
		{
			//
			// These tweak the animation speeds to something we feel is right,
			// so the foot speed matches the floor speed. Your art should probably
			// do this - but that ain't how we roll
			//
			SetParam( "walkspeed_scale", 2.0f / 190.0f );
			SetParam( "runspeed_scale", 2.0f / 320.0f );
			SetParam( "duckspeed_scale", 2.0f / 80.0f );

			//
			// Work out our movement relative to our body rotation
			//
			var moveDir = WishVelocity;
			var forward = idealRotation.Forward.Dot( moveDir );
			var sideward = idealRotation.Right.Dot( moveDir );

			//
			// Set our speeds on the animgraph
			//
			var speedScale = Pawn.Scale;

			SetParam( "forward", forward );
			SetParam( "sideward", sideward );
			SetParam( "wishspeed", speedScale > 0.0f ? WishVelocity.Length / speedScale : 0.0f );
		}
	}
}
