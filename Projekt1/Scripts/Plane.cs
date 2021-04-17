using System;
using System.Diagnostics;
using System.Numerics;

namespace Projekt1.Scripts
{
	public class Plane
	{
		public Plane( Triangle triangle )
		{
			Point a = triangle.Points[ 1 ] - triangle.Points[ 0 ];
			Point b = triangle.Points[ 2 ] - triangle.Points[ 0 ];

			cA = a.Y * b.Z - a.Z * b.Y;
			cB = a.Z * b.X - a.X * b.Z;
			cC = a.X * b.Y - a.Y * b.X;
			
			cD = -( cA * triangle.Points[ 0 ].X + cB * triangle.Points[ 0 ].Y + cC * triangle.Points[ 0 ].Z );
		}
		
		public Plane( Point p1, Point p2, Point p3 )
		{
			Point a = p2 - p1;
			Point b = p3 - p1;

			cA = a.Y * b.Z - a.Z * b.Y;
			cB = a.Z * b.X - a.X * b.Z;
			cC = a.X * b.Y - a.Y * b.X;
			
			cD = -( cA * p1.X + cB * p1.Y + cC * p1.Z );
		}
		
		float cA;
		float cB;
		float cC;
		float cD;

		public double DistanceToPlane( Point p, Vector3 line )
		{
			float ro = 0;
			if( Math.Abs( cA * line.X + cB * line.Y + cC * line.Z ) > 0 )
			{
				ro = ( cA * p.X + cB * p.Y + cC * p.Z + cD ) / ( cA * line.X + cB * line.Y + cC * line.Z );
			}
			else
			{
				ro = ( cA * p.X + cB * p.Y + cC * p.Z + cD );
			}

			float x1 = p.X - line.X * ro;
			float y1 = p.Y - line.Y * ro;
			float z1 = p.Z - line.Z * ro;

			return Math.Sqrt( Math.Pow( p.X - x1, 2 ) + Math.Pow( p.Y - y1, 2 ) + Math.Pow( p.Z - z1, 2 ) );
		}
	}
}