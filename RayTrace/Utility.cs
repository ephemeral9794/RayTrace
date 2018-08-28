using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	class Utility
	{
		public static float Radian(float deg) { return (deg / 180.0f) * (float)Math.PI; }
		public static float Degree(float rad) { return (rad / (float)Math.PI) * 180.0f; }
		public static Color ToColor(Ray r, Color draw, Color back) {
			Vector3 d = r.Direction.Normalize();
			float t = 0.5f * (r.Direction.Y + 1.0f);
			return Color.Lerp(t, draw, back);
		}
		public static Color VectorToColor(Vector3 v) {
			v.Normalize();
			return new Color(v.X, v.Y, v.Z);
		}
		public static Vector3 ColorToVector(Color c) {
			return new Vector3(c.R, c.G, c.B);
		}
	}
}
