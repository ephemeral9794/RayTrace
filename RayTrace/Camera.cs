using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	public class Camera
	{
		Vector3 origin;  // 位置
		Vector3[] uvw;  // 直交基底ベクトル

		public Camera() {
			uvw = new Vector3[3];
			for (int i = 0; i < uvw.Length; i++) {
				uvw[i] = new Vector3();
			}
		}
		public Camera(Vector3　u, Vector3 v, Vector3 w) {
			origin = new Vector3();
			uvw = new Vector3[3];
			uvw[0] = u;
			uvw[1] = v;
			uvw[2] = w;
		}
		public Camera(Vector3 lookfrom, Vector3 lookat, Vector3 vup, float vfov, float aspect) {
			Vector3 u, v, w;
			float halfH = (float)Math.Tan(Utility.Radian(vfov) / 2.0f);
			float halfW = aspect * halfH;
			origin = lookfrom;
			w = (lookfrom - lookat).Normalize();
			u = Vector3.Cross(vup, w).Normalize();
			v = Vector3.Cross(w, u);
			uvw = new Vector3[3];
			uvw[2] = origin - halfW * u - halfH * v - w;
			uvw[0] = 2.0f * halfW * u;
			uvw[1] = 2.0f * halfH * v;
		}

		public Ray GetRay(float u, float v) {
			return new Ray(origin, uvw[2] + uvw[0] * u + uvw[1] * v - origin);
		}
	}
}
