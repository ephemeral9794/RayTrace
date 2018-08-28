using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	public class Sphere : IShape
	{
		Vector3 center;	// 中心
		float radius;	// 半径

		public Sphere() {
			center = Vector3.Zero;
			radius = 0.0f;
		}
		public Sphere(Vector3 c, float r) {
			center = c;
			radius = r;
		}

		public bool Hit(Ray ray, float t0, float t1, out HitRect rect)
		{
			rect = new HitRect();
			// center ->c
			// ray p(t) = ->o + t * ->d
			// ->oc = ->o - ->c
			Vector3 o_c = ray.Origin - center;
			// a = ->o・->o
			float a = Vector3.Dot(ray.Direction, ray.Direction);
			// b = 2(->d・->oc))
			float b = 2.0f * Vector3.Dot(ray.Direction, o_c);
			// c = (->oc・->oc) - r^2
			float c = Vector3.Dot(o_c, o_c) - (float)Math.Pow(radius, 2);
			// D = b^2 - 4ac
			float D = b * b - 4 * a * c;

			// hit
			if (D > 0) {
				// y = (-b - √D) / 2a
				float root = (float)Math.Sqrt(D);
				float temp = (-b - root) / (2.0f * a);
				if (temp < t1 && temp > t0) {
					rect.t = temp;
					rect.point = ray.At(rect.t);
					rect.normal = (rect.point - center) / radius;
					return true;
				}
				// y = (-b + √D) / 2a
				temp = (-b + root) / (2.0f * a);
				if (temp < t1 && temp > t0) {
					rect.t = temp;
					rect.point = ray.At(rect.t);
					rect.normal = (rect.point - center) / radius;
					return true;
				}
			}
			return false;
		}
	}
}
