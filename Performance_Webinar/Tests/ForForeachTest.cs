using System;
using System.Collections.Generic;
using System.Text;
namespace Performance_Webinar.Tests
{
	public class ForForeachTest : PerformanceTest
	{
		private const int DEFAULT_ITERATIONS = 100;
		private const int LIST_SIZE = 1_000_000;

		private List<int> list = null;

		public ForForeachTest() : base("For/Foreach", "A:foreach, B:for", DEFAULT_ITERATIONS)
		{
			Random random = new Random();
			list = new List<int>(LIST_SIZE);
			for (int i = 0; i < LIST_SIZE; i++)
			{
				int number = random.Next(256);
				list.Add(number);
			}
		}

		protected override bool MeasureTestA()
		{
			// walk through the array using a foreach loop
			for (int i = 0; i < Iterations; i++)
			{
				foreach (int number in list)
				{
					// do something with number
				}
			}
			return true;
		}

		protected override bool MeasureTestB()
		{
			// walk through the array using a for loop
			for (int i = 0; i < Iterations; i++)
			{
				for (int j = 0; j < LIST_SIZE; j++)
				{
					var number = list[j];
				}
			}
			return true;
		}

		protected unsafe override bool MeasureTestC()
		{
			return false;
		}
	}
}
