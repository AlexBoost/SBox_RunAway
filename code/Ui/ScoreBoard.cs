using Sandbox;
using Sandbox.Hooks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

namespace RunAway.Ui
{
	class ScoreBoard : Panel
	{
		public Label scoreboard;
		public ScoreBoard()
		{
			scoreboard = Add.Label("", "text");
		}

		public override void Tick()
		{
			base.Tick();

			SetClass( "open", Input.Down( InputButton.Score ) );
		}
	}
}
