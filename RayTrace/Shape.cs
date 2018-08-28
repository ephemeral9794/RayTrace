using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	public struct HitRect {
		public float t;
		public Vector3 point;
		public Vector3 normal;
	}
	interface IShape {
		bool Hit(Ray ray, float t0, float t1, out HitRect hitrect);
	}
}
