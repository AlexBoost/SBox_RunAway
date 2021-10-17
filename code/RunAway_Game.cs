using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using RunAway.Extensions;
using RunAway.Ui;
using RunAway.Players;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace RunAway
{
	[Library( "Run Away" )]
	public partial class RunAway_Game : Sandbox.Game
	{
		[Net, Predicted] public static string PlayerClass { get; set; } = "Runner";
		public RunAway_Game()
		{
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				new RunAwayHUD();
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new RunnerPlayer();
			client.Pawn = player;

			player.Respawn();
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( IsServer && Input.Pressed( InputButton.Attack2 ) )
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
			else if (IsClient && Input.Pressed( InputButton.Attack2 )  )
			{
				if ( cl.Pawn is RunnerPlayer )
				{
					PlayerClass = "Designer";
				}
				else
				{
					PlayerClass = "Runner";
				}
			}
		}
	}

}
