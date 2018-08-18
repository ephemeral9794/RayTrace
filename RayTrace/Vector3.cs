using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
	public class Vector3
	{
		// member 
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float Length {
			get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
		}
		public float InverseLength {
			get { return 1.0f / (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
		}
		public static Vector3 Zero { 
			get { return new Vector3(0, 0, 0); } 
		}
		public static Vector3 One { 
			get { return new Vector3(1, 1, 1); } 
		}
		public static Vector3 XAxis { 
			get { return new Vector3(1, 0, 0); }
		}
		public static Vector3 YAxis {
			get { return new Vector3(0, 1, 0); }
		}
		public static Vector3 ZAxis {
			get { return new Vector3(0, 0, 1); }
		}

		public Vector3() {
			X = Y = Z = 0.0f;
		}
		public Vector3(float x, float y, float z) {
			X = x;
			Y = y;
			Z = z;
		}
		public Vector3(Vector3 src) {
			X = src.X;
			Y = src.Y;
			Z = src.Z;
		}

		public void Initialize(float x, float y, float z) {
			X = x;
			Y = y;
			Z = z;
		}
		public void NormalInitialize(float x, float y, float z) {
			X = x * InverseLength;
			Y = y * InverseLength;
			Z = z * InverseLength;
		}
		
		public static float Dot(Vector3 v1, Vector3 v2) {
			return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
		}
		public static Vector3 Cross(Vector3 v1, Vector3 v2) {
			return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y,
                               v1.Z * v2.X - v1.X * v2.Z,
                               v1.X * v2.Y - v1.Y * v2.X);
		}

		public static Vector3 Unit(Vector3 v) {
			float inv = v.InverseLength; 
			return v * inv;
		}
		public void Normalize() {
			X *= InverseLength;
			Y *= InverseLength;
			Z *= InverseLength;
		}

		// operator overload
		public static Vector3 operator-(Vector3 v) {
			return new Vector3(-v.X, -v.Y, -v.Z);
		}
		public static bool operator==(Vector3 v1, Vector3 v2) {
			return (v1.X == v2.X) && (v1.Y == v2.Y) &&(v1.Z == v2.Z);
		}
		public static bool operator!=(Vector3 v1, Vector3 v2) {
			return !(v1 == v2);
		}
		public static Vector3 operator*(float f, Vector3 v) {
			return new Vector3(v.X * f, v.Y * f, v.Z * f);
		}
		public static Vector3 operator*(Vector3 v, float f) {
			return f * v;
		}
		public static Vector3 operator/(Vector3 v, float f) {
			float inv = 1.0f / f;
			return v * inv;
		}
		public static Vector3 operator+(Vector3 v1, Vector3 v2) {
			return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
		}
		public static Vector3 operator-(Vector3 v1, Vector3 v2) {
			return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
		}

		public override bool Equals(object obj) {
            if(obj == null || obj.GetType() != GetType()) {
                return false;
            }
            Vector3 vec3=(Vector3)obj;
            return (this == vec3);
        }
        public override int GetHashCode() {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }
		public override string ToString() {
			return $"[{X},{Y},{Z}]";
		}
	}
}
