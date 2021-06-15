using System;
using System.Text;
namespace Performance_Webinar.Tests
{
	public class ArraysTest : PerformanceTest
	{
		private const int DEFAULT_ITERATIONS = 300;

		public ArraysTest() : base("Arrays", "A:3-dimensional, B:1_dimensional, C:incremental", DEFAULT_ITERATIONS)
		{
		}

		protected override bool MeasureTestA()
		{
			// do some calculations with a 3-dimensional array
			var array = new int[Iterations, Iterations, Iterations];
			for (int i = 0; i < Iterations; i++)
			{
				for (int j = 0; j < Iterations; j++)
				{
					for (int k = 0; k < Iterations; k++)
					{
						array[i, j, k]++;
					}
				}
			}
			return true;
		}

		protected override bool MeasureTestB()
		{
			// do the same calculation, but now with a flattened 1-dimensional array
			var array = new int[Iterations * Iterations * Iterations];
			for (int i = 0; i < Iterations; i++)
			{
				for (int j = 0; j < Iterations; j++)
				{
					for (int k = 0; k < Iterations; k++)
					{
						var index = k + Iterations * (j + Iterations * i);
						array[index]++;
					}
				}
			}
			return true;
		}

		protected override bool MeasureTestC()
		{
			// do the same calculation, but now with a flattened array and incremental access
			var array = new int[Iterations * Iterations * Iterations];
			var index = 0;
			for (int i = 0; i < Iterations; i++)
			{
				for (int j = 0; j < Iterations; j++)
				{
					for (int k = 0; k < Iterations; k++)
					{
						array[index]++;
						index++;
					}
				}
			}
			return true;
		}
	}
}
