using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	public class Scene
	{
		Camera camera;
		Image image;
		ShapeList world;
		Color background;

		public Scene(Image img, Color back) {
			image = img;
			background = back;
		}

		public void Build() {
			Vector3 cx = new Vector3(4.0f, 0.0f, 0.0f);
			Vector3 cy = new Vector3(0.0f, 2.0f, 0.0f);
			Vector3 cz = new Vector3(-2.0f, -1.0f, -1.0f);
			//camera = new Camera(cx, cy, cz);
			int w = image.Width, h = image.Height;
			camera = new Camera(new Vector3(2,1,5), new Vector3(0,0,0), -Vector3.YAxis, 20.0f, w / h);

			world = new ShapeList();
			world.Add(new Sphere(new Vector3(0.0f, 0.0f, -1.0f), 0.5f));
			world.Add(new Sphere(new Vector3(0.0f, -100.5f, -1.0f), 100.0f));
		}

		Color ToColor(Ray ray, Color draw, Color back)
		{
			Vector3 d = ray.Direction.Normalize();
			float t = 0.5f * (ray.Direction.Y + 1.0f);
			return Color.Lerp(t, draw, back);
		}

		public void Render()
		{
			Build();

			int w = image.Width, h = image.Height;
			Console.WriteLine("-- Camera & Ray Test --");
			var sw = new Stopwatch();	// 計測用ストップウォッチ
			sw.Start();
			// 並列処理
			Parallel.For(0, h, (y) => {
				for (int x = 0; x < w; x++) {
					float u = (float)(x) / w;
					float v = (float)(y) / h;
					Ray ray = camera.GetRay(u, v);
					if (world.Hit(ray, 0.0f, float.MaxValue, out HitRect rect)) {
						image[x, y] = Utility.VectorToColor(0.5f * (rect.normal + Vector3.One));
					} else {
						image[x, y] = ToColor(ray, background, new Color(1.0f, 1.0f, 1.0f));
					}
				}
			});
			sw.Stop();
			Console.WriteLine("Complete Rendering ({0}ms)", sw.ElapsedMilliseconds);
		}
	}
}
