using System;
using System.Diagnostics;

namespace Performance_Webinar
{
    public class PerformanceResult
    {
        public int MillisecondsA { get; set; }
        public int MillisecondsB { get; set; }
        public int CollectionsA { get; set; }
        public int CollectionsB { get; set; }

        public PerformanceResult(int msA, int msB, int collA, int collB)
        {
            MillisecondsA = msA;
            MillisecondsB = msB;
            CollectionsA = collA;
            CollectionsB = collB;
        }
    }

    public class PerformanceTest : IPerformanceTest
    {
        private const int DEFAULT_REPETITIONS = 10;

        public string Name { get; }

        public string Description { get; }

        public int Iterations { get; set; }

        public bool RunBaseline { get; set; }

        protected virtual bool MeasureTestA()
        {
            return false;
        }

        protected virtual bool MeasureTestB()
        {
            return false;
        }

        protected virtual bool MeasureTestC()
        {
            return false;
        }

        public PerformanceTest(string name, string description, int interactions)
        {
            Name = name;
            Description = description;
            Iterations = interactions;
        }

        public (int, int, int) Measure()
        {
            long totalA = 0, totalB = 0, totalC = 0;

            var stopwatch = new Stopwatch();

            // run baseline tests
            if (RunBaseline)
            {
                for (long i = 0; i < DEFAULT_REPETITIONS; i++)
                {
                    stopwatch.Restart();
                    var implemented = MeasureTestA();
                    stopwatch.Stop();
                    if (implemented)
                        totalA += stopwatch.ElapsedMilliseconds;
                }
            }

            // run optimized test B
            for (long i = 0; i < DEFAULT_REPETITIONS; i++)
            {
                stopwatch.Restart();
                var implemented = MeasureTestB();
                stopwatch.Stop();
                if (implemented)
                    totalB += stopwatch.ElapsedMilliseconds;
            }

            // run optimized tests C
            for (long i = 0; i < DEFAULT_REPETITIONS; i++)
            {
                stopwatch.Restart();
                var implemented = MeasureTestC();
                stopwatch.Stop();
                if (implemented)
                    totalC += stopwatch.ElapsedMilliseconds;
            }

            // return results
            return (
                (int)(totalA / DEFAULT_REPETITIONS),
                (int)(totalB / DEFAULT_REPETITIONS),
                (int)(totalC / DEFAULT_REPETITIONS));
        }
    }
}
