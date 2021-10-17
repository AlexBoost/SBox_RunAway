using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAway.Extensions
{
	public static class VectorExtensions
	{
		public static string ToVectorString( this Vector2 vector )
		{
			return ($"X : {vector.x}, Y : {vector.y}");
		}

		public static string ToVectorString( this Vector3 vector )
		{
			return ($"X : {vector.x}, Y : {vector.y}, Z : {vector.z}");
		}
	}
}
