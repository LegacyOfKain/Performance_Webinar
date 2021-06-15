using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Webinar.Tests
{
    class ForForeachTest : PerformanceTest
    {
        //constants
        private const int DEFAULT_ITERATIONS = 100;
        private const int LIST_SIZE = 1000_000;

        //fields
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
            //walk through the array using a foreach loop
            for (int i = 0; i < Iterations; i++)
            {
                foreach (int number in list)
                {
                    var number1 = number;
                }
            }
            return true;
        }

        protected override bool MeasureTestB()
        {
            //parse numbers using TryParse
            for (int i = 0; i < Iterations; i++)
            {
                for (int j = 0; j < LIST_SIZE; j++)
                {
                    var number1 = list[j];
                }
            }
            return true;
        }

        protected override bool MeasureTestC()
        {
            return false;
        }
    }
}

