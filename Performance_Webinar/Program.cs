// Tutorial :- https://www.youtube.com/watch?v=-H5oEgOdO6U&t=421s&ab_channel=MarkFarragherMarkFarragher
// Transcript :- https://blog.postsharp.net/post/webinar-recording-fast-csharp1.html

using Performance_Webinar.Tests;
using System;
using System.Collections.Generic;

namespace Performance_Webinar
{
    class Program
    {
        private static List<IPerformanceTest> tests = new List<IPerformanceTest>();

        private static void ShowAllTests()
        {
            Console.WriteLine("Available tests:");
            for (int i = 0; i < tests.Count; i++)
            {
                Console.WriteLine($"{i}: {tests[i].Name}");
            }
            Console.WriteLine();
        }

        private static IPerformanceTest AskForTest()
        {
            Console.Write("Please select a test: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
                return null;
            else
                return index >= 0 && index < tests.Count ? tests[index] : null;
        }

        private static void ConfigureTest(IPerformanceTest test)
        {
            Console.Write($"How many iterations ({test.Iterations}): ");
            int iterations = 0;
            if (int.TryParse(Console.ReadLine(), out iterations))
            {
                test.Iterations = iterations;
            }
            Console.Write($"Run baseline test? (yes): ");
            var input = Console.ReadLine();
            test.RunBaseline = (string.IsNullOrEmpty(input) || input.ToLower().StartsWith("y"));
        }

        private static void ShowGraph((int, int, int) result)
        {
            const int NUM_STARS = 50;

            // normalize results
            var max = Math.Max(result.Item1, Math.Max(result.Item2, result.Item3));
            var barA = max > 0 ? result.Item1 * NUM_STARS / max : 0;
            var barB = max > 0 ? result.Item2 * NUM_STARS / max : 0;
            var barC = max > 0 ? result.Item3 * NUM_STARS / max : 0;

            // show bar graph
            Console.WriteLine($"A |{new string('\x25A0', barA)}");
            Console.WriteLine($"B |{new string('\x25A0', barB)}");
            Console.WriteLine($"C |{new string('\x25A0', barC)}");
            Console.WriteLine("  +--------------------------------------------------");
            Console.WriteLine($"    A: {result.Item1}ms, B: {result.Item2}ms, C: {result.Item3}ms");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            /*
            ExceptionTest obj = new ExceptionTest();
            obj.RunBaseline = true;
            obj.Iterations = 100000; // use this setting for best test results
            Console.WriteLine(obj.Measure());
            */

            /*
            StringTest obj = new StringTest();
            obj.RunBaseline = true;
            obj.Iterations = 200000; // use this setting for best test results
            Console.WriteLine(obj.Measure());
            */

            /*
            ArraysTest obj = new ();
            obj.RunBaseline = true;
            Console.WriteLine(obj.Measure());
            */

            /*
            ForForeachTest obj = new();
            obj.RunBaseline = true;
            Console.WriteLine(obj.Measure());
            */

            /* 
            StructsTest obj = new();
            obj.RunBaseline = true;
            Console.WriteLine(obj.Measure());
            */

            /*
            MemoryTest obj = new();
            obj.RunBaseline = true;
            Console.WriteLine(obj.Measure());
            */

            /*
            InstantiationTest obj = new();
            obj.RunBaseline = true;
            Console.WriteLine(obj.Measure());
            */

            /*
            PropertiesTest obj = new();
            obj.RunBaseline = true;
            Console.WriteLine(obj.Measure());

            Console.ReadKey();
            */


            // initialize test list
            tests.Add(new Tests.ExceptionTest());
            tests.Add(new Tests.StringsTest());
            tests.Add(new Tests.ArraysTest());
            tests.Add(new Tests.ForForeachTest());
            tests.Add(new Tests.StructsTest());
            tests.Add(new Tests.MemoryTest());
            tests.Add(new Tests.InstantiationTest());
            tests.Add(new Tests.PropertiesTest());

            while (true)
            {
                // show test menu
                ShowAllTests();

                // get test
                var test = AskForTest();
                if (test == null)
                    return;

                // configure test
                ConfigureTest(test);

                // run test
                Console.WriteLine();
                Console.WriteLine($"Running '{test.Name}' with {test.Iterations} iterations...");
                Console.WriteLine(test.Description);
                var result = test.Measure();
                Console.WriteLine();

                // show results
                ShowGraph(result);
            }
        }
    }
}
