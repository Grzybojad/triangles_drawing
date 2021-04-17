using System.Numerics;
using System.Windows.Media;

namespace Projekt1.Scripts
{
	public class ShapesPainting
	{
		public PaintingCanvas PaintRedGreenTriangles( Triangle redTriangle, Triangle greenTriangle )
		{
			// Canvas settings
			const int width = 360;
			const int height = 360;
			const float pixelSize = 0.01f;
			
			// Camera settings
			const float yOffset = 10;
			const float focalLength = 5;
			
			// Transform the triangles for the camera's perspective
			Triangle perspectiveRedTriangle = GetTriangleInPerspective( redTriangle, focalLength );
			Triangle perspectiveGreenTriangle = GetTriangleInPerspective( greenTriangle, focalLength );

			// Triangle shadows
			PaintingCanvas t1Shadow = GetTriangleShadow( perspectiveRedTriangle, width, height );
			PaintingCanvas t2Shadow = GetTriangleShadow( perspectiveGreenTriangle, width, height );

			Plane plane1 = new Plane( redTriangle );
			Plane plane2 = new Plane( greenTriangle );

			double[,] zBuffer = new double[width, height];
			FillBuffer( zBuffer, 1000000 );

			PaintingCanvas trianglesCanvas = PaintingCanvas.CreateInstance( width, height );
			
			for( int i = 0; i < trianglesCanvas.Height; i++ )
			{
				float y = ( trianglesCanvas.Height / 2 - i ) * pixelSize;
				
				for( int j = 0; j < trianglesCanvas.Width; j++ )
				{
					float x = ( j - trianglesCanvas.Width / 2 ) * pixelSize;

					// Draw triangle 1
					if( t1Shadow.IsPixelColored( i, j ) )
					{
						Point cameraPoint = new Point( x, yOffset, y );
						Vector3 line = new Vector3( -x, 1, -y );
						
						double d = plane1.DistanceToPlane( cameraPoint, line );

						if( d < zBuffer[ i, j ] )
						{
							zBuffer[ i, j ] = d;
							trianglesCanvas.DrawPixel( i, j, Colors.Red );
						}
					}
					
					// Draw triangle 2
					if( t2Shadow.IsPixelColored( i, j ) )
					{
						Point cameraPoint = new Point( x, yOffset, y );
						Vector3 line = new Vector3( -x, 1, -y );

						double d = plane2.DistanceToPlane( cameraPoint, line );

						if( d < zBuffer[ i, j ] )
						{
							zBuffer[ i, j ] = d;
							trianglesCanvas.DrawPixel( i, j, Colors.Lime );
						}
					}
				}
			}

			trianglesCanvas.Save();
			return trianglesCanvas;
		}
		
		Triangle GetTriangleInPerspective( Triangle triangle, float focalLength )
		{
			Matrix4x4 h = Transformations.CreatePerspectiveMatrix( focalLength );

			return Triangle.MultiplyTriangleByMatrix( triangle, h );
		}

		PaintingCanvas GetTriangleShadow( Triangle triangle, int canvasHeight, int canvasWidth )
		{
			// Create separate canvases for the triangles shadows
			PaintingCanvas shadowCanvas = PaintingCanvas.CreateInstance( canvasWidth, canvasHeight );

			Shadow.PaintTriangleShadow( triangle, shadowCanvas );

			return shadowCanvas;
		}

		void FillBuffer( double[,] buffer, double fillValue )
		{
			for( int x = 0; x < buffer.GetLength( 0 ); x++ )
			{
				for( int y = 0; y < buffer.GetLength( 1 ); y++ )
				{
					buffer[ x, y ] = fillValue;
				}
			}
		}
	}
}