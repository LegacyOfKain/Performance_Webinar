// Tutorial :- https://www.youtube.com/watch?v=-H5oEgOdO6U&t=421s&ab_channel=MarkFarragherMarkFarragher
// Transcript :- https://blog.postsharp.net/post/webinar-recording-fast-csharp1.html

using Performance_Webinar.Tests;
using System;

namespace Performance_Webinar
{
    class Program
    {
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

            PropertiesTest obj = new();
            obj.RunBaseline = true;
            Console.WriteLine(obj.Measure());

            Console.ReadKey();
        }
    }
}
