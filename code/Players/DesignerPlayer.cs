using minimal.Animators;
using minimal.Controllers;
using minimal.Extensions;
using Sandbox;
using Sandbox.UI;
using SBox_RunAway.Context;
using SBox_RunAway.Models;
using System;
using System.IO;
using System.Linq;

namespace MinimalExample
{
	partial class DesignerPlayer : Player
	{
		public override void Respawn()
		{
			SetModel( "models/cursoraim.vmdl" );
			Scale = 0.5f;

			Controller = new DesignerController();
			Animator = null;
			Camera = new DesignerCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();

			//Position = new Vector3( Position.x, Position.y - 500, Position.z + 300 );
			Rotation = Rotation.FromYaw( 90 );
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( IsServer && Input.Down( InputButton.Attack1 ) )
			{
				Log.Info( "SPAWN STEP : " + Position.ToVectorString() );
				var ragdoll = new ModelEntity();
				ragdoll.SetModel( "models/step.vmdl" );
				ragdoll.Scale = 0.1f;
				ragdoll.Position = new Vector3( 1000, Position.y, Position.z );
				ragdoll.Rotation = Rotation.Identity;
				ragdoll.EnableAllCollisions = true;
				ragdoll.SetupPhysicsFromModel( PhysicsMotionType.Static, false );
				RunAwayContext.StepList.Add( new Step
				{
					Entity = ragdoll,
					CreationDate = DateTime.Now
				});
			}
		}
	}
}
