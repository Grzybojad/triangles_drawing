using System;
using System.Diagnostics;
using System.Numerics;

namespace Projekt1.Scripts
{
	public class Point
	{
		public Point( float x, float y, float z )
		{
			homogeneous = new Vector4 { X = x, Y = y, Z = z, W = 1 };
		}

		public Point Copy()
		{
			return new Point( homogeneous.X, homogeneous.Y, homogeneous.Z );
		}

		Vector4 homogeneous;

		public Vector4 Homogeneous
		{
			get
			{
				if( homogeneous.W != 1 )
				{
					FixCoordinates();
				}

				return homogeneous;
			}
			set
			{
				homogeneous = value;
				FixCoordinates();
			}
		}

		public float X => Homogeneous.X;
		public float Y => Homogeneous.Y;
		public float Z => Homogeneous.Z;

		void FixCoordinates()
		{
			if( homogeneous.Z == 1 ) return;
			
			homogeneous.X /= homogeneous.Z;
			homogeneous.Y /= homogeneous.Z;
			homogeneous.Z /= homogeneous.Z;
			homogeneous.Z = 1;
		}

		public static Point MultiplyByMatrix( Point p, Matrix4x4 m )
		{
			float x = m.M11 * p.X + m.M12 * p.Y + m.M13 * p.Z + m.M14 * p.Homogeneous.W;
			float y = m.M21 * p.X + m.M22 * p.Y + m.M23 * p.Z + m.M24 * p.Homogeneous.W;
			float z = m.M31 * p.X + m.M32 * p.Y + m.M33 * p.Z + m.M34 * p.Homogeneous.W;
			float w = m.M41 * p.X + m.M42 * p.Y + m.M43 * p.Z + m.M44 * p.Homogeneous.W;

			x /= w;
			y /= w;
			z /= w;

			return new Point( x, y, z );
		}

		public override string ToString()
		{
			return $"({Homogeneous.X}, {Homogeneous.Y}, {Homogeneous.Z})";
		}

		public static Point operator -( Point a, Point b )
		{
			float x = a.X - b.X;
			float y = a.Y - b.Y;
			float z = a.Z - b.Z;

			return new Point( x, y, z );
		}
	}
}