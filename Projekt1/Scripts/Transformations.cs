using System;
using System.Numerics;

namespace Projekt1.Scripts
{
	public static class Transformations
	{
		public static Matrix4x4 CreatePerspectiveMatrix( float focalLength )
		{
			return new Matrix4x4
			{
				M11 = 1, 
				M42 = -1 / focalLength, 
				M33 = 1, 
				M44 = 1
			};
		}
		
		public static Matrix4x4 CreateTransformationMatrix( float roll, float pitch, float yaw, float px, float py, float pz )
		{
			float[] cos =
			{
				(float)Math.Cos( roll ), 
				(float)Math.Cos( pitch ), 
				(float)Math.Cos( yaw )
			};
			float[] sin =
			{
				(float)Math.Sin( roll ), 
				(float)Math.Sin( pitch ), 
				(float)Math.Sin( yaw )
			};

			return new Matrix4x4
			{
				M11 = cos[ 0 ] * cos[ 1 ],
				M12 = cos[ 0 ] * sin[ 1 ] * sin[ 2 ] - sin[ 0 ] * cos[ 2 ],
				M13 = cos[ 0 ] * sin[ 1 ] * cos[ 2 ] + sin[ 0 ] * sin[ 2 ],
				M14 = px,

				M21 = sin[ 0 ] * cos[ 1 ],
				M22 = sin[ 0 ] * sin[ 1 ] * sin[ 2 ] + cos[ 0 ] * cos[ 2 ],
				M23 = sin[ 0 ] * sin[ 1 ] * cos[ 2 ] - cos[ 0 ] * sin[ 2 ],
				M24 = py,

				M31 = -sin[ 1 ],
				M32 = cos[ 1 ] * sin[ 2 ],
				M33 = cos[ 1 ] * cos[ 2 ],
				M34 = pz,

				M41 = 0,
				M42 = 0,
				M43 = 0,
				M44 = 1,
			};
		}
	}
}