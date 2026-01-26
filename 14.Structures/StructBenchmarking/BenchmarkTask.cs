using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace StructBenchmarking;

public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
        GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                        // и как-то повлияет на них.

        task.Run();

        var timer = Stopwatch.StartNew();
        for (var i = 0; i < repetitionCount; i++)
        {
            task.Run();
        }
        timer.Stop();
        return timer.Elapsed.TotalMilliseconds / repetitionCount;
    }
}

public class StringBuilderTask : ITask
{
    public void Run()
    {
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < 10000; i++)
        {
            sb.Append('a');
        }
        sb.ToString();
    }
}

public class StringConstructorTask : ITask
{
    public void Run()
    {
        var str = new string('a', 10000);
    }
}

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var benchmark = new Benchmark();
        var stringBuilderTask = new StringBuilderTask();
        var stringConstructorTask = new StringConstructorTask();

        var repetitionCount = 1000;

        var stringBuilderTime = benchmark.MeasureDurationInMs(stringBuilderTask, repetitionCount);
        var stringConstructorTime = benchmark.MeasureDurationInMs(stringConstructorTask, repetitionCount);

        Assert.That(stringConstructorTime, Is.LessThan(stringBuilderTime));
    }
}
