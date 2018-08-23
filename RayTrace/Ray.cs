using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	public class Ray
	{
		public Vector3 Origin { get; set; }
		public Vector3 Direction { get; set; }

		public Ray() {
			Origin = new Vector3();
			Direction = new Vector3();
		}
        public Ray(Vector3 origin, Vector3 direct){
			Origin = origin;
			Direction = direct;
        }

		public Vector3 At(float t) {
			return Origin + t * Direction;
		}
	}
}
