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
				// Image Test
				/*int w = 640, h = 480;
				Image image = new Image(w, h, Color.Gray);
				int n = Math.Min(w, h);
				for(int i = 0; i < n; i++) {
					// 色の計算式は適当
					image[i,i] = new Color(((i + 8) * 15 % 256) / 255.0f,
										  (i * 5 % 256) / 255.0f,
										  (i * 13 % 256) / 255.0f);
					Console.WriteLine("{0}:{1}", i, image[i, i]);
				}
				image.Export(@"C:\Users\Administrator\Desktop\RayTrace_Sample01.ppm");*/

				// Vector Test
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
			} catch (Exception e) {
				Console.Error.WriteLine(e.Message);
				Console.Error.WriteLine(e.StackTrace);
			}
		}
	}
}
