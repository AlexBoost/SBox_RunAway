using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using minimal.Extensions;
using SBox_RunAway.Context;
using System.Linq;
using System.Collections.Generic;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace MinimalExample
{

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// 
	/// Your game needs to be registered (using [Library] here) with the same name 
	/// as your game addon. If it isn't then we won't be able to find it.
	/// </summary>
	[Library( "runaway" )]
	public partial class MinimalGame : Sandbox.Game
	{
		private DateTime _lavaResetTime;
		private bool _isLavaMoving;
		private ModelEntity _lava;

		public MinimalGame()
		{
			if ( IsServer )
			{
				InitLava();
				ResetLava();

				Log.Info( "My Gamemode Has Created Serverside!" );

				// Create a HUD entity. This entity is globally networked
				// and when it is created clientside it creates the actual
				// UI panels. You don't have to create your HUD via an entity,
				// this just feels like a nice neat way to do it.
				new MinimalHudEntity();
			}
		}

		private void InitLava()
		{
			_lava = new ModelEntity();
			_lava.SetModel( "models/lavafloor2.vmdl" );
			_lava.Scale = 1f;
			_lava.Rotation = Rotation.FromYaw( 180 );
			_lava.EnableAllCollisions = true;
			_lava.SetupPhysicsFromModel( PhysicsMotionType.Static, false );
		}

		private void ResetLava()
		{
			_lava.Position = new Vector3( 1000, Position.y, Position.z - 700 );
			_lavaResetTime = DateTime.Now;
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new RunnerPlayer();
			client.Pawn = player;

			RunAwayContext.ClientList.Add(client);

			player.Respawn();
		}

		public override void ClientSpawn()
		{
			base.ClientSpawn();
		}

		public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
		{
			RunAwayContext.ClientList.RemoveAll(x => x.SteamId == cl.SteamId );
			base.ClientDisconnect( cl, reason );
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( IsServer)
			{
				RemoveOldStep();
				//MoveLava();
				if ( Input.Pressed( InputButton.Attack2 ) )
				{
					cl.Pawn.Delete();
					if ( cl.Pawn is RunnerPlayer )
					{
						var player = new DesignerPlayer();
						cl.Pawn = player;
						player.Respawn();
					}
					else
					{
						var player = new RunnerPlayer();
						cl.Pawn = player;
						player.Respawn();
					}
				}
			}
		}

		private void MoveLava()
		{
			if ( !_isLavaMoving && _lavaResetTime < DateTime.Now.AddSeconds( -10 ) )
			{
				_lava.MoveTo( new Vector3( _lava.Position.x, _lava.Position.y, 800 ), 60f );
				_isLavaMoving = true;
			}
		}

		private void RemoveOldStep()
		{
			if ( RunAwayContext.StepList.Any() )
			{
				float time = 15;

				if ( RunAwayContext.ClientList.Count == 1 )
					time = 30;

				var stepList = RunAwayContext.StepList.Where( x => x.CreationDate < DateTime.Now.AddSeconds( -time ) ).ToArray();
				foreach ( var step in stepList )
				{
					step.Entity.Delete();
					RunAwayContext.StepList.Remove( step );
				}
			}
		}
	}

}
