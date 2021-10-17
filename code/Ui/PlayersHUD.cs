using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using RunAway.Players;

namespace RunAway.Ui
{
	public class PlayersHUD : Panel
	{
		public Label playerClass;
		public Label subBox;
		public PlayersHUD()
		{
			playerClass = Add.Label( "", "value" );
			subBox = Add.Label( "", "subBox" );
		}

		public override void Tick()
		{
			var player = Local.Client;
			if ( player == null ) return;
			playerClass.Text = RunAway_Game.PlayerClass;
		}
	}
}
