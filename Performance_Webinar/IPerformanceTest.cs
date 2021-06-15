using System;
namespace Performance_Webinar
{
    public interface IPerformanceTest
    {
        string Name { get; }
        string Description { get; }

        int Iterations { get; set; }
        bool RunBaseline { get; set; }

        (int, int, int) Measure();
    }
}
