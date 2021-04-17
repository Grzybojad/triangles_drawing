using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Projekt1.Scripts
{
	public class PaintingCanvas
	{
		public static PaintingCanvas CreateInstance( int width, int height )
		{
			PaintingCanvas paintingCanvas = new PaintingCanvas();
			paintingCanvas.InitCanvas( width, height );
			
			return paintingCanvas;
		}

		public WriteableBitmap WriteableBitmap { get; private set; }
		byte[,,] pixels;

		int width, height;
		public int Width => width;
		public int Height => height;

		void InitCanvas( int w, int h )
		{
			width = w;
			height = h;
			WriteableBitmap = new WriteableBitmap( width, height, 96, 96, PixelFormats.Rgb24, null );
			pixels = new byte[h, w, 4];
			
			Clear( Colors.Black );
		}

		public void Clear( Color clearColor )
		{
			for (int x = 0; x < height; x++)
			{
				for (int y = 0; y < width; y++)
				{
					pixels[x, y, 0] = clearColor.R;
					pixels[x, y, 1] = clearColor.G;
					pixels[x, y, 2] = clearColor.B;
				}
			}
		}
		
		public void DrawPixel( int x, int y, Color color )
		{
			if( x < 0 || y < 0 || x >= width || y >= height ) return;
			
			pixels[x, y, 0] = color.R;
			pixels[x, y, 1] = color.G;
			pixels[x, y, 2] = color.B;
		}

		public byte[] GetPixel( int x, int y )
		{
			if( x < 0 || y < 0 || x >= width || y >= height ) return null;

			return new[] { pixels[ x, y, 0 ], pixels[ x, y, 1 ], pixels[ x, y, 2 ] };
		}

		public bool IsPixelColored( int x, int y )
		{
			if( x < 0 || y < 0 || x >= width || y >= height ) return false;

			return pixels[ x, y, 0 ] > 0 || pixels[ x, y, 1 ] > 0 || pixels[ x, y, 2 ] > 0;
		}

		public void CopyBuffer( Color[,] rgbBuffer )
		{
			if( rgbBuffer.GetLength( 0 ) != width || rgbBuffer.GetLength( 0 ) != height )
			{
				throw new ArgumentOutOfRangeException( "Mismatched buffer size!" );
			}
			
			for (int x = 0; x < height; x++)
			{
				for (int y = 0; y < width; y++)
				{
					pixels[ x, y, 0 ] = rgbBuffer[ x, y ].R;
					pixels[ x, y, 1 ] = rgbBuffer[ x, y ].G;
					pixels[ x, y, 2 ] = rgbBuffer[ x, y ].B;
				}
			}
		}

		public void Save()
		{
			Int32Rect rect = new Int32Rect(0, 0, width, height);
			int stride = 3 * width;
			WriteableBitmap.WritePixels( rect, To1DArray(), stride, 0 );
		}
		
		byte[] To1DArray()
		{
			byte[] pixels1d = new byte[height * width * 3];
			
			int index = 0;
			for (int x = 0; x < height; x++)
			{
				for (int y = 0; y < width; y++)
				{
					for( int colorIndex = 0; colorIndex < 3; colorIndex++ )
					{
						pixels1d[ index++ ] = pixels[ x, y, colorIndex ];
					}
				}
			}

			return pixels1d;
		}
		
		public void ConvertColor( Color from, Color to )
		{
			for (int x = 0; x < height; x++)
			{
				for (int y = 0; y < width; y++)
				{
					if( pixels[ x, y, 0 ] == from.R && pixels[ x, y, 1 ] == from.G && pixels[ x, y, 2 ] == from.B )
					{
						pixels[ x, y, 0 ] = to.R;
						pixels[ x, y, 1 ] = to.G;
						pixels[ x, y, 2 ] = to.B;
					}
				}
			}
		}
	}
}