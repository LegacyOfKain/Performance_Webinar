using System;
using System.Text;
namespace Performance_Webinar.Tests
{
    public class StringsTest : PerformanceTest
    {
        private const int DEFAULT_ITERATIONS = 50_000;

        public StringsTest() : base("Strings", "A:string, B:StringBuilder, C:char pointer", DEFAULT_ITERATIONS)
        {
        }

        protected override bool MeasureTestA()
        {
            // string additions using regular string type
            var result = string.Empty;
            for (int i = 0; i < Iterations; i++)
            {
                result = result + '*';
            }
            return true;
        }

        protected override bool MeasureTestB()
        {
            // string additions using stringbuilder
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Iterations; i++)
            {
                result.Append('*');
            }
            return true;
        }

        protected unsafe override bool MeasureTestC()
        {
            // fill string by using pointer operations
            var result = new char[Iterations];
            fixed (char* fixedPointer = result)
            {
                var pointer = fixedPointer;
                for (int i = 0; i < Iterations; i++)
                {
                    *(pointer++) = '*';
                }
            }
            return true;
        }
    }
}
