using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Webinar.Tests
{
    public class PointClass
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointClass(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class PointClassFinalized : PointClass
    {
        public PointClassFinalized(int x, int y) : base(x, y)
        { }
        ~PointClassFinalized()
        {
            // added a finalizer to slow down the GC
        }
    }
    public struct PointStruct
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointStruct(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class StructsTest : PerformanceTest
    {
        //constants
        private const int DEFAULT_ITERATIONS = 1_000_000;

        public StructsTest() : base("Structs", "A:finalized class, B:class, C:struct", DEFAULT_ITERATIONS)
        {

        }

        protected override bool MeasureTestA()
        {
            //access array elements
            var list = new PointClassFinalized[Iterations];
            for (int i = 0; i < Iterations; i++)
            {
                list[i] = new PointClassFinalized(i, i);
            }
            return true;
        }

        protected override bool MeasureTestB()
        {
            //access array elements
            var list = new PointClass[Iterations];
            for (int i = 0; i < Iterations; i++)
            {
                list[i] = new PointClass(i, i);
            }
            return true;

        }

        protected override bool MeasureTestC()
        {
            //access array elements
            var list = new PointStruct[Iterations];
            for (int i = 0; i < Iterations; i++)
            {
                list[i] = new PointStruct(i, i);
            }
            return true;
        }
    }
}
