using System;
using System.Text;
namespace Performance_Webinar.Tests
{
	public class MemoryTest : PerformanceTest
	{
		private const int DEFAULT_ITERATIONS = 500;
		private const int BUFFER_SIZE = 1_000_000;

		private byte[] buffer1 = null;
		private byte[] buffer2 = null;

		public MemoryTest() : base("Byte array copy", "A:direct, B:with pointers, C:CopyTo", DEFAULT_ITERATIONS)
		{
			buffer1 = new byte[BUFFER_SIZE];

			buffer2 = new byte[BUFFER_SIZE];
		}

		protected override bool MeasureTestA()
		{
			// copy buffer using a simple loop
			for (int i = 0; i < Iterations; i++)
			{
				for (int j = 0; j < BUFFER_SIZE; j++)
				{
					buffer2[j] = buffer1[j];
				}
			}
			return true;
		}

		protected unsafe override bool MeasureTestB()
		{
			// copy buffer using pointers
			fixed (byte* fixed1 = &buffer1[0])
			fixed (byte* fixed2 = &buffer2[0])
			{
				for (int i = 0; i < Iterations; i++)
				{
					var source = fixed1;
					var dest = fixed2;
					for (int j = 0; j < BUFFER_SIZE; j++)
					{
						*(dest++) = *(source++);
					}
				}
			}
			return true;
		}

		protected override bool MeasureTestC()
		{
			// copy buffer using the CopyTo method
			for (int i = 0; i < Iterations; i++)
			{
				buffer1.CopyTo(buffer2, 0);
			}
			return true;
		}

	}
}
