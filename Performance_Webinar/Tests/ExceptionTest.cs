using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Webinar.Tests
{
    public class ExceptionTest : PerformanceTest
    {
        //constants
        private const int DEFAULT_ITERATIONS = 100;
        private const int LIST_SIZE = 1000;
        private const int NUMBER_SIZE = 5;

        //fields
        private char[] digitArray = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'X' };
        private List<string> numbers = new List<string>();

        public ExceptionTest() : base("Exceptions", "A:Parse, B:TryParse", DEFAULT_ITERATIONS)
        {
            Random random = new Random();
            for (int i = 0; i < LIST_SIZE; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int d = 0; d < NUMBER_SIZE; d++)
                {
                    int index = random.Next(digitArray.Length);
                    sb.Append(index);
                }
                numbers.Add(sb.ToString());
            }
        }

        protected override bool MeasureTestA()
        {
            //parse numbers using Parse
            for (int i = 0; i < Iterations; i++)
            {
                for (int j = 0; j < LIST_SIZE; j++)
                {
                    try
                    {
                        int.Parse(numbers[j]);
                    }
                    catch (FormatException)
                    {

                    }
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
                    var success = int.TryParse(numbers[j], out int number);
                }
            }
            return true;
        }
    }
}
