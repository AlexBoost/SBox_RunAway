using RunAway.Extensions;
using RunAway.Ui;
using Sandbox;
using Sandbox.UI;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace RunAway
{
	public partial class RunAwayHUD : HudEntity<RootPanel>
	{
		public RunAwayHUD()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/Ui/RunAway_HUD.scss" );

			RootPanel.AddChild<PlayersHUD>();
			RootPanel.AddChild<ScoreBoard>();

				//RootPanel.SetMouseCapture( true );
				//Log.Info( "Mouse position : " + RootPanel.MousePos.ToCustomString() );
		}
	}

}
