using System.Numerics;
using System.Windows;
using Projekt1.Scripts;
using Point = Projekt1.Scripts.Point;

namespace Projekt1
{
	public partial class MainWindow : Window
	{
		Triangle redTriangle;
		Triangle greenTriangle;

		public MainWindow()
		{
			InitializeComponent();

			CreateTriangles();

			ShapesPainting a = new ShapesPainting();
			PaintingCanvas part1Canvas = a.PaintRedGreenTriangles( redTriangle, greenTriangle );
			ImageCanvas1.Source = part1Canvas.WriteableBitmap;

			RotateTriangles( 90, 5, 61 );
			PaintingCanvas part2Canvas = a.PaintRedGreenTriangles( redTriangle, greenTriangle );
				
			ImageCanvas2.Source = part2Canvas.WriteableBitmap;
		}

		void CreateTriangles()
		{
			Point pA1 = new Point( 2, 0, 0 );
			Point pA2 = new Point( -1, -2, -1 );
			Point pA3 = new Point( -1, 2, 1 );
			redTriangle = new Triangle( pA1, pA2, pA3 );
			
			Point pB1 = new Point( 0, 0, -2 );
			Point pB2 = new Point( -1, -1, 2 );
			Point pB3 = new Point( 1, 1, 2 );
			greenTriangle = new Triangle( pB1, pB2, pB3 );
		}

		void RotateTriangles( float roll, float pitch, float yaw )
		{
			Matrix4x4 h = Transformations.CreateTransformationMatrix( roll, pitch, yaw, 0, 0, 0 );

			redTriangle = Triangle.MultiplyTriangleByMatrix( redTriangle, h );
			greenTriangle = Triangle.MultiplyTriangleByMatrix( greenTriangle, h );
		}
	}
}