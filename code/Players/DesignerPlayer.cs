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

			Position = new Vector3( Position.x - 300, Position.y - 100, Position.z + 100 );
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( IsServer && Input.Down( InputButton.Attack1 ) )
			{
				var step = new ModelEntity();
				step.SetModel( "models/step.vmdl" );
				step.Scale = 0.1f;
				step.Position = new Vector3( 1000, Position.y, Position.z );
				step.Rotation = Rotation.Identity;
				step.EnableAllCollisions = true;
				step.SetupPhysicsFromModel( PhysicsMotionType.Static, false );
				RunAwayContext.StepList.Add( new Step
				{
					Entity = step,
					CreationDate = DateTime.Now
				});
			}
		}
	}
}
