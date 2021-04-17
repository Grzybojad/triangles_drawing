using System;
using System.Numerics;

namespace Projekt1.Scripts
{
	public class Triangle
	{
		public Triangle( Point point1, Point point2, Point point3 )
		{
			Points = new Point[3];
			Points[ 0 ] = point1;
			Points[ 1 ] = point2;
			Points[ 2 ] = point3;
		}

		public Point[] Points { get; set; }
		
		public Triangle Copy()
		{
			return new Triangle( Points[ 0 ], Points[ 1 ], Points[ 2 ] );
		}

		public static Triangle MultiplyTriangleByMatrix( Triangle t, Matrix4x4 m )
		{
			Point a = Point.MultiplyByMatrix( t.Points[ 0 ], m );
			Point b = Point.MultiplyByMatrix( t.Points[ 1 ], m );
			Point c = Point.MultiplyByMatrix( t.Points[ 2 ], m );

			return new Triangle( a, b, c );
		}

		public override string ToString()
		{
			return $"[{Points[ 0 ]}, {Points[ 1 ]}, {Points[ 2 ]}]";
		}
	}
}