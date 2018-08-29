using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	public class ShapeList : IShape
	{
		List<IShape> list;

		public ShapeList()
		{
			list = new List<IShape>();
		}

		public void Add(IShape shape)
		{
			list.Add(shape);
		}

		public bool Hit(Ray ray, float t0, float t1, out HitRect hitrect)
		{
			hitrect = new HitRect();
			bool hit_anything = false;
			float closest_so_far = t1;
			foreach (var p in list) {
				if (p.Hit(ray, t0, closest_so_far, out HitRect temp_rec)) {
					hit_anything = true;
					closest_so_far = temp_rec.t;
					hitrect.t = temp_rec.t;
					hitrect.point = temp_rec.point;
					hitrect.normal = temp_rec.normal;
				}
			}
			return hit_anything;
		}
	}
}
