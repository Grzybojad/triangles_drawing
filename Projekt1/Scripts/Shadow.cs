using System.Windows.Media;

namespace Projekt1.Scripts
{
	public static class Shadow
	{
		class ShadowData
		{
			readonly float x1, x2, x3;
			readonly float y1, y2, y3;

			readonly float a1, a2, a3;
			readonly float b1, b2, b3;
			readonly float c1, c2, c3;

			public ShadowData( Triangle t )
			{
				x1 = t.Points[ 0 ].X;
				x2 = t.Points[ 1 ].X;
				x3 = t.Points[ 2 ].X;
				y1 = t.Points[ 0 ].Z;
				y2 = t.Points[ 1 ].Z;
				y3 = t.Points[ 2 ].Z;
			
				a1 = y3 - y2;
				a2 = y3 - y1;
				a3 = y1 - y2;
			
				b1 = x2 - x3;
				b2 = x1 - x3;
				b3 = x2 - x1;
			
				c1 = y2*(x3-x2) - x2*(y3-y2);
				c2 = y1*(x3-x1) - x1*(y3-y1);
				c3 = y2*(x1-x2) - x2*(y1-y2);
			}

			public bool IsPointInsideShadow( float x, float y )
			{
				if( ( a1 * x1 + b1 * y1 + c1 ) * ( a1 * x + b1 * y + c1 ) < 0 ) return false;
				if( ( a2 * x2 + b2 * y2 + c2 ) * ( a2 * x + b2 * y + c2 ) < 0 ) return false;
				if( ( a3 * x3 + b3 * y3 + c3 ) * ( a3 * x + b3 * y + c3 ) < 0 ) return false;

				return true;
			}
		}
		
		public static void PaintTriangleShadow( Triangle t, PaintingCanvas canvas )
		{
			ShadowData s = new ShadowData( t );

			for( int i = 0; i < canvas.Height; i++ )
			{
				int y = canvas.Height / 2 - i;
				
				for( int j = 0; j < canvas.Width; j++ )
				{
					int x = j - canvas.Width / 2;

					if( s.IsPointInsideShadow( x * 0.01f, y * 0.01f ) )
					{
						canvas.DrawPixel( i, j, Colors.White );
					}
				}
			}
		}
	}
}