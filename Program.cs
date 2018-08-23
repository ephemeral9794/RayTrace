using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	class Program
	{
		static void Main(string[] args)
		{
			try {
				/// http://www.sist.ac.jp/~iigura/ToyBox/
				/// https://qiita.com/mebiusbox2/items/89e2db3b24e4c39502fe

				// Image Test
				Console.WriteLine("-- Image Test --");
				int w = 640, h = 480;
				Image image = new Image(w, h, Color.Gray);
				int n = Math.Min(w, h);
				for (int y = 0; y < h; y++) {
					Console.WriteLine($"Rendering (y = {y}) {(100.0 * y / (h - 1))} %");
					for (int x = 0; x < w; x++) {
						float r = (float)x / w;
						float g = (float)y / h;
						float b = 0.5f;
						image[x, y] = new Color(r, g, b);
					}
				}
				image.Export(@"C:\Users\Administrator\Desktop\RayTrace_Sample01.ppm");

				// Vector Test
				Console.WriteLine("-- Vector Test --");
				Vector3 v1=new Vector3();
				Console.WriteLine("v1="+v1);
 
				Vector3 v2=new Vector3(1,2,3);
				Console.WriteLine("v1.Init(4,5,6)");
				v1.Initialize(4,5,6);
				Console.WriteLine("v1="+v1);
				Console.WriteLine("v2="+v2);
				Console.WriteLine("v1+v2="+(v1+v2));
				Console.WriteLine("v1・v2="+Vector3.Dot(v1,v2));
				Console.WriteLine("v1xV2="+Vector3.Cross(v1,v2));
				Console.WriteLine("2*v1="+2*v1);
				Console.WriteLine("normalize(v1)="+Vector3.Unit(v1));
				Console.WriteLine("v1="+v1);
				Console.WriteLine("v1.Normalize()");
				v1.Normalize();
				Console.WriteLine("v1="+v1);

				// Camera & Ray Test
				Vector3 cx = new Vector3(4.0f, 0.0f, 0.0f);
				Vector3 cy = new Vector3(0.0f, 2.0f, 0.0f);
				Vector3 cz = new Vector3(-2.0f, -1.0f, -1.0f);
				Camera camera = new Camera(cx, cy, cz);
				for (int y = 0; y < h; y++) {
					Console.WriteLine($"Rendering (y = {y}) {(100.0 * y / (h - 1))} %");
					for (int x = 0; x < w; x++) {
						float u = (float)(x) / w;
						float v = (float)(y) / h;
						Ray ray = camera.GetRay(u, v);
						image[x, y] = Utility.ToColor(ray, new Color(1, 1, 1), new Color(0, 0, 0));
					}
				}
				image.Export(@"C:\Users\Administrator\Desktop\RayTrace_Sample02.ppm");
			} catch (Exception e) {
				Console.Error.WriteLine(e.Message);
				Console.Error.WriteLine(e.StackTrace);
			}
		}
	}
}
