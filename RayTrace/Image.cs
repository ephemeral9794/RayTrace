using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace RayTrace
{
	public struct Color
	{
		public static Color White {
			get { return new Color(1.0f, 1.0f, 1.0f); }
		}
		public static Color Black {
			get { return new Color(0.0f, 0.0f, 0.0f); }
		}
		public static Color Gray {
			get { return new Color(0.5f, 0.5f, 0.5f); }
		}
		public static Color Red {
			get { return new Color(1.0f, 0.0f, 0.0f); }
		}
		public static Color Green {
			get { return new Color(0.0f, 1.0f, 0.0f); }
		}
		public static Color Blue {
			get { return new Color(0.0f, 0.0f, 1.0f); }
		}

		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }

		public Color(float r, float g, float b) {
			R = r;
			G = g;
			B = b;
		}
		public Color(Color src) {
            R = src.R;
            G = src.G;
            B = src.B;
        }
		public Color(float intensity) {
			R = G = B = intensity;
		}

		public Color Clamp(float min = 0, float max = 1)
		{
			R = Clamp(R, min, max);
			G = Clamp(G, min, max);
			B = Clamp(B, min, max);
			return this;
		}
		public static float Clamp(float input, float min, float max)
		{
			if (input > max) {
				return max;
			} else if (input < min) {
				return min;
			} else {
				return input;
			}
		}

		// see Blender
		public static byte FloatToByte(float inValue) {
			return inValue<=0.0f ? (byte)0
					: ( inValue>1.0f-0.5f/255.0f ? (byte)255
						: (byte)(255.0f*inValue+0.5f) );
		}
		
        // operator overload
        static public Color operator-(Color c) {
            return new Color(-c.R,-c.G,-c.B);
        }
        static public Color operator+(Color c1, Color c2) {
            return new Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
        }
        static public Color operator-(Color c1, Color c2) {
			return new Color(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B);
        }
        static public Color operator*(Color c1, Color c2) {
			return new Color(c1.R * c2.R, c1.G * c2.G, c1.B * c2.B);
        }
        static public Color operator*(float f, Color c) {
            return new Color(f * c.R, f * c.G, f * c.B);
        }
        static public Color operator*(Color c, float f) {
            return f * c;
        }
        static public Color operator/(Color c1, Color c2) {
			return new Color(c1.R / c2.R, c1.G / c2.G, c1.B / c2.B);
        }
        static public Color operator/(Color c,float f) {
            float inv = 1.0f / f;
            return c * inv;
        }
        static public bool operator==(Color inColor1,Color inColor2) {
            return inColor1.R==inColor2.R
                && inColor1.G==inColor2.G
                && inColor1.B==inColor2.B; 
        }
        static public bool operator!=(Color inColor1,Color inColor2) {
            return inColor1.R!=inColor2.R
                || inColor1.G!=inColor2.G
                || inColor1.B!=inColor2.B;
        }
 
        public override bool Equals(object inObj) {
            if(inObj == null || inObj.GetType() != GetType()) {
                return false;
            }
            Color rgb=(Color)inObj;
            return this == rgb;
        }
        public override int GetHashCode() {
            return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
        }
        public override string ToString() {
            return $"[{R},{G},{B}]";
        }
	}

	public class Image
	{
		Color[,] pixels;
		public int Width { get; private set; }
		public int Height { get; private set; }

		public Image(int inWidth, int inHeight) {
            Init(inWidth, inHeight);
        }

        public Image(int inWidth, int inHeight, Color inBackgroundColor) {
            Init(inWidth, inHeight);
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    pixels[x, y] = inBackgroundColor.Clamp();
                }
            }
        }

		void Init(int inWidth,int inHeight) {
            Width = inWidth;
            Height = inHeight;
            pixels = new Color[Width, Height];
        }

		public Color this[int x, int y] {
			get { return pixels[x, y]; }
			set { pixels[x, y] = value.Clamp(); }
		}

		public void Export(string path) {
			using (var file = File.Create(path)) {
				WriteString(file, "P6\n");
                WriteString(file, $"{Width} {Height}\n");
                WriteString(file,"255\n");
                for(int y = 0; y < Height; y++) {
                    for(int x = 0; x < Width; x++) {
                        var color=pixels[x,y];
                        color=color.Clamp();
                        byte r = Color.FloatToByte(color.R);
                        byte g = Color.FloatToByte(color.G);
                        byte b = Color.FloatToByte(color.B);
                        file.WriteByte(r);
                        file.WriteByte(g);
                        file.WriteByte(b);
                    }
                }
			}
		}
		static void WriteString(Stream stream, string str) {
			byte[] bin = Encoding.ASCII.GetBytes(str);
			stream.Write(bin, 0, bin.Length);
		}
	}
}
