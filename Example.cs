using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDBrequestsBenchmark
{
    class Example
    {
        static async Task ExampleMethod()
        {
            Stopwatch longApproach = new Stopwatch();
            longApproach.Start();
            await DoSomeWork();
            await DoSomeWork();
            await DoSomeWork();
            await DoSomeWork();
            await DoSomeWork();
            longApproach.Stop();
            Console.WriteLine(longApproach.ElapsedMilliseconds / 1000); //Output is 5 seconds

            Stopwatch fastApproach = new Stopwatch();
            fastApproach.Start();
            var task1 = DoSomeWork();
            var task2 = DoSomeWork();
            var task3 = DoSomeWork();
            var task4 = DoSomeWork();
            var task5 = DoSomeWork();
            Task.WaitAll(task1, task2, task3, task4, task5);
            fastApproach.Stop();
            Console.WriteLine(fastApproach.ElapsedMilliseconds / 1000); //Output is 1 second
        }

        static async Task DoSomeWork()
        {
            await Task.Delay(1000);
        }
    }
}
