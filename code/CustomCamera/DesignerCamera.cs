
namespace Sandbox
{
	public class DesignerCamera : Camera
	{
		public Vector3 MyUpdatedPos = Vector3.Zero;

		public override void Activated()
		{ 
			Viewer = null;
		}

		public override void Update()
		{
			var pawn = Local.Pawn;
			if ( pawn == null ) return;

			FieldOfView = 10;
			Pos = new Vector3( -15000, 0, 900 );
		}

		public Vector3 GetPosition()
		{
			return Pos;
		}
	}
}
