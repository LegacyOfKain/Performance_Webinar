using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Webinar.Tests
{
    public class StringTest : PerformanceTest
    {
        //constants
        private const int DEFAULT_ITERATIONS = 50_000;

        public StringTest() : base("Strings", "A:string, B:StringBuilder, C:char pointer", DEFAULT_ITERATIONS)
        {

        }

        protected override bool MeasureTestA()
        {
            //string additions using regular string type
            var result = String.Empty;
            for (int i = 0; i < Iterations; i++)
            {
                result = result + "*";
            }
            return true;
        }

        protected override bool MeasureTestB()
        {
            //Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",CultureInfo.InvariantCulture));
            //string additions using StringBuilder
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Iterations; i++)
            {
                result = result.Append("*");
            }
            //Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",CultureInfo.InvariantCulture));
            return true;

        }

        protected unsafe override bool MeasureTestC()
        {
            //fill string by using pointer operations
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
